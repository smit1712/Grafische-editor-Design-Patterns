using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private enum MyShape
        {
            Line, Ellipse, Rectangle, SelectBox, Move, Resize
        }
        Point start;
        Point end;
        private List<Figuren> AllFiguren = new List<Figuren>();
        private MyShape currShape = MyShape.SelectBox;
        private List<Figuren> SelectedFiguren = new List<Figuren>();
        Border SelectBorder = new Border() //selectborder definition
        {
            BorderBrush = Brushes.Gray,
            BorderThickness = new Thickness(1),
            Padding = new Thickness(5),
        };

        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Children.Add(SelectBorder);//add selectborder to canvas

        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Line;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Ellipse;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Rectangle;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.SelectBox;
        }
        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Move;
        }
        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Resize;
        }
        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(this);
            if (currShape == MyShape.SelectBox)
                SelectBorder.Visibility = Visibility.Visible;
            else
                SelectBorder.Visibility = Visibility.Hidden;

        }
        private void MyCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (currShape == MyShape.SelectBox)
                SelectBorder.Visibility = Visibility.Visible;

            switch (currShape)
            {
                case MyShape.Line:
                    DrawLine();
                    break;
                case MyShape.Ellipse:
                    DrawEllipse();
                    break;
                case MyShape.Rectangle:
                    DrawRectangle();
                    break;
                case MyShape.SelectBox:
                    SelectShape();
                    break;
                case MyShape.Move:
                    MoveShape();
                    break;
                case MyShape.Resize:
                    ResizeShape();
                    break;
                default:
                    return;
            }
        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                end = e.GetPosition(this);
                if (currShape == MyShape.SelectBox)
                {
                    double moveX = end.X - start.X;
                    double moveY = end.Y - start.Y;
                    Canvas.SetLeft(SelectBorder, start.X);
                    Canvas.SetTop(SelectBorder, start.Y);
                    Canvas.SetRight(SelectBorder, end.X);
                    Canvas.SetBottom(SelectBorder, end.Y);
                    SelectShape();
                }
            }

        }

        private void DrawLine()
        {
            Line newLine = new Line()
            {
                Stroke = Brushes.Blue,
                X1 = start.X,
                Y1 = start.Y - 50,
                X2 = end.X,
                Y2 = end.Y - 50
            };
            Lijn ELlipsenFiguren = new Lijn(newLine);
            AllFiguren.Add(ELlipsenFiguren);

            MyCanvas.Children.Add(newLine);
        }

        private void DrawEllipse()
        {
            Ellipse newEllipse = new Ellipse()
            {
                Stroke = Brushes.Green,
                Fill = Brushes.Red,
                StrokeThickness = 4,
                Height = 10,
                Width = 10          
            };
            Ellipsen ELlipsenFiguren = new Ellipsen(newEllipse);
            AllFiguren.Add(ELlipsenFiguren);
            if(end.X >= start.X)
            {
                newEllipse.SetValue(Canvas.LeftProperty, start.X);
                newEllipse.Width = end.X - start.X;
            } else
            {
                newEllipse.SetValue(Canvas.LeftProperty, end.X);
                newEllipse.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                newEllipse.SetValue(Canvas.TopProperty, start.Y - 50);
                newEllipse.Height = end.Y - start.Y;
            }
            else
            {
                newEllipse.SetValue(Canvas.TopProperty, end.Y - 50);
                newEllipse.Height = start.Y - end.Y;
            }

            MyCanvas.Children.Add(newEllipse);
        }

        private void DrawRectangle()
        {
            Rectangle newRectangle = new Rectangle()
            {
                Stroke = Brushes.Green,
                Fill = Brushes.Red,
                StrokeThickness = 4,
            };
            Rechthoeken ELlipsenFiguren = new Rechthoeken(newRectangle);
            AllFiguren.Add(ELlipsenFiguren);

            if (end.X >= start.X)
            {
                newRectangle.SetValue(Canvas.LeftProperty, start.X);
                newRectangle.Width = end.X - start.X;
            }
            else
            {
                newRectangle.SetValue(Canvas.LeftProperty, end.X);
                newRectangle.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                newRectangle.SetValue(Canvas.TopProperty, start.Y - 50);
                newRectangle.Height = end.Y - start.Y;
            }
            else
            {
                newRectangle.SetValue(Canvas.TopProperty, end.Y - 50);
                newRectangle.Height = start.Y - end.Y;
            }

            MyCanvas.Children.Add(newRectangle);
        }
        private void SelectShape()
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
        private void SelectInBorder()
        {
            SelectedFiguren.Clear();
            foreach (Figuren F in AllFiguren)
            {
                F.Deslelect();
                F.UpdateXY(Canvas.GetLeft(F.GetShape()), Canvas.GetTop(F.GetShape()));
                if (F.X > start.X && F.X < end.X && F.Y > start.Y && F.Y < end.Y)
                {
                    SelectedFiguren.Add(F);
                    F.Select();
                }
            }
        }
       public void MoveShape()
       {
            double moveX = end.X - start.X;
            double moveY = end.Y - start.Y;
            Canvas.SetLeft(SelectBorder, Canvas.GetLeft(SelectBorder) + moveX);
            Canvas.SetTop(SelectBorder, Canvas.GetTop(SelectBorder) + moveY);

            foreach (Figuren F in SelectedFiguren)
            {
                F.Move(moveX, moveY);

            }
        }

        public void ResizeShape()
        {
            double moveX = end.X - start.X;
            double moveY = end.Y - start.Y;

            if (moveX >= 0 && moveY >= 0)
            {
                foreach (Figuren F in SelectedFiguren)
                {
                    F.Resize(moveX, moveY);
                }
            }
        }        
    }
}
