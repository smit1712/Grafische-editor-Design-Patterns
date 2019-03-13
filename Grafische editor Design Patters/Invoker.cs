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
        private int CommandCounter = 0;

   
        public void ellipse(Point s, Point e, Canvas c, List<Figuren> AF)
        {
            DrawEllipse DE = new DrawEllipse(s, e, c, AF);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, DE);
            }
            else
            {
                commands.Add(DE);
            }
            CommandCounter++;





        }

        public void rectangle(Point s, Point e, Canvas c, List<Figuren> AF)
        {
            DrawRectangle DE = new DrawRectangle(s, e, c, AF);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, DE);
            }
            else
            {
                commands.Add(DE);
            }
            CommandCounter++;



        }

        public void moveShape(Point s, Point e, Canvas c, List<Figuren> AF)
        {
           
            MoveShape DE = new MoveShape(s, e, c, AF,SF);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, DE);
            }
            else
            {
                commands.Add(DE);
            }
            CommandCounter++;

        }

        public void SelectShape(Point s, Point e, Canvas c, List<Figuren> AF, Border SB)
        {
           
            SelectShape DE = new SelectShape(s, e, c, AF, SF, SB);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, DE);
            }
            else
            {
                commands.Add(DE);
            }
            CommandCounter++;

        }

        public void ResizeShape(Point s, Point e, Canvas c)
        {
           
            ResizeShape DE = new ResizeShape(s, e, c, SF);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, DE);
            }
            else
            {
                commands.Add(DE);
            }
            CommandCounter++;

        }

        public void groupIn(Point s, Point e, Canvas c, List<Figuren> AF, Border GB)
        {
           
            groupIn DE = new groupIn(s, e, c, SF, AF, GB);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, DE);
            }
            else
            {
                commands.Add(DE);
            }
            CommandCounter++;

        }

        public void groupOut(Point s, Point e, Canvas c, List<Figuren> AF, Border GB)
        {
            
            groupOut DE = new groupOut(s, e, c, SF, AF, GB);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, DE);
            }
            else
            {
                commands.Add(DE);
            }
            CommandCounter++;

        }

        public void ExecuteCommands()
        {
            //int commandstodo = commands.Count() - CommandCounter;

            
                    commands[CommandCounter -1].Execute();
                          
            
            //commands.Clear();
        }
        public void Undo(Canvas C, List<Figuren> AF)
        {
            C.Children.Clear();
            AF.Clear();
            if(CommandCounter > 0)
            CommandCounter--;
            for (int i = 0; i < CommandCounter; i++)
            {
                commands[i].Execute();
            }
        }
        public void Redo()
        {
            if (CommandCounter < commands.Count())
            {
                CommandCounter++;
                commands[CommandCounter - 1].Execute();
            }
        }



    }


}
