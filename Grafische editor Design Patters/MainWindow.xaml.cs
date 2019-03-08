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
        private Invoker invoker = new Invoker();

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
                    invoker.ellipse(start,end,MyCanvas,AllFiguren);
                    break;
                case MyShape.Rectangle:
                    invoker.rectangle(start, end, MyCanvas, AllFiguren);
                    break;
                case MyShape.SelectBox:
                    invoker.SelectShape(start, end, MyCanvas, AllFiguren,SelectBorder);
                    break;
                case MyShape.Move:
                    invoker.moveShape(start, end, MyCanvas, AllFiguren);
                    break;
                case MyShape.Resize:
                    invoker.ResizeShape(start, end, MyCanvas);
                    break;
                case MyShape.Group:
                    invoker.groupIn(start, end, MyCanvas, AllFiguren, GroupBorder);
                    break;
                case MyShape.DeGroup:
                    invoker.groupOut(start, end, MyCanvas, AllFiguren, GroupBorder);
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
                if (currShape == MyShape.SelectBox)
                {
                    double moveX = end.X - start.X;
                    double moveY = end.Y - start.Y;
                    Canvas.SetLeft(SelectBorder, start.X);
                    Canvas.SetTop(SelectBorder, start.Y);
                    Canvas.SetRight(SelectBorder, end.X);
                    Canvas.SetBottom(SelectBorder, end.Y);
                }
                if (currShape == MyShape.Group || currShape == MyShape.DeGroup)
                {
                    double moveX = end.X - start.X;
                    double moveY = end.Y - start.Y;
                    Canvas.SetLeft(GroupBorder, start.X);
                    Canvas.SetTop(GroupBorder, start.Y);
                    Canvas.SetRight(GroupBorder, end.X);
                    Canvas.SetBottom(GroupBorder, end.Y);
                    //if (currShape == MyShape.Group)
                    //    //Group();
                    //else
                    //    DeGroup();
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
