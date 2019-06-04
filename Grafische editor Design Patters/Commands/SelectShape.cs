using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Command voor Selecteren van Figuren
    /// Execute vult de selectedFiguren List met alle objecten in de selectborder
    /// </summary>
    class SelectShape : ICommand
    {
        private List<BasisFiguur> SelectedFiguren;
        private readonly List<BasisFiguur> AllFiguren;
        private Point start, end;
        private Border SelectBorder;
        public SelectShape(Point s, Point e, Canvas c, List<BasisFiguur> AF, ref List<BasisFiguur> SF, Border SB)
        {
            SelectedFiguren = SF;
            AllFiguren = AF;
            SelectBorder = SB;
            start = s;
            end = e;
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
            SelectedFiguren.Clear();
            foreach (BasisFiguur F in AllFiguren)
            {

                double LeftBorder = Canvas.GetLeft(SelectBorder);
                double RightBorder = Canvas.GetRight(SelectBorder);
                double TopBorder = Canvas.GetTop(SelectBorder);
                double BotBorder = Canvas.GetBottom(SelectBorder);
                F.figuur.Deselect();
                if (F.figuur.left > start.X && F.figuur.right < end.X && F.figuur.top > start.Y && F.figuur.bot < end.Y)
                {
                    SelectedFiguren.Add(F);
                    F.figuur.Select();
                }
            }
        }
    }
}
