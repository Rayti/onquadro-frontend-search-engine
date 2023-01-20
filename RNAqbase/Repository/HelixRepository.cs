﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using RNAqbase.Models;

namespace RNAqbase.Repository
{
    public class HelixRepository : RepositoryBase, IHelixRepository
    {
        public HelixRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<HelixReference> GetHelixReferenceById(int id)
        {			
			using (var connection = Connection)
            {
                connection.Open();

                var helix = await connection.QueryFirstAsync<HelixReference>(
                    (@"
                        SELECT DISTINCT ON(h.id)
						h.id AS Id,     
						h.dot_bracket AS Dot_bracket,
						p.identifier AS PdbIdentifier,
                        p.title AS Title,
						to_char(MAX(p.release_date)::date, 'YYYY-MM-DD') as PdbDeposition,
						p.assembly AS AssemblyId,
						max(q_view.molecule) AS Molecule,
						STRING_AGG(COALESCE((n1.short_name)||(n2.short_name)||(n3.short_name)||(n4.short_name), ''), '') AS Sequence,
						COUNT(t.id) AS NumberOfTetrads,
						p.experiment AS Experiment,
						COUNT(DISTINCT(q.id)) AS NumberOfQuadruplexes,
						 CASE
								WHEN max(q_view.chains) = 1 THEN 'unimolecular'
								WHEN max(q_view.chains) = 2 THEN  'bimolecular'
								ELSE 'tetramolecular'
						 END 
						 as TypeOfStrands
					FROM HELIX h
					JOIN HELIX_QUADRUPLEX hq on h.id = hq.helix_id
					JOIN QUADRUPLEX q on hq.quadruplex_id = q.id
					JOIN QUADRUPLEX_VIEW q_view on q.id = q_view.id
					JOIN TETRAD t ON q.id = t.quadruplex_id
					JOIN NUCLEOTIDE n1 ON t.nt1_id = n1.id
					JOIN NUCLEOTIDE n2 ON t.nt2_id = n2.id
					JOIN NUCLEOTIDE n3 ON t.nt3_id = n3.id
					JOIN NUCLEOTIDE n4 ON t.nt4_id = n4.id
					JOIN PDB p ON n1.pdb_id = p.id
					where h.id = @HelixId
					GROUP BY h.id, p.identifier, n1.pdb_id, p.assembly, p.title, n1.molecule, p.experiment"),
                    new {HelixId = id});

                return helix;
            }
        }

        public async Task<IEnumerable<NucleotidesChiValues>> GetNucleotideChiValues(int id)
        {		
			using (var connection = Connection)
			{
                connection.Open();

                return await connection.QueryAsync<NucleotidesChiValues>(
                    (@"
						SELECT t.id as tetrad_id,
						n1.chi as n1_chi, 
						n2.chi as n2_chi, 
						n3.chi as n3_chi,
						n4.chi as n4_chi,
						n1.glycosidic_bond as n1_glycosidic_bond,
						n2.glycosidic_bond as n2_glycosidic_bond,
						n3.glycosidic_bond as n3_glycosidic_bond,
						n4.glycosidic_bond as n4_glycosidic_bond
						FROM helix_quadruplex hq
						JOIN tetrad t on t.quadruplex_id = hq.quadruplex_id
						JOIN nucleotide n1 on t.nt1_id = n1.id
						JOIN nucleotide n2 on t.nt2_id = n2.id
						JOIN nucleotide n3 on t.nt3_id = n3.id
						JOIN nucleotide n4 on t.nt4_id = n4.id
						WHERE hq.helix_id = @HelixId"),
                    new {HelixId = id});
            }
        }

        public async Task<List<HelixTable>> GetAllHelices()
        {			
			using (var connection = Connection)
			{
                connection.Open();

                return (await connection.QueryAsync<HelixTable>(
                    @"SELECT DISTINCT ON(h.id)
							h.id AS Id,       
    						string_agg(DISTINCT(q.id)::text, ',') as QuadruplexesIds,
							p.identifier AS PdbId,
							to_char(MAX(p.release_date)::date, 'YYYY-MM-DD') as PdbDeposition,
							p.assembly AS AssemblyId,
							max(q_view.molecule) AS Molecule,
    						max(p.experiment) AS Experiment,
							STRING_AGG(COALESCE((n1.short_name)||(n2.short_name)||(n3.short_name)||(n4.short_name), ''), '') AS Sequence,
							COUNT(t.id) AS NumberOfTetrads,
							p.experiment AS Experiment,
							COUNT(DISTINCT(q.id)) AS NumberOfQuadruplexes,
							 CASE
									WHEN max(q_view.chains) = 1 THEN 'unimolecular'
									WHEN max(q_view.chains) = 2 THEN  'bimolecular'
									ELSE 'tetramolecular'
							 END 
							 as TypeOfStrands
						FROM HELIX h
						JOIN HELIX_QUADRUPLEX hq on h.id = hq.helix_id
						JOIN QUADRUPLEX q on hq.quadruplex_id = q.id
						JOIN QUADRUPLEX_VIEW q_view on q.id = q_view.id
						JOIN TETRAD t ON q.id = t.quadruplex_id
						JOIN NUCLEOTIDE n1 ON t.nt1_id = n1.id
						JOIN NUCLEOTIDE n2 ON t.nt2_id = n2.id
						JOIN NUCLEOTIDE n3 ON t.nt3_id = n3.id
						JOIN NUCLEOTIDE n4 ON t.nt4_id = n4.id
						JOIN PDB p ON n1.pdb_id = p.id
						GROUP BY h.id, p.identifier, n1.pdb_id, p.assembly, n1.molecule, p.experiment
						order by h.id;
                        ")).ToList();
            }
        }

        public async Task<MemoryStream> GetHelix3dVisualization(int id)
        {			
			using (var connection = Connection)
			{
                connection.Open();
                var coordinates1Query = await connection.QueryAsync<string>
                (@" 
					SELECT 
						n1.coordinates
					FROM tetrad t
							JOIN nucleotide n1 on t.nt1_id = n1.id
					WHERE t.id IN (select tetrad.Id from quadruplex
						join tetrad on quadruplex.Id = tetrad.quadruplex_id 
					where quadruplex.id IN (select quadruplex_id from helix_quadruplex where helix_id = @id))",
                    new {id = id});

                var coordinates2Query = await connection.QueryAsync<string>
                (@" 
					SELECT 
						n2.coordinates
					FROM tetrad t
							JOIN nucleotide n2 on t.nt2_id = n2.id
					WHERE t.id IN (select tetrad.Id from quadruplex
						join tetrad on quadruplex.Id = tetrad.quadruplex_id 
					where quadruplex.id IN (select quadruplex_id from helix_quadruplex where helix_id = @id))",
                    new {id = id});

                var coordinates3Query = await connection.QueryAsync<string>
                (@" 
					SELECT 
						n3.coordinates
					FROM tetrad t
							JOIN nucleotide n3 on t.nt3_id = n3.id
					WHERE t.id IN (select tetrad.Id from quadruplex
						join tetrad on quadruplex.Id = tetrad.quadruplex_id 
					where quadruplex.id IN (select quadruplex_id from helix_quadruplex where helix_id = @id))",
                    new {id = id});

                var coordinates4Query = await connection.QueryAsync<string>
                (@" 
					SELECT 
						n4.coordinates
					FROM tetrad t
							JOIN nucleotide n4 on t.nt4_id = n4.id
					WHERE t.id IN (select tetrad.Id from quadruplex
						join tetrad on quadruplex.Id = tetrad.quadruplex_id 
					where quadruplex.id IN (select quadruplex_id from helix_quadruplex where helix_id = @id))",
                    new {id = id});


                var loopNucleotideCoordinates = await connection.QueryAsync<string>
                (@" 
					select coordinates from nucleotide where id IN 
					(select nucleotide_id from loop
					join loop_nucleotide on loop.id = loop_nucleotide.loop_id
					where loop.quadruplex_id in (select quadruplex_id from helix_quadruplex where helix_id = @id))",
                    new {id = id});


                var coordinates = new CoordinatesQuadruplex();

                coordinates.C1 = coordinates1Query.ToArray();
                coordinates.C2 = coordinates2Query.ToArray();
                coordinates.C3 = coordinates3Query.ToArray();
                coordinates.C4 = coordinates4Query.ToArray();
                coordinates.LoopNucleotideCoordinates = loopNucleotideCoordinates.ToArray();

                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(coordinates.CoordinatesAsString);
                writer.Flush();

                stream.Position = 0;
                return stream;
            }
        }
    }
}