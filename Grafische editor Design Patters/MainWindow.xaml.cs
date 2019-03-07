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
        Point start;
        Point end;
        private List<Figuren> AllFiguren = new List<Figuren>();
        private MyShape currShape = MyShape.SelectBox;
        private List<Figuren> SelectedFiguren = new List<Figuren>();
        private int readline = 0;

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
            Save();
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }


        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(this);
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
                case MyShape.Group:
                    Group();
                    break;
                case MyShape.DeGroup:
                    DeGroup();
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
                if (currShape == MyShape.Group || currShape == MyShape.DeGroup)
                {
                    double moveX = end.X - start.X;
                    double moveY = end.Y - start.Y;
                    Canvas.SetLeft(GroupBorder, start.X);
                    Canvas.SetTop(GroupBorder, start.Y);
                    Canvas.SetRight(GroupBorder, end.X);
                    Canvas.SetBottom(GroupBorder, end.Y);
                    if (currShape == MyShape.Group)
                        Group();
                    else
                        DeGroup();
                }
            }

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
          
            if(end.X >= start.X)
            {
                newEllipse.SetValue(Canvas.LeftProperty, start.X);
                newEllipse.SetValue(Canvas.RightProperty, end.X);


                newEllipse.Width = end.X - start.X;
            } else
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
            Ellipsen ELlipsenFiguren = new Ellipsen(newEllipse);
            AllFiguren.Add(ELlipsenFiguren);
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
            AllFiguren.Add(RectangleFiguren);
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
                F.Deselect();
                //F.UpdateXY(Canvas.GetLeft(F.GetShape()), Canvas.GetTop(F.GetShape()));
                if (F.left > Canvas.GetLeft(SelectBorder) && F.right < Canvas.GetRight(SelectBorder) && F.top > Canvas.GetTop(SelectBorder) && F.bot < Canvas.GetBottom(SelectBorder))
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
            Canvas.SetRight(SelectBorder, Canvas.GetRight(SelectBorder) + moveX);
            Canvas.SetBottom(SelectBorder, Canvas.GetBottom(SelectBorder) + moveY);

            foreach (Figuren F in SelectedFiguren)
            {
                if(!SelectedFiguren.Contains(F.Parent))
                F.Move(moveX, moveY);

            }
        }

        public void ResizeShape()
        {
                foreach (Figuren F in SelectedFiguren)
                {
                    MyCanvas.Children.Remove(F.GetShape());
                    F.Resize(start, end);
                    MyCanvas.Children.Add(F.GetShape());
                }            
        }
        public void Group()
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

            foreach (Figuren F in AllFiguren)
            {
                if (F.left > start.X && F.left < end.X && F.top > start.Y && F.top < end.Y && SelectedFiguren[0] != null)
                {
                    SelectedFiguren[0].InsertGroep(F);
                }
            }

        }
        public void DeGroup()
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

            foreach (Figuren F in AllFiguren)
            {
                if (F.left > start.X && F.left < end.X && F.top > start.Y && F.top < end.Y && SelectedFiguren[0] != null)
                {
                    SelectedFiguren[0].RemoveFromGroep(F);
                }
            }
        }
        public void Load()
        {
            ResetCanvas();
            StreamReader sr = new StreamReader(@"C:/GrafischeEditor/Save.txt");
            List<string> read = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
               line = line.Replace("-", string.Empty);
                read.Add(line);
            }
            string[] result = read.ToArray();
            for (readline = 0; readline < result.Length; readline++)
            {
                Figuren child = LoadFig(result);        

            }
            sr.Close();
        }

        public Figuren LoadFig(string[] result)
        {
            string[] position, Gsize;
            int left, top, right, bot, numChildren;
            switch (result[readline])
                  {
                case "Rechthoek":
                    Gsize = result[readline - 1].Split(':');
                    numChildren = Convert.ToInt16(Gsize[1]);
                    position = result[readline + 1].Split(' ');
                    left = Convert.ToInt16(position[0]);
                    top = Convert.ToInt16(position[1]);
                    right = Convert.ToInt16(position[2]);
                    bot = Convert.ToInt16(position[3]);
                    Rectangle newRectangle = new Rectangle()
                    {
                        Stroke = Brushes.Green,
                        Fill = Brushes.Red,
                        StrokeThickness = 4,
                        Width = right - left,
                        Height = bot - top,
                    };
                    MyCanvas.Children.Add(newRectangle);
                    Rechthoeken RectangleFiguren = new Rechthoeken(newRectangle);
                    RectangleFiguren.SetPosition(top, left, bot, right);
                    for (int c = 0; c < numChildren; c++)
                    {
                        readline += 3;
                        Figuren child = LoadFig(result);
                        int childgroupsize = 0;
                        if (child != null)
                        {
                            RectangleFiguren.InsertGroep(child);
                            childgroupsize = child.GetGroupSize();
                        }
                    }
                    AllFiguren.Add(RectangleFiguren);
                    return RectangleFiguren;
                    
                    
                case "Ellipse":
                    Gsize = result[readline - 1].Split(':');
                    numChildren = Convert.ToInt16(Gsize[1]);
                    position = result[readline + 1].Split(' ');
                    left = Convert.ToInt16(position[0]);
                    top = Convert.ToInt16(position[1]);
                    right = Convert.ToInt16(position[2]);
                    bot = Convert.ToInt16(position[3]);

                    Ellipse newEllipse = new Ellipse()
                    {
                        Stroke = Brushes.Green,
                        Fill = Brushes.Red,
                        StrokeThickness = 4,
                        Width = right - left,
                        Height = bot - top,
                    };
                    MyCanvas.Children.Add(newEllipse);
                    Ellipsen ELlipsenFiguren = new Ellipsen(newEllipse);
                    ELlipsenFiguren.SetPosition(top, left, bot, right);
                    for (int c = 0; c < numChildren; c++)
                    {
                        readline += 3;
                        Figuren child = LoadFig(result);
                        int childgroupsize = 0;
                        if (child != null)
                        {
                            ELlipsenFiguren.InsertGroep(child);
                            childgroupsize = child.GetGroupSize();
                        }
                    }
                    AllFiguren.Add(ELlipsenFiguren);
                    return ELlipsenFiguren;
            }        
            return null;
        }

        public void Save()
        {
            StreamWriter sw = new StreamWriter(@"C:/GrafischeEditor/Save.txt");
            int RecusionLevel = 1;
            foreach(Figuren F in AllFiguren)
            {
                if(F.Isingroup == false)
                {
                    sw.WriteLine("Groep:" + F.GetGroep().Count().ToString());
                    sw.WriteLine(F.type);
                    sw.WriteLine(Canvas.GetLeft(F.GetShape()) +" "+ Canvas.GetTop(F.GetShape()) + " " + Canvas.GetRight(F.GetShape()) + " " + Canvas.GetBottom(F.GetShape()));
                    SaveChild(F, sw,RecusionLevel);
                }
            }
            sw.Close();
        }
        private void SaveChild(Figuren F, StreamWriter sw, int Reclvl)
        {
            foreach (Figuren fig in F.GetGroep())
            {
                for (int i = 0; i < Reclvl; i++)
                {
                    sw.Write("-");
                }
                sw.WriteLine("Groep: " + fig.GetGroep().Count().ToString() +" ");
                for (int i = 0; i < Reclvl; i++)
                {
                    sw.Write("-");
                }
                sw.WriteLine(fig.type);
                for (int i = 0; i < Reclvl; i++)
                {
                    sw.Write("-");
                }
                sw.WriteLine(Canvas.GetLeft(fig.GetShape()) + " " + Canvas.GetTop(fig.GetShape()) + " " + Canvas.GetRight(fig.GetShape()) + " " + Canvas.GetBottom(fig.GetShape()));
                Reclvl++;
                if (fig.GetGroep().Count != 0)
                SaveChild(fig, sw,Reclvl);
            }
        }
        private void ResetCanvas()
        {
            MyCanvas.Children.Clear();
            AllFiguren.Clear();       
            MyCanvas.Children.Add(SelectBorder);//add selectborder to canvas
            MyCanvas.Children.Add(GroupBorder);//add selectborder to canvas
            readline = 0;
        }

    }
}
