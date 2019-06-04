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
    class Save : IVisitor
    {

        private List<Figuren> AllFiguren;

        public Save(List<Figuren> AF)
        {
            AllFiguren = AF;
        }
        public void Visit(Figuren F)
        {
            StreamWriter sw = new StreamWriter(@"C:/GrafischeEditor/Save.txt");
            int RecusionLevel = 1;
            foreach (Figuren f in AllFiguren)
            {
                if (f.Isingroup == false)
                {
                    sw.WriteLine("Groep:" + f.GetGroep().Count().ToString());
                    foreach (Ornament OR in f.GetOrnament())
                    {
                        sw.WriteLine("ornament " + OR.GetLocation() + " " + OR.GetText() + " ");
                    }
                    sw.WriteLine(f.type);
                    int Left = Convert.ToInt16(Canvas.GetLeft(f.GetShape()));
                    int Top = Convert.ToInt16(Canvas.GetTop(f.GetShape()));
                    int Right = Convert.ToInt16(Canvas.GetRight(f.GetShape()));
                    int Bot = Convert.ToInt16(Canvas.GetBottom(f.GetShape()));
                    sw.WriteLine(Left + " " + Top + " " + Right + " " + Bot);
                    SaveChild(f, sw, RecusionLevel);
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

    }
}
