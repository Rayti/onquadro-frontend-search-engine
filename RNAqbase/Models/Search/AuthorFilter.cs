using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNAqbase.Models.Search
{
    public class AuthorFilter
    {
        private readonly string _fieldInSQL;

        public AuthorFilter()
        {
            _fieldInSQL = "whatever"; // TODD from SQL
        }

        public List<Condition> Conditions
        {
            get
            {
                return Conditions;
            }
            set
            {
                Conditions.Add(new Condition("=", "A"));
                Conditions.Add(new Condition("!=", "B"));
                Conditions.Add(new Condition("=", "C"));
                Conditions.Add(new Condition("=", "D"));
                Conditions.Add(new Condition("!=", "E"));
            }
        }

        public string FieldInSQL
        {
            get
            {
                return _fieldInSQL;
            }
        }

        public string JoinConditions()
        {
            if (Conditions.Count == 0)
            {
                return "";
            }

            var authorLike = Conditions.Where(x => x.Operator == "=").ToList();
            var authorNotLike = Conditions.Where(x => x.Operator == "!=").ToList();
            string query = "(";

            if (authorNotLike.Any())
            {
                query = $"({FieldInSQL} NOT IN ('{authorNotLike[0].Value}'";
                for (int i = 1; i < authorNotLike.Count; i++)
                {
                    query += $", \"'{authorNotLike[i].Value}'\"";
                }
            }

            if (authorLike.Any())
            {
                if(authorNotLike.Any())
                {
                    query += ") AND ";
                }
                query = $"({FieldInSQL} IN ('{authorLike[0].Value}'";
                for (int i = 1; i < authorLike.Count; i++)
                {
                    query += $", \"'{authorLike[i].Value}'\"";
                }
            }

            return query + "))";
        }
    }
}
