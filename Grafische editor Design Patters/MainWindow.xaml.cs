using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Grafische_editor_Design_Patters
{

    public partial class MainWindow : Window
    {
        //Enum shapes voor de verschillende figuren en het beinvloeden van het Canvas
        private enum MyShape
        {
            Line, Ellipse, Rectangle, SelectBox, Move, Resize, Group, DeGroup
        }
        private MyShape currShape = MyShape.SelectBox;
        //2 points voor het vastleggen van de muis beweging
        static Point start;
        static Point end;

        private static List<Figuren> AllFiguren = new List<Figuren>();
        private static List<Figuren> SelectedFiguren = new List<Figuren>();

        //invoker en visitor nodig voor het command en visitor pattern
        private Invoker invoker = new Invoker();
        private Visitor visitor;
        //default OrnamentLocatie
        private string OrnamentLocation = "Top";

        public MainWindow()
        {
            InitializeComponent();
            ResetCanvas();
            visitor = new Visitor(ref AllFiguren, ref SelectedFiguren, start, end, ref MyCanvas);
        }
        //Verschillende _Click events van het menu.
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
        private void AddOrnament_Click(object sender, RoutedEventArgs e)
        {
            OrnamentTextBox.Visibility = Visibility.Visible;
            OrnamentLocationBox.Visibility = Visibility.Visible;
            OrnamentTextBox.Focus();
        }
        private void OrnamentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                invoker.AddOrnament(ref SelectedFiguren, OrnamentTextBox.Text, OrnamentLocation);
                invoker.ExecuteCommands();
                OrnamentTextBox.Visibility = Visibility.Hidden;
                OrnamentLocationBox.Visibility = Visibility.Hidden;
            }
        }
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            OrnamentLocation = radioButton.Content.ToString();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            visitor.Visit(new Save());

        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            ResetCanvas();
            visitor.Visit(new Load());
        }
        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Undo(MyCanvas, AllFiguren, SelectBorder,GroupBorder);
        }
        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            invoker.Redo();
        }
        //Mous events voeren verschillende acties uit op basis van de geselecteerde myshape enum
        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(this);
            end = start;
            start.Y -= 91;//91 aanpassing inverband met grote van menu
            end.Y -= 91;
            if (currShape == MyShape.SelectBox)
            {
                SelectBorder.Visibility = Visibility.Visible;
                Canvas.SetLeft(SelectBorder, start.X);
                Canvas.SetBottom(SelectBorder, end.Y);                
            }
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
            {
                SelectBorder.Visibility = Visibility.Hidden;
                SelectBorder.Width = 0;
                SelectBorder.Height = 0;
            }
            if (currShape == MyShape.Group || currShape == MyShape.DeGroup)
            {
                GroupBorder.Visibility = Visibility.Hidden;
                GroupBorder.Width = 0;
                GroupBorder.Height = 0;
            }

            switch (currShape)
            {
                case MyShape.Ellipse:
                    invoker.ellipse(start, end, MyCanvas, AllFiguren);
                    break;
                case MyShape.Rectangle:
                    invoker.rectangle(start, end, MyCanvas, AllFiguren);
                    break;
                case MyShape.SelectBox:
                    invoker.SelectShape(start, end, MyCanvas, AllFiguren, ref SelectedFiguren, SelectBorder);
                    break;
                case MyShape.Move:
                    visitor.RefreshPoints(start, end);
                    visitor.Visit(new MoveShape());
                    break;
                case MyShape.Resize:
                    visitor.RefreshPoints(start, end);
                    visitor.Visit(new ResizeShape());
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
            //ExecuteCommands voor het command uit dat bij de voorgaande switchcase gemaakt wordt
            invoker.ExecuteCommands();
            start = new Point();
            end = new Point();
            

        }
        //Indien select of group gebruikt wordt, verplaatst en hij de bijbehoorende border. En past bij mousedown altijd de end point aan
        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                end = e.GetPosition(this);
                end.Y -= 91;
                double moveX = end.X - start.X;
                double moveY = end.Y - start.Y;
                if (currShape == MyShape.SelectBox)
                {
                    SelectBorder.Visibility = Visibility.Visible;
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

        Border SelectBorder = new Border() //selectborder definition
        {
            BorderBrush = Brushes.Gray,
            BorderThickness = new Thickness(1),
            Padding = new Thickness(5),

        };
        Border GroupBorder = new Border() //Groupborder definition
        {
            BorderBrush = Brushes.Red,
            BorderThickness = new Thickness(2),
            Padding = new Thickness(10),
        };
        //Reset het canvas en alle figuren.
        private void ResetCanvas()
        {
            MyCanvas.Children.Clear();
            AllFiguren.Clear();
            MyCanvas.Children.Add(SelectBorder);
            MyCanvas.Children.Add(GroupBorder);
            Canvas.SetLeft(SelectBorder, -100);
            Canvas.SetLeft(GroupBorder, -100);
        }

    }
}
