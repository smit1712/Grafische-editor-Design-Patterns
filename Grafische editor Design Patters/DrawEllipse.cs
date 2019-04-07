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
    class DrawEllipse : Command
    {
        Point start, end;
        Canvas MyCanvas;
        List<Figuren> Allfiguren;
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
                newEllipse.SetValue(Canvas.TopProperty, start.Y - 50);
                newEllipse.SetValue(Canvas.BottomProperty, end.Y - 50);


                newEllipse.Height = end.Y - start.Y;
            }
            else
            {
                newEllipse.SetValue(Canvas.TopProperty, end.Y - 50);
                newEllipse.SetValue(Canvas.BottomProperty, start.Y - 50);
                newEllipse.Height = start.Y - end.Y;
            }
            Ellipsen ELlipsenFiguren = new Ellipsen(newEllipse, MyCanvas);
            Allfiguren.Add(ELlipsenFiguren);
            MyCanvas.Children.Add(newEllipse);
        }
    }
}
