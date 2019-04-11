using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        public Visitor(ref List<Figuren> AF, ref List<Figuren> SF, Point S, Point E, ref Canvas MyC)
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
                    foreach (Ornament OR in F.GetOrnament())
                    {
                        sw.WriteLine("ornament " + OR.GetLocation() + " " + OR.GetText() + " ");
                    }
                    sw.WriteLine(F.type);
                    int Left = Convert.ToInt16(Canvas.GetLeft(F.GetShape()));
                    int Top = Convert.ToInt16(Canvas.GetTop(F.GetShape()));
                    int Right = Convert.ToInt16(Canvas.GetRight(F.GetShape()));
                    int Bot= Convert.ToInt16(Canvas.GetBottom(F.GetShape()));
                    sw.WriteLine(Left + " " + Top + " " + Right + " " + Bot);
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
                sw.WriteLine("Groep:" + fig.GetGroep().Count().ToString() + " ");
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
                int Left = Convert.ToInt16(Canvas.GetLeft(fig.GetShape()));
                int Top = Convert.ToInt16(Canvas.GetTop(fig.GetShape()));
                int Right = Convert.ToInt16(Canvas.GetRight(fig.GetShape()));
                int Bot = Convert.ToInt16(Canvas.GetBottom(fig.GetShape()));
                sw.WriteLine(Left + " " + Top + " " + Right + " " + Bot); if (fig.GetGroep().Count != 0)
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
            int[] position = new int[4];
            Regex regex = new Regex("");
            MatchCollection matches;
            int groupsize = 0;
            List<string[]> ornamentsloaded = new List<string[]>();
            List<Figuren> templist = new List<Figuren>();
            while (readline < result.Count())
            {
                regex = new Regex("Groep:(\\d*)");
                matches = regex.Matches(result[readline]);
                foreach (Match M in matches)
                {
                    groupsize = Convert.ToInt16(M.Groups[1].ToString());
                    readline++;
                }
                while (result[readline].Contains("ornament"))
                {
                    regex = new Regex("ornament\\s(\\w*)\\s(.*)");
                    matches = regex.Matches(result[readline]);
                    foreach (Match M in matches)
                    {

                        string loc = M.Groups[1].ToString();
                        string or = M.Groups[2].ToString();
                        string[] ornament = { loc, or };
                        ornamentsloaded.Add(ornament);
                        readline++;
                    }
                }
                regex = new Regex("(\\d+)\\s(\\d+)\\s(\\d+)\\s(\\d+)");

                if (result.Count() > readline + 1 && regex.IsMatch(result[readline + 1]))
                {
                    matches = regex.Matches(result[readline + 1]);
                    position[0] = Convert.ToInt16(matches[0].Groups[1].Value);
                    position[1] = Convert.ToInt16(matches[0].Groups[2].Value);
                    position[2] = Convert.ToInt16(matches[0].Groups[3].Value);
                    position[3] = Convert.ToInt16(matches[0].Groups[4].Value);
                }

                switch (result[readline])
                {
                    case "Rechthoek":

                        Rectangle newRectangle = new Rectangle()
                        {
                            Stroke = Brushes.Green,
                            Fill = Brushes.Red,
                            StrokeThickness = 4,
                            Width = position[2] - position[0],
                            Height = position[3] - position[1],
                        };
                        MyCanvas.Children.Add(newRectangle);
                        Rechthoeken RectangleFiguren = new Rechthoeken(newRectangle, MyCanvas);
                        RectangleFiguren.SetPosition(position[0], position[1], position[2], position[3]);
                        templist = new List<Figuren>
                        {
                            RectangleFiguren
                        };
                        foreach (string[] str in ornamentsloaded)
                        {
                            commandinvoker.AddOrnament(ref templist, str[1], str[0]);
                        }
                        commandinvoker.ExecuteCommands();

                        for (int c = 0; c < groupsize; c++)
                        {
                            readline += 2;
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

                        Ellipse NewElipse = new Ellipse()
                        {
                            Stroke = Brushes.Green,
                            Fill = Brushes.Red,
                            StrokeThickness = 4,
                            Width = position[2] - position[0],
                            Height = position[3] - position[1],
                        };
                        MyCanvas.Children.Add(NewElipse);
                        Ellipsen ElipseFiguren = new Ellipsen(NewElipse, MyCanvas);
                        ElipseFiguren.SetPosition(position[0], position[1], position[2], position[3]);
                        templist = new List<Figuren>
                        {
                            ElipseFiguren
                        };
                        foreach (string[] str in ornamentsloaded)
                        {
                            commandinvoker.AddOrnament(ref templist, str[1], str[0]);
                        }
                        commandinvoker.ExecuteCommands();

                        for (int c = 0; c < groupsize; c++)
                        {
                            readline += 2;
                            Figuren child = LoadFig(result);
                            int childgroupsize = 0;
                            if (child != null)
                            {
                                ElipseFiguren.Add(child);
                                childgroupsize = child.GetGroupSize();
                            }
                        }
                        AllFiguren.Add(ElipseFiguren);
                        return ElipseFiguren;

                    default:
                        readline++;
                        break;
                }
            }
            return null;
        }
    }
}