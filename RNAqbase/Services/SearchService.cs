﻿using RNAqbase.Models;
using RNAqbase.Models.Search;
using RNAqbase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNAqbase.Services
{ 
    public class SearchService
    {
        private List<Filter> listOfFilters = new List<Filter>();
        private readonly SearchRepository searchRepository;
        private string query =
@"SELECT
MAX(q.id) AS Id,
q.loop_class as LoopTopology,
STRING_AGG(DISTINCT(qg.gba_quadruplex_class)::text,', ') AS TetradCombination,
CONCAT(MAX(q.onzm), MAX(q.subtype)) AS OnzmClass,
to_char(MAX(p.release_date)::date, 'YYYY-MM-DD') as PdbDeposition,
MAX(p.identifier) AS PdbId,
string_agg(DISTINCT(ion.name)::text, ', ') as Ion,
string_agg(DISTINCT(ion.charge)::text, ', ') as Ion_charge,  
MAX(p.assembly) AS AssemblyId,
MAX(q_view.molecule) AS Molecule,
MAX(p.experiment) AS Experiment,
STRING_AGG(COALESCE((n1.short_name)||(n2.short_name)||(n3.short_name)||(n4.short_name), ''), '') AS Sequence,
COUNT(DISTINCT SUBSTRING(t.onz::TEXT FROM 1 FOR 1)) AS TypeCount,
COUNT(DISTINCT(t.id)) AS NumberOfTetrads,
MAX(p.experiment) AS experiment,
CASE
		WHEN max(q_view.chains) = 1 THEN 'unimolecular'
		WHEN max(q_view.chains) = 2 THEN  'bimolecular'
		ELSE 'tetramolecular'
	END 
	as TypeOfStrands
FROM QUADRUPLEX q
JOIN QUADRUPLEX_GBA qg on qg.quadruplex_id = q.id
JOIN TETRAD t ON q.id = t.quadruplex_id
JOIN QUADRUPLEX_VIEW q_view ON q.id = q_view.id
JOIN NUCLEOTIDE n1 ON t.nt1_id = n1.id
JOIN NUCLEOTIDE n2 ON t.nt2_id = n2.id
JOIN NUCLEOTIDE n3 ON t.nt3_id = n3.id
JOIN NUCLEOTIDE n4 ON t.nt4_id = n4.id
JOIN PDB p ON n1.pdb_id = p.id
LEFT JOIN pdb_ion ON p.id = pdb_ion.pdb_id
LEFT JOIN ion ON ion.id = pdb_ion.ion_id
";

        public SearchService(SearchRepository searchRepository)
        {
            this.searchRepository = searchRepository;
        }

        public string GetTest()
        {
            bool isFirst = true;
            foreach (Filter filter in listOfFilters) 
            {
                var helper = filter.JoinConditions();
                if (helper != "")
                {
                    if (isFirst)
                    {
                        query += $"WHERE {helper} ";
                        isFirst = false;
                    }
                    else 
                    {
                        query += $"AND {helper} ";
                    }
                }
            }

            return query + "GROUP BY q.id HAVING COUNT(t.id) > 1;";
        }
    }

   
}