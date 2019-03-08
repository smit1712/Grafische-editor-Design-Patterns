using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Grafische_editor_Design_Patters
{
    class MoveShape : Command
    {
        Point start, end;
        Canvas MyCanvas;
        List<Figuren> Allfiguren;
        private List<Figuren> SelectedFiguren;


        public MoveShape(Point s, Point e, Canvas c, List<Figuren> AF, List<Figuren> SF)
        {
            start = s;
            end = e;
            MyCanvas = c;
            Allfiguren = AF;
            SelectedFiguren = SF;

        }

        public void Execute()
        {
            double moveX = end.X - start.X;
            double moveY = end.Y - start.Y;
            

            foreach (Figuren F in SelectedFiguren)
            {
                if (!SelectedFiguren.Contains(F.Parent))
                    F.Move(moveX, moveY);

            }
        }
    }
}
