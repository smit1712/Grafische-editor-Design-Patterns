﻿using System;
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
    abstract public class Figuren : IShape
    {
        private List<Figuren> Groep = new List<Figuren>();
        public double top, left, bot,right;
        protected Shape MyFigure;
        public bool Isingroup;
        public Figuren Parent;
        public string type;
        private List<Ornament> Ornamenten = new List<Ornament>();
        public Canvas Mycanvas;
        public Figuren(Shape S, string T, Canvas C)
        {
            MyFigure = S;
            type = T;
            Mycanvas = C;
            SetPosition( Canvas.GetLeft(S), Canvas.GetTop(S), Canvas.GetRight(S), Canvas.GetBottom(S));

        }
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
        public void AddNewOrniment(string text, string location)
        {
            Ornament Or = new Ornament(Mycanvas,text,location,MyFigure);
            Ornamenten.Add(Or);
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
        public void Add(Figuren F)
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
        public void ControlPosition()
        {
            left = Canvas.GetLeft(MyFigure);
            top = Canvas.GetTop(MyFigure);
            right = Canvas.GetRight(MyFigure);
            bot = Canvas.GetBottom(MyFigure);

            double temp;
            if(left > right)
            {
                temp = right;
                right = left;
                left = temp;
            }
            if(top > bot)
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
        public void Resize(Point start, Point end)
        {
            double sizex = end.X - start.X;
            double sizey = end.Y - start.Y;
      
            bot = Canvas.GetTop(MyFigure) + sizey;
            right = Canvas.GetLeft(MyFigure) + sizex;
            MyFigure.Height = sizey;
            MyFigure.Width = sizex;
            SetPosition(Canvas.GetLeft(MyFigure),Canvas.GetTop(MyFigure), Canvas.GetLeft(MyFigure) + sizex, Canvas.GetTop(MyFigure) + sizey);

            ControlPosition();

            foreach (Figuren F in Groep)
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