using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Komputerowa1.Extentions
{
    public static class MouseEventArgsExtentions
    {
        public static bool IsPoint(this MouseEventArgs mouseEventArgs, CollectionFigure collection)
        {
            Vertice p = new Vertice(mouseEventArgs.X, mouseEventArgs.Y);
            foreach(var fig in collection.figures)
            {
                if(fig.IsPoint(p))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
