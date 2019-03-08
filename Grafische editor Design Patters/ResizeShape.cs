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
    class ResizeShape : Command
    {


        List<Figuren> SelectedFiguren;
        Point start, end;

        public ResizeShape(Point s, Point e, Canvas c, List<Figuren> SF)
        {
            SelectedFiguren = SF;
            start = s;
            end = e;
        }

        public void Execute()
        {
            foreach (Figuren F in SelectedFiguren)
            {
                F.Resize(start, end);
            }
        }
    }
}
