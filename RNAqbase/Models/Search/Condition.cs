﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNAqbase.Models.Search
{
    public class Condition
    {
        public string Operator;
        public string Value;
        public Condition(string Operator, string Value)
        {
            this.Operator = Operator;
            this.Value = Value;
        }
    }
}