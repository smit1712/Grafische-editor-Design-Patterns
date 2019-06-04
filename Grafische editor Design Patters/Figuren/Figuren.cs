using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{

    /// <summary>
    /// Abstracte classe van alle tekenbare Figuren
    /// Bevat Recursieve move,resize en group functies.
    /// De Ornamenten zijn Via deze functie gekoppeld aan de canvas Shape
    /// </summary>
    public class Figuren : IVisitable
    {
        //List met alle Figuren Behorende bij de groep van dit Figuur
        private List<Figuren> Groep = new List<Figuren>();
        //List Met alle Ornamenten bij deze Figuur
        public List<Ornament> Ornamenten = new List<Ornament>();

        public double top, left, bot, right;
        public Shape MyFigure;
        public bool Isingroup;
        public Figuren Parent { get; private set; }
        public string type;
        public readonly Canvas Mycanvas;
        private readonly Idelegatefiguur delegatefiguur;
        public Figuren(Shape S, string T, Canvas C)
        {
            MyFigure = S;
            type = T;
            Mycanvas = C;
            SetPosition(Canvas.GetLeft(S), Canvas.GetTop(S), Canvas.GetRight(S), Canvas.GetBottom(S));

            if (S.GetType() == typeof(Rectangle))
                delegatefiguur = Rechthoeken.Instance((Rectangle)S, Mycanvas);
            if (S.GetType() == typeof(Ellipse))
                delegatefiguur = Ellipsen.Instance((Ellipse)S, Mycanvas);

            delegatefiguur.Draw(S);
        }
        //Zet de location van het figuur. Niet recursief
        public void SetPosition(double L, double T, double R, double B)
        {
            top = T;
            left = L;
            bot = B;
            right = R;
            Canvas.SetLeft(MyFigure, left);
            Canvas.SetTop(MyFigure, top);
            Canvas.SetRight(MyFigure, right);
            Canvas.SetBottom(MyFigure, bot);
        }
        //Voegt nieuw Ornament toe aan dit Figuur
     

        public Shape GetShape()
        {
            return MyFigure;
        }
        // Virtuele functies voor selecteren en deselecteren
        public virtual void Select()
        {
            MyFigure.Stroke = Brushes.Black;
        }
        public virtual void Deselect()
        {
            MyFigure.Stroke = Brushes.Green;
        }
        public List<Figuren> GetGroep()
        {
            return Groep;
        }
        public int GetGroupSize()
        {
            int size = 0;
            if (Groep.Count() == 0)
                return -1;
            foreach (Figuren F in Groep)
            {

                size += F.GetGroupSize();
                if (size == -1)
                    size = 1;
            }
            return size;
        }
        //Voeg nieuw figuur toe aan groep van dit figuur
        public void Add(Figuren F)
        {
            if (this != F && F.Isingroup != true)
            {
                Groep.Add(F);
                F.AddParent(this);
                F.Isingroup = true;
            }
        }
        public void AddParent(Figuren F)
        {
            if (Parent == null)
            {
                Parent = F;
            }
        }
        public void RemoveFromGroep(Figuren F)
        {
            List<Figuren> RemoveFiguren = new List<Figuren>();
            foreach (Figuren figuren in Groep)
            {
                if (figuren == F)
                {
                    RemoveFiguren.Add(F);
                }
            }
            foreach (Figuren figuren in RemoveFiguren)
            {
                figuren.Parent = null;
                Groep.Remove(figuren);
                F.Isingroup = false;
            }
        }
        //Controleerd huidige positie, en draaid Right en Left om indien nodig
        public void ControlPosition()
        {
            left = Canvas.GetLeft(MyFigure);
            top = Canvas.GetTop(MyFigure);
            right = Canvas.GetRight(MyFigure);
            bot = Canvas.GetBottom(MyFigure);

            double temp;
            if (left > right)
            {
                temp = right;
                right = left;
                left = temp;
            }
            if (top > bot)
            {
                temp = top;
                top = bot;
                bot = temp;
            }
            Canvas.SetLeft(MyFigure, left);
            Canvas.SetTop(MyFigure, top);
            Canvas.SetRight(MyFigure, right);
            Canvas.SetBottom(MyFigure, bot);

        }
        //recusieve move functie. Moved alles in de groep
        public void Move(double x, double y)
        {
            left += x;
            top += y;
            right += x;
            bot += y;
            Canvas.SetLeft(MyFigure, Canvas.GetLeft(MyFigure) + x);
            Canvas.SetTop(MyFigure, Canvas.GetTop(MyFigure) + y);
            Canvas.SetRight(MyFigure, Canvas.GetRight(MyFigure) + x);
            Canvas.SetBottom(MyFigure, Canvas.GetBottom(MyFigure) + y);
            foreach (Ornament OR in Ornamenten)
            {
                OR.ChangeLocation();
            }
            ControlPosition();
            foreach (Figuren F in Groep)
            {

                F.Move(x, y);
            }

        }
        //recusieve Resize functie. Resized alles in de groep
        public void Resize(Point start, Point end)
        {
            double sizex = end.X - start.X;
            double sizey = end.Y - start.Y;

            bot = Canvas.GetTop(MyFigure) + sizey;
            right = Canvas.GetLeft(MyFigure) + sizex;
            MyFigure.Height = sizey;
            MyFigure.Width = sizex;
            SetPosition(Canvas.GetLeft(MyFigure), Canvas.GetTop(MyFigure), Canvas.GetLeft(MyFigure) + sizex, Canvas.GetTop(MyFigure) + sizey);

            ControlPosition();

            foreach (Figuren F in Groep)
            {
                F.Resize(start, end);
            }
        }

        public void Accept(IVisitor v)
        {
            foreach (Figuren F in Groep)
            {
                F.Accept(v);
            }
            v.Visit(this);
        }

        public List<Ornament> GetOrnament()
        {
            return Ornamenten;
        }

    }
}