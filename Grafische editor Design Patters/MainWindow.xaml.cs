using System;
using System.Collections.Generic;
using System.IO;
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
            Line, Ellipse, Rectangle, SelectBox, Move, Resize, Group,DeGroup
        }
        static Point start;
        static Point end;        
        private static List<Figuren> AllFiguren = new List<Figuren>();
        private MyShape currShape = MyShape.SelectBox;
        private static List<Figuren> SelectedFiguren = new List<Figuren>();
        private Invoker invoker = new Invoker();
        private VisitExecutor Executor;


        Border SelectBorder = new Border() //selectborder definition
        {
            BorderBrush = Brushes.Gray,
            BorderThickness = new Thickness(1),
            Padding = new Thickness(5),
            
        };
        Border GroupBorder = new Border() //Group definition
        {
            BorderBrush = Brushes.Red,
            BorderThickness = new Thickness(2),
            Padding = new Thickness(10),
        };

        public MainWindow()
        {
            InitializeComponent();
            ResetCanvas();
            Executor = new VisitExecutor(ref AllFiguren, ref SelectedFiguren, start, end,ref MyCanvas);
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
        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Group;
        }
        private void DeGroupButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.DeGroup;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Executor.Visit(new Save());
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            ResetCanvas();
            Executor.Visit(new Load());
        }
        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Undo(MyCanvas,AllFiguren);
        }
        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Redo();
        }
        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(this);
            start.Y -= 50;
            if (currShape == MyShape.SelectBox)
                SelectBorder.Visibility = Visibility.Visible;
            else
                SelectBorder.Visibility = Visibility.Hidden;


            if (currShape == MyShape.Group || currShape == MyShape.DeGroup)
                GroupBorder.Visibility = Visibility.Visible;
            else
                GroupBorder.Visibility = Visibility.Hidden;

        }
        private void MyCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (currShape == MyShape.SelectBox)
                SelectBorder.Visibility = Visibility.Visible;


            if (currShape == MyShape.Group || currShape == MyShape.DeGroup)
                GroupBorder.Visibility = Visibility.Visible;

            switch (currShape)
            {               
                case MyShape.Ellipse:
                    invoker.ellipse(start,end,MyCanvas,AllFiguren);
                    break;
                case MyShape.Rectangle:
                    invoker.rectangle(start, end, MyCanvas, AllFiguren);
                    break;
                case MyShape.SelectBox:
                    invoker.SelectShape(start, end, MyCanvas, AllFiguren,ref SelectedFiguren,SelectBorder);
                    break;
                case MyShape.Move:
                    Executor.RefreshPoints(start, end);
                    Executor.Visit(new MoveShape());
                    break;
                case MyShape.Resize:
                    Executor.RefreshPoints(start, end);
                    Executor.Visit(new ResizeShape());
                    break;
                case MyShape.Group:
                    invoker.groupIn(start, end, MyCanvas, AllFiguren, ref SelectedFiguren, GroupBorder);
                    break;
                case MyShape.DeGroup:
                    invoker.groupOut(start, end, MyCanvas, AllFiguren, ref SelectedFiguren, GroupBorder);
                    break;
                default:
                    return;
            }
            invoker.ExecuteCommands();
        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                end = e.GetPosition(this);
                end.Y -= 50;
                double moveX = end.X - start.X;
                double moveY = end.Y - start.Y;
                if (currShape == MyShape.SelectBox)
                {
                    Canvas.SetLeft(SelectBorder, start.X);
                    Canvas.SetTop(SelectBorder, start.Y);
                    Canvas.SetRight(SelectBorder, end.X);
                    Canvas.SetBottom(SelectBorder, end.Y);
                    if (moveX > 0 && moveY > 0)
                    {
                        SelectBorder.Width = moveX;
                        SelectBorder.Height = moveY;
                    }
                }
                if (currShape == MyShape.Group || currShape == MyShape.DeGroup)
                {      
                    Canvas.SetLeft(GroupBorder, start.X);
                    Canvas.SetTop(GroupBorder, start.Y);
                    Canvas.SetRight(GroupBorder, end.X);
                    Canvas.SetBottom(GroupBorder, end.Y);
                    if (moveX > 0 && moveY > 0)
                    {
                        GroupBorder.Width = moveX;
                        GroupBorder.Height = moveY;
                    }
                }
            }
            else
            {
                SelectBorder.Visibility = Visibility.Hidden;
                GroupBorder.Visibility = Visibility.Hidden;
            }

        }

      
       
        private void ResetCanvas()
        {
            MyCanvas.Children.Clear();
            AllFiguren.Clear();       
            MyCanvas.Children.Add(SelectBorder);//add selectborder to canvas
            MyCanvas.Children.Add(GroupBorder);//add selectborder to canvas
            Canvas.SetLeft(SelectBorder, -100);
            Canvas.SetLeft(GroupBorder, -100);
        }

    }
}
