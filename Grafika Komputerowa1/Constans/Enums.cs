using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Constans
{
    public enum RelationEnum
    {
        None,
        Equal,
        Perpendicular
    }

    public enum ToolStripChoice
    {
        DrawFigure,
        MoveVertice,
        MoveEdge,
        MoveFigure,
        RemoveFigure,
        AddRelation,
        RemoveRelation,
        AddPoint,
        RemovePoint
    }

    public enum StylePainting
    {
        Brezenham,
        DrawLine,
        WU,
        Symmetric
    }
}
