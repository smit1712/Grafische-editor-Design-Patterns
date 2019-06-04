using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    class BotOrnamentDecorator : IDecorator
    {
        public void Decorate(Figuren F, string text)
        {
            Ornament Or = new Ornament(F.Mycanvas, text, "Bot", F.MyFigure);
            F.Ornamenten.Add(Or);
        }
    }
}
