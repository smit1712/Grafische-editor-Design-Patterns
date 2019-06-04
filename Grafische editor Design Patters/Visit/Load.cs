using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    class Load : IVisitor
    {

        private int readline = 0;
        private readonly Canvas MyCanvas;
        private Invoker commandinvoker = new Invoker();
        private List<Figuren> AllFiguren;
        private IDecorator decorator;

        public Load(List<Figuren> AF, Canvas C, IDecorator D)
        {
            AllFiguren = AF;
            MyCanvas = C;
            decorator = D;
        }

        public void Visit(Figuren F)
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
                        //MyCanvas.Children.Add(newRectangle);
                        Figuren RectangleFiguren = new Figuren(newRectangle, "Rechthoek", MyCanvas);
                        RectangleFiguren.SetPosition(position[0], position[1], position[2], position[3]);
                        templist = new List<Figuren>
                        {
                            RectangleFiguren
                        };
                        foreach (string[] str in ornamentsloaded)
                        {
                            switch (str[0])
                            {
                                case "Top":
                                    decorator = new TopOrnamentDecorator();
                                    break;

                                case "Bot":
                                    decorator = new BotOrnamentDecorator();
                                    break;

                                case "Left":
                                    decorator = new LeftOrnamentDecorator();
                                    break;

                                case "Right":
                                    decorator = new RightOrnamentDecorator();
                                    break;
                                default:
                                    break;
                            }
                            commandinvoker.AddOrnament(ref templist, str[1], decorator);
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
                        //MyCanvas.Children.Add(NewElipse);
                        Figuren ElipseFiguren = new Figuren(NewElipse, "Ellipse", MyCanvas);
                        ElipseFiguren.SetPosition(position[0], position[1], position[2], position[3]);
                        templist = new List<Figuren>
                        {
                            ElipseFiguren
                        };
                        foreach (string[] str in ornamentsloaded)
                        {
                            switch (str[0])
                            {
                                case "Top":
                                    decorator = new TopOrnamentDecorator();
                                    break;

                                case "Bot":
                                    decorator = new BotOrnamentDecorator();
                                    break;

                                case "Left":
                                    decorator = new LeftOrnamentDecorator();
                                    break;

                                case "Right":
                                    decorator = new RightOrnamentDecorator();
                                    break;
                                default:
                                    break;
                            }
                            commandinvoker.AddOrnament(ref templist, str[1], decorator);
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
