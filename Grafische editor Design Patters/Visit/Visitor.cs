using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    class Visitor 
    {
        private List<Figuren> AllFiguren;
        private List<Figuren> SelectedFiguren;
        private Point start, end;
        private int readline = 0;
        private Canvas MyCanvas;
        private Invoker commandinvoker = new Invoker();
        public Visitor(ref List<Figuren> AF, ref List<Figuren> SF, Point S, Point E, ref Canvas MyC)
        {
            AllFiguren = AF;
            SelectedFiguren = SF;
            start = S;
            end = E;
            MyCanvas = MyC;
        }
        public void RefreshPoints(Point S, Point E)
        {
            start = S;
            end = E;
        }
       
        public void Visit(ResizeShape R)
        {
            foreach (Figuren F in SelectedFiguren)
            {
                F.Resize(start, end);
            }
        }


       

      
    }
}