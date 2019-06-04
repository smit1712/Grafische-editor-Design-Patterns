using System.Collections.Generic;
using System.Windows;
namespace Grafische_editor_Design_Patters
{

    class MoveShape : IVisitor
    {
        Point end, start;
        List<Figuren> SelectedFiguren;
        public MoveShape(Point s, Point e, List<Figuren> SF)
        {
            start = s;
            end = e;
            SelectedFiguren = SF;
        }
        public void Visit(Figuren F)
        {
            double moveX = end.X - start.X;
            double moveY = end.Y - start.Y;

            if (!SelectedFiguren.Contains(F.Parent))
                F.Move(moveX, moveY);

        }
    }
}
