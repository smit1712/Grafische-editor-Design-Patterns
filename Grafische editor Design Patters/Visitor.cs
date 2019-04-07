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
    class Visitor : IVisitor
    {
        private List<Figuren> AllFiguren;
        private List<Figuren> SelectedFiguren;
        private Point start, end;
        private int readline = 0;
        private Canvas MyCanvas;
        private Invoker commandinvoker = new Invoker();
        public Visitor(ref List<Figuren> AF,ref List<Figuren> SF, Point S, Point E, ref Canvas MyC)
        {
            AllFiguren = AF;
            SelectedFiguren = SF;
            start = S;
            end = E;
            MyCanvas = MyC;
        }
        public void RefreshPoints(Point S, Point E)
        {
            start = S;
            end = E;
        }
        public void Visit(Save S)
        {           
                StreamWriter sw = new StreamWriter(@"C:/GrafischeEditor/Save.txt");
                int RecusionLevel = 1;
                foreach (Figuren F in AllFiguren)
                {
                    if (F.Isingroup == false)
                    {
                        sw.WriteLine("Groep:" + F.GetGroep().Count().ToString());
                        foreach(Ornament OR in F.GetOrnament())
                        {
                        sw.WriteLine("ornament " + OR.GetLocation() + " " + OR.GetText() + " ");
                        }
                        sw.WriteLine(F.type);
                        sw.WriteLine(Canvas.GetLeft(F.GetShape()) + " " + Canvas.GetTop(F.GetShape()) + " " + Canvas.GetRight(F.GetShape()) + " " + Canvas.GetBottom(F.GetShape()));
                        SaveChild(F, sw, RecusionLevel);
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
                sw.WriteLine("Groep: " + fig.GetGroep().Count().ToString() + " ");
                for (int i = 0; i < Reclvl; i++)
                {
                    sw.Write("-");
                }
                foreach (Ornament OR in fig.GetOrnament())
                {
                    sw.WriteLine("ornament " + OR.GetLocation() + " " + OR.GetText() + "");
                }
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
                if (fig.GetGroep().Count != 0)
                    Reclvl++;
                    SaveChild(fig, sw, Reclvl);
            }
        }

        public void Visit(ResizeShape R)
        {           
            foreach (Figuren F in SelectedFiguren)
            {
                F.Resize(start, end);
            }            
        }
               

        public void Visit(MoveShape M)
        {
            double moveX = end.X - start.X;
            double moveY = end.Y - start.Y;
            foreach (Figuren F in SelectedFiguren)
            {
                if (!SelectedFiguren.Contains(F.Parent))
                    F.Move(moveX, moveY);

            }
        }
               
        public void Visit(Load L)
        {
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

        private Figuren LoadFig(string[] result)
        {
            string[] position, Gsize;
            int left, top, right, bot, numChildren;
            int backtrack = 2;
            while (readline < result.Count())
            {
                switch (result[readline])
                {
                    case "Rechthoek":
                        Gsize = result[readline - backtrack].Split(':');
                        while (Gsize.Count() == 1)
                        {
                            Gsize = result[readline - backtrack].Split(':');
                            backtrack++;

                        }
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
                        Rechthoeken RectangleFiguren = new Rechthoeken(newRectangle, MyCanvas);
                        RectangleFiguren.SetPosition(top, left, bot, right);

                        List<Figuren> figlist = new List<Figuren>();
                        figlist.Add(RectangleFiguren);
                        string[] ornamenttext = result[readline - 1].Split(' ');
                        commandinvoker.AddOrnament(ref figlist, ornamenttext[2], ornamenttext[1]);
                        commandinvoker.ExecuteCommands();

                        for (int c = 0; c < numChildren; c++)
                        {
                            readline += 4;
                            Figuren child = LoadFig(result);
                            int childgroupsize = 0;
                            if (child != null)
                            {
                                RectangleFiguren.Add(child);
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
                        Ellipsen ELlipsenFiguren = new Ellipsen(newEllipse, MyCanvas);
                        ELlipsenFiguren.SetPosition(top, left, bot, right);
                        for (int c = 0; c < numChildren; c++)
                        {
                            readline += 3;
                            Figuren child = LoadFig(result);
                            int childgroupsize = 0;
                            if (child != null)
                            {
                                ELlipsenFiguren.Add(child);
                                childgroupsize = child.GetGroupSize();
                            }
                        }
                        AllFiguren.Add(ELlipsenFiguren);
                        return ELlipsenFiguren;
                }
                readline++;
            }
            return null;
        }

        
    }
}
