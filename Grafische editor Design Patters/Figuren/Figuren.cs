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
    abstract public class Figuur : IDecorator
    {
        //List met alle Figuren Behorende bij de groep van dit Figuur
        private List<Figuur> Groep = new List<Figuur>();
        //List Met alle Ornamenten bij deze Figuur
        private List<Ornament> Ornamenten = new List<Ornament>();

        public double top, left, bot, right;
        public Shape MyFigure;
        public bool Isingroup;
        public Figuur Parent { get; private set; }        
        public string type;
        private readonly Canvas Mycanvas;
        public Figuur(Shape S, string T, Canvas C)
        {
            MyFigure = S;
            type = T;
            Mycanvas = C;
            SetPosition(Canvas.GetLeft(S), Canvas.GetTop(S), Canvas.GetRight(S), Canvas.GetBottom(S));
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
        public void AddNewOrnament(string text, string location)
        {
            Ornament Or = new Ornament(Mycanvas, text, location, MyFigure);
            Ornamenten.Add(Or);
        }

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
        public List<Figuur> GetGroep()
        {
            return Groep;
        }
        public int GetGroupSize()
        {
            int size = 0;
            if (Groep.Count() == 0)
                return -1;
            foreach (Figuur F in Groep)
            {

                size += F.GetGroupSize();
                if (size == -1)
                    size = 1;
            }
            return size;
        }
        //Voeg nieuw figuur toe aan groep van dit figuur
        public void Add(Figuur F)
        {
            if (this != F && F.Isingroup != true)
            {
                Groep.Add(F);
                F.AddParent(this);
                F.Isingroup = true;
            }
        }
        public void AddParent(Figuur F)
        {
            if(Parent == null)
            {
                Parent = F;
            }
        }
        public void RemoveFromGroep(Figuur F)
        {
            List<Figuur> RemoveFiguren = new List<Figuur>();
            foreach (Figuur figuren in Groep)
            {
                if (figuren == F)
                {
                    Add(F);
                }
            }
            foreach (Figuur figuren in RemoveFiguren)
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
            foreach (Figuur F in Groep)
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

            foreach (Figuur F in Groep)
            {
                F.Resize(start, end);
            }
        }

        public List<Ornament> GetOrnament()
        {
            return Ornamenten;
        }
    }
}