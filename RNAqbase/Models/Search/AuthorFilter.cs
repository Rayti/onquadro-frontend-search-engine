using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNAqbase.Models.Search
{
    public class AuthorFilter : Filter
    {
        public new readonly string FieldInSQL = "Author";
        public override List<Condition> Conditions { get; set; } = new List<Condition>();
        
        public AuthorFilter()
        {
            Conditions.Add(new Condition("=", "A"));
            Conditions.Add(new Condition("!=", "B"));
            Conditions.Add(new Condition("=", "C"));
            Conditions.Add(new Condition("=", "D"));
            Conditions.Add(new Condition("!=", "E"));
         }

        public override string JoinConditions()
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
                query += $"{FieldInSQL} NOT IN ('{authorNotLike[0].Value}'";
                for (int i = 1; i < authorNotLike.Count; i++)
                {
                    query += $", '{authorNotLike[i].Value}'";
                }
            }

            if (authorLike.Any())
            {
                if(authorNotLike.Any())
                {
                    query += ") AND ";
                }

                query += $"{FieldInSQL} IN ('{authorLike[0].Value}'";
                for (int i = 1; i < authorLike.Count; i++)
                {
                    query += $", \"'{authorLike[i].Value}'\"";
                }
            }

            return query + "))";
        }
    }
}
