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
    class groupIn : Command
    {
        List<Figuren> SelectedFiguren;
        List<Figuren> AllFiguren;
        Point start, end;
        Border GroupBorder;
        public groupIn(Point s, Point e, Canvas c, List<Figuren> SF, List<Figuren> AF, Border GB)
        {
            SelectedFiguren = SF;
            AllFiguren = AF;
            start = s;
            end = e;
            GroupBorder = GB;
        }

        public void Execute()
        {
            if (end.X >= start.X)
            {
                GroupBorder.SetValue(Canvas.LeftProperty, start.X);
                GroupBorder.Width = end.X - start.X;
            }
            else
            {
                GroupBorder.SetValue(Canvas.LeftProperty, end.X);
                GroupBorder.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                GroupBorder.SetValue(Canvas.TopProperty, start.Y - 50);
                GroupBorder.Height = end.Y - start.Y;
            }
            else
            {
                GroupBorder.SetValue(Canvas.TopProperty, end.Y - 50);
                GroupBorder.Height = start.Y - end.Y;
            }

            foreach (Figuren F in AllFiguren)
            {
                if (F.left > start.X && F.left < end.X && F.top > start.Y && F.top < end.Y && SelectedFiguren.Count() != 0)
                {
                    SelectedFiguren[0].InsertGroep(F);
                }
            }
        }
    }
}
