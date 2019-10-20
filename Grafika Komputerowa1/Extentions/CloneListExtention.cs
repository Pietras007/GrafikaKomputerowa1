using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Extentions
{
    public static class CloneListExtention
    {
        public static List<Vertice> Clone(this List<Vertice> vertices)
        {
            List<Vertice> clonedList = new List<Vertice>();
            foreach(var v in vertices)
            {
                clonedList.Add(new Vertice(v.x, v.y));
            }
            return clonedList;
        }
    }
}
