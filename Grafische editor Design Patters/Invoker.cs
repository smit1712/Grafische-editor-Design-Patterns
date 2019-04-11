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
    /// <summary>
    /// Control class voor het commandpattern. 
    /// Alle commando's worden hier gemaakt, bijgehouden en uiteindelijk uitgevoerd.
    /// </summary>
    class Invoker
    {
        private List<Command> commands = new List<Command>();
        private int CommandCounter = 0;
        private int CommandsDone = 0;
   
        public void ellipse(Point s, Point e, Canvas c, List<Figuren> AF)
        {
            DrawEllipse El = new DrawEllipse(s, e, c, AF);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, El);
            }
            else
            {
                commands.Add(El);
            }
            CommandCounter++;
        }

        public void rectangle(Point s, Point e, Canvas c, List<Figuren> AF)
        {

            DrawRectangle Re = new DrawRectangle(s, e, c, AF);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, Re);
            }
            else
            {
                commands.Add(Re);
            }
            CommandCounter++;
        }

        public void SelectShape(Point s, Point e, Canvas c, List<Figuren> AF,ref List<Figuren> SF , Border SB)
        {
            SelectShape Sh = new SelectShape(s, e, c, AF,ref SF, SB);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, Sh);
            }
            else
            {
                commands.Add(Sh);
            }
            CommandCounter++;

        }        

        public void groupIn(Point s, Point e, Canvas c, List<Figuren> AF, ref List<Figuren> SF, Border GB)
        {
           
            groupIn Gi = new groupIn(s, e, c, SF, AF, GB);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, Gi);
            }
            else
            {
                commands.Add(Gi);
            }
            CommandCounter++;

        }

        public void groupOut(Point s, Point e, Canvas c, List<Figuren> AF, ref List<Figuren> SF, Border GB)
        {
            
            groupOut Go = new groupOut(s, e, c, SF, AF, GB);
            if (CommandCounter <= commands.Count())
            {
                commands.Insert(CommandCounter, Go);
            }
            else
            {
                commands.Add(Go);
            }
            CommandCounter++;
        }

        public void AddOrnament(ref List<Figuren> SF, string Or, String Loc)
        {
            foreach (Figuren F in SF) {
                AddOrnament AO = new AddOrnament(F, Or, Loc);
                if (CommandCounter <= commands.Count())
                {
                    commands.Insert(CommandCounter, AO);
                }
                else
                {
                    commands.Add(AO);
                }
                CommandCounter++;
            }
        }


        public void ExecuteCommands()
        {
             while(CommandsDone < CommandCounter)
            {
                commands[CommandsDone].Execute();
                CommandsDone++;
            }
              
        }
        //Undo voert alle commando's opnieuw uit, behalve de laatste
        public void Undo(Canvas C, List<Figuren> AF, Border SB, Border GB)
        {
            C.Children.Clear();
            C.Children.Add(SB);
            C.Children.Add(GB);
            AF.Clear();
            CommandsDone = 0;
            if(CommandCounter > 0)
            CommandCounter--;
            for (int i = 0; i < CommandCounter; i++)
            {
                commands[i].Execute();
            }
        }
        //Redo voer nog bestaande Undo's alsnog uit
        public void Redo()
        {
            if (CommandCounter < commands.Count())
            {
                CommandCounter++;
                CommandsDone++;
                commands[CommandCounter - 1].Execute();
            }
        }
    }
}
