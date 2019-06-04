﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Control class voor het commandpattern. 
    /// Alle commando's worden hier gemaakt, bijgehouden en uiteindelijk uitgevoerd.
    /// </summary>
    class Invoker
    {

        private List<ICommand> commands = new List<ICommand>();
        private int CommandCounter = 0;
        private int CommandsDone = 0;
        private static Invoker _instance;
        private Invoker()
        {

        }
        public static Invoker Instance
        {
            get
                {
                if (_instance == null)
                {
                    _instance = new Invoker();
                }
                return _instance;
            }
        }
        public void Ellipse(Point s, Point e, Canvas c, List<BasisFiguur> AF)
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

        public void Rectangle(Point s, Point e, Canvas c, List<BasisFiguur> AF)
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

        public void SelectShape(Point s, Point e, Canvas c, List<BasisFiguur> AF, ref List<BasisFiguur> SF, Border SB)
        {
            SelectShape Sh = new SelectShape(s, e, c, AF, ref SF, SB);
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

        public void GroupIn(Point s, Point e, Canvas c, List<BasisFiguur> AF, ref List<BasisFiguur> SF, Border GB)
        {

            GroupIn Gi = new GroupIn(s, e, c, SF, AF, GB);
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

        public void GroupOut(Point s, Point e, Canvas c, List<BasisFiguur> AF, ref List<BasisFiguur> SF, Border GB)
        {

            GroupOut Go = new GroupOut(s, e, c, SF, AF, GB);
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

        public void AddOrnament(ref List<BasisFiguur> SF, string Or, String Loc)
        {
            foreach (BasisFiguur F in SF)
            {
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
            while (CommandsDone < CommandCounter)
            {
                commands[CommandsDone].Execute();
                CommandsDone++;
            }

        }
        //Undo voert alle commando's opnieuw uit, behalve de laatste
        public void Undo(Canvas C, List<BasisFiguur> AF, Border SB, Border GB)
        {
            C.Children.Clear();
            C.Children.Add(SB);
            C.Children.Add(GB);
            AF.Clear();
            CommandsDone = 0;
            if (CommandCounter > 0)
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
