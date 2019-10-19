using Grafika_Komputerowa1.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Models
{
    public class Relation
    {
        public Edge edge1 { get; set; }
        public Edge edge2 { get; set; }
        public RelationEnum relation { get; set; }

        public Relation(Edge e1, Edge e2, RelationEnum rel)
        {
            edge1 = e1;
            edge2 = e2;
            relation = rel;
        }
    }
}
