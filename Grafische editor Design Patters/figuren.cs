using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace Grafische_editor_Design_Patters
{
    public class Figuren
    {
        private List<Figuren> Groep = new List<Figuren>();
        public double X, Y;
        public Shape MyFigure;
        public bool Isingroup;
        public Figuren Parent;
        public string type;
        public Figuren(Shape S, string T)
        {
            MyFigure = S;
            type = T;
        }     
      
        public Shape GetShape()
        {
            return MyFigure;
        }
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
        public void InsertGroep(Figuren F)
        {
            if (this != F && F.Isingroup != true)
            {
                Groep.Add(F);
                F.Parent = this;
                F.Isingroup = true;
            }
        }
        public void RemoveFromGroep(Figuren F)
        {
            List<Figuren> RemoveFiguren = new List<Figuren>();
            foreach(Figuren figuren in Groep)
            {
                if (figuren == F)
                {
                    RemoveFiguren.Add(F);
                }
            }
            foreach(Figuren figuren in RemoveFiguren)
            {
                figuren.Parent = null;
                Groep.Remove(figuren);
                F.Isingroup = false;
            }
        }
        public void UpdateXY(double x, double y)
        {
            
            X = x;
            Y = y + 50;
            Canvas.SetLeft(MyFigure, x);
            Canvas.SetTop(MyFigure, y);
            
        }
        public void Move(double x, double y)
        {           
            X = x;
            Y = y + 50;
            Canvas.SetLeft(MyFigure, Canvas.GetLeft(MyFigure) + x);
            Canvas.SetTop(MyFigure, Canvas.GetTop(MyFigure) + y);
            foreach (Figuren F in Groep)
            {

                F.Move(x, y);
            }
            
        }
        public void Resize(double w, double h)
        {     
            MyFigure.Height = h;
            MyFigure.Width = w;
            foreach (Figuren F in Groep)
            {
                F.Resize(w, h);
            }
        }
    }
}