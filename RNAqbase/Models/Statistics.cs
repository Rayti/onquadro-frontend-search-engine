﻿namespace RNAqbase.Models
{
	public class Statistics
	{
		public string Sequence { get; set; }
		public int Dna { get; set; }
		public int Rna { get; set; }
		public int Other { get; set; }
		public int Total { get; set; }
		public string NumberOfTetrads { get; set; }
		public string Onz { get; set; }
		public int Unimolecular { get; set; }
		public int Bimolecular { get; set; }
		public int Tetramolecular { get; set; }
		public string Onzm { get; set; }
		public int Plus { get; set; }
		public int Minus { get; set; }
		public int Star { get; set; }
		public string Chains { get; set; }
		public string Ion { get; set; }
		public string gba_class { get; set; }
		public string loop_class { get; set; }
		public string loop_progression_da_silva { get; set; }
		public string experimental_method { get; set; }
	}
}
