using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

using System.Windows.Media.Imaging;

namespace Grafische_editor_Design_Patters
{
    class Invoker
    {
        private List<Command> commands = new List<Command>();
        private List<Figuren> SF = new List<Figuren>();



        public void ellipse(Point s, Point e, Canvas c, List<Figuren> AF)
        {
            DrawEllipse DE = new DrawEllipse(s, e, c, AF);
            commands.Add(DE);           
        }

        public void rectangle(Point s, Point e, Canvas c, List<Figuren> AF)
        {
            DrawRectangle DE = new DrawRectangle(s, e, c, AF);
            commands.Add(DE);
        }

        public void moveShape(Point s, Point e, Canvas c, List<Figuren> AF)
        {
            MoveShape DE = new MoveShape(s, e, c, AF,SF);
            commands.Add(DE);
        }

        public void SelectShape(Point s, Point e, Canvas c, List<Figuren> AF, Border SB)
        {
            SelectShape DE = new SelectShape(s, e, c, AF, SF, SB);
            commands.Add(DE);
        }

        public void ResizeShape(Point s, Point e, Canvas c)
        {
            ResizeShape DE = new ResizeShape(s, e, c, SF);
            commands.Add(DE);
        }

        public void groupIn(Point s, Point e, Canvas c, List<Figuren> AF, Border GB)
        {
            groupIn DE = new groupIn(s, e, c, SF, AF, GB);
            commands.Add(DE);
        }

        public void groupOut(Point s, Point e, Canvas c, List<Figuren> AF, Border GB)
        {
            groupOut DE = new groupOut(s, e, c, SF, AF, GB);
            commands.Add(DE);
        }

        public void ExecuteCommands()
        {
            foreach (Command C in commands)
            {
                C.Execute();
            }
            commands.Clear();
        }



    }


}
