﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNAqbase.Models.Search
{
    public class KeywordFilter : Filter
    {
        public KeywordFilter()
        {
        }
        public override List<Condition> Conditions { get; set; } = new List<Condition>();
        public override string JoinConditions()
        {
            return "";
        }

        
    }
}