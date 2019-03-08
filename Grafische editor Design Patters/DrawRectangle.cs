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
    class DrawRectangle : Command
    {
        Point start, end;
        Canvas MyCanvas;
        List<Figuren> Allfiguren;
        public DrawRectangle(Point s, Point e, Canvas c, List<Figuren> AF)
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
                newRectangle.SetValue(Canvas.TopProperty, start.Y - 50);
                newRectangle.SetValue(Canvas.BottomProperty, end.Y - 50);


                newRectangle.Height = end.Y - start.Y;
            }
            else
            {
                newRectangle.SetValue(Canvas.TopProperty, end.Y - 50);
                newRectangle.SetValue(Canvas.BottomProperty, start.Y - 50);
                newRectangle.Height = start.Y - end.Y;
            }
            Rechthoeken RectangleFiguren = new Rechthoeken(newRectangle);
            Allfiguren.Add(RectangleFiguren);
            MyCanvas.Children.Add(newRectangle);
        }
    }
}
