using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// GroupIn Command
    /// Execute functie handelt alles voor het Degrouperen van alle selecteerde figuren
    /// </summary>
    class GroupOut : ICommand
    {
        readonly List<BasisFiguur> SelectedFiguren;
        readonly List<BasisFiguur> AllFiguren;
        Point start, end;
        Border GroupBorder;
        public GroupOut(Point s, Point e, Canvas c, List<BasisFiguur> SF, List<BasisFiguur> AF, Border GB)
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

            foreach (BasisFiguur F in AllFiguren)
            {
                if (F.figuur.left > start.X && F.figuur.left < end.X && F.figuur.top > start.Y && F.figuur.top < end.Y && SelectedFiguren.Count() != 0)
                {
                    SelectedFiguren[0].figuur.RemoveFromGroep(F.figuur);
                }
            }
        }
    }
}
