﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    class LeftOrnamentDecorator : IDecorator
    {
        public void Decorate(Figuren F, string text)
        {
            Ornament Or = new Ornament(F.Mycanvas, text, "Left", F.MyFigure);
            F.Ornamenten.Add(Or);
        }
    }
}
