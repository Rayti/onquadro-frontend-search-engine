﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNAqbase.Models.Search
{
    public class NoTetradsFilter : Filter
    {
        public NoTetradsFilter()
        {
            FieldInSQL = "COUNT(DISTINCT(t.id))";
            joinType = JoinType.Having;
        }
        public override List<Condition> Conditions { get; set; } = new List<Condition>();

        public override string Join()
        {
            string query = "(";
            for (int i = 0; i < Conditions.Count; i++)
            {
                query += $"({FieldInSQL} {Conditions[i].Operator} {Conditions[i].Value})";
                if (i != Conditions.Count - 1)
                {
                    query += " AND ";
                }
            }

            return query + ")";
        }
    }
}
