﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Grafische_editor_Design_Patters
{
    class Rechthoeken : Figuren
    {
        public Rechthoeken(Rectangle R) : base(R)
        {
           
        }
        public override void Select()
        {
            MyFigure.Stroke = Brushes.Black;
        }
        public override void Deslelect()
        {
            MyFigure.Stroke = Brushes.Green;
        }
    }
}
