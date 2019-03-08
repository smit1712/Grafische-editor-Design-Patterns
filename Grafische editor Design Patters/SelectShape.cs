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
    class SelectShape : Command
    {
        List<Figuren> SelectedFiguren;
        List<Figuren> AllFiguren;
        Point start, end;
        Border SelectBorder;
        public SelectShape(Point s, Point e, Canvas c, List<Figuren> AF,List<Figuren> SF, Border SB)
        {
            SelectedFiguren = SF;
            AllFiguren = AF;
            SelectBorder = SB;
        }

 
        
        private void SelectInBorder()
        {
            SelectedFiguren.Clear();

            foreach (Figuren F in AllFiguren)
            {
                F.Deselect();
                //F.UpdateXY(Canvas.GetLeft(F.GetShape()), Canvas.GetTop(F.GetShape()));
                if (F.left > Canvas.GetLeft(SelectBorder) && F.right < Canvas.GetRight(SelectBorder) && F.top > Canvas.GetTop(SelectBorder) && F.bot < Canvas.GetBottom(SelectBorder))
                {
                    SelectedFiguren.Add(F);
                    F.Select();
                }
            }
        }

        public void Execute()
        {
            if (end.X >= start.X)
            {
                SelectBorder.SetValue(Canvas.LeftProperty, start.X);
                SelectBorder.Width = end.X - start.X;
            }
            else
            {
                SelectBorder.SetValue(Canvas.LeftProperty, end.X);
                SelectBorder.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                SelectBorder.SetValue(Canvas.TopProperty, start.Y - 50);
                SelectBorder.Height = end.Y - start.Y;
            }
            else
            {
                SelectBorder.SetValue(Canvas.TopProperty, end.Y - 50);
                SelectBorder.Height = start.Y - end.Y;
            }
            SelectInBorder();
        }
    }
}
