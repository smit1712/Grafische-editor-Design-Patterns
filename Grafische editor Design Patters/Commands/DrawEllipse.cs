using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Command voor tekeken van de Ellipse
    /// Execute functie handelt alles voor het tekenen van de Ellipse
    /// </summary>
    class DrawEllipse : ICommand
    {
        private Point start, end;
        private Canvas MyCanvas;
        private List<Figuren> Allfiguren;
        public DrawEllipse(Point s, Point e, Canvas c, List<Figuren> AF)
        {
            start = s;
            end = e;
            MyCanvas = c;
            Allfiguren = AF;
        }

        public void Execute()
        {
            Ellipse newEllipse = new Ellipse()
            {
                Stroke = Brushes.Green,
                Fill = Brushes.Red,
                StrokeThickness = 4,
                Height = 10,
                Width = 10
            };

            if (end.X >= start.X)
            {
                newEllipse.SetValue(Canvas.LeftProperty, start.X);
                newEllipse.SetValue(Canvas.RightProperty, end.X);


                newEllipse.Width = end.X - start.X;
            }
            else
            {
                newEllipse.SetValue(Canvas.LeftProperty, end.X);
                newEllipse.SetValue(Canvas.RightProperty, start.X);
                newEllipse.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                newEllipse.SetValue(Canvas.TopProperty, start.Y);
                newEllipse.SetValue(Canvas.BottomProperty, end.Y);


                newEllipse.Height = end.Y - start.Y;
            }
            else
            {
                newEllipse.SetValue(Canvas.TopProperty, end.Y);
                newEllipse.SetValue(Canvas.BottomProperty, start.Y);
                newEllipse.Height = start.Y - end.Y;
            }
            Figuren ELlipsenFiguren = new Figuren(newEllipse, "Ellipse", MyCanvas);
            Allfiguren.Add(ELlipsenFiguren);
            //MyCanvas.Children.Add(newEllipse);
        }
    }
}
