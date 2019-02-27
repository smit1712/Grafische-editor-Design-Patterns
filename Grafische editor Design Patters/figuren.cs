using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    public class Figuren 
    {
        private List<Figuren> Groep;
        public int X, Y,Width,Height;
        public List<Figuren> GetGroep()
        {
            return Groep;
        }

        public void InsertGroep(Figuren F)
        {
            Groep.Add(F);
        }
        public void RemoveFromGroep(Figuren F)
        {
            foreach(Figuren figuren in Groep)
            {
                if (figuren == F)
                    Groep.Remove(F);
            }
        }
        public void Move(int x, int y)
        {
            X += x;
            Y += y;
            foreach(Figuren figuren in Groep)
            {
                figuren.X += x;
                figuren.Y += y;
            }
        }
        public void Resize(int w, int h)
        {
            Width += w;
            Height += h;
            foreach (Figuren figuren in Groep)
            {
                figuren.Width += w;
                figuren.Height += h;
            }
        }
    }
}