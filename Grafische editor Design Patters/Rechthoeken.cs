﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace Grafische_editor_Design_Patters
{
    class Rechthoeken : Figuren
    {
        public Rechthoeken(Rectangle R, Canvas C) : base(R, "Rechthoek", C)
        {
           
        }
        public override void Select()
        {
            MyFigure.Stroke = Brushes.Black;
        }
        public override void Deselect()
        {
            MyFigure.Stroke = Brushes.Green;
        }
    }
}
