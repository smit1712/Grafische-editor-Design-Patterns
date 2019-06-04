using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Command voor tekeken van de Rechthoek
    /// Execute functie handelt alles voor het tekenen van de Rechthoek
    /// </summary>
    class DrawRectangle : ICommand
    {
        private Point start, end;
        private Canvas MyCanvas;
        private List<BasisFiguur> Allfiguren;
        public DrawRectangle(Point s, Point e, Canvas c, List<BasisFiguur> AF)
        {
            start = s;
            end = e;
            MyCanvas = c;
            Allfiguren = AF;
        }

        public void Execute()
        {
            Rectangle newRectangle = new Rectangle()
            {
                Stroke = Brushes.Green,
                Fill = Brushes.Red,
                StrokeThickness = 4,

            };

            if (end.X >= start.X)
            {
                newRectangle.SetValue(Canvas.LeftProperty, start.X);
                newRectangle.SetValue(Canvas.RightProperty, end.X);


                newRectangle.Width = end.X - start.X;
            }
            else
            {
                newRectangle.SetValue(Canvas.LeftProperty, end.X);
                newRectangle.SetValue(Canvas.RightProperty, start.X);
                newRectangle.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                newRectangle.SetValue(Canvas.TopProperty, start.Y);
                newRectangle.SetValue(Canvas.BottomProperty, end.Y);


                newRectangle.Height = end.Y - start.Y;
            }
            else
            {
                newRectangle.SetValue(Canvas.TopProperty, end.Y);
                newRectangle.SetValue(Canvas.BottomProperty, start.Y);
                newRectangle.Height = start.Y - end.Y;
            }
            BasisFiguur RectangleFiguren = new BasisFiguur(newRectangle, MyCanvas);

            Allfiguren.Add(RectangleFiguren);
            MyCanvas.Children.Add(newRectangle);
        }
    }
}
