using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;


namespace Grafische_editor_Design_Patters
{
    class Ellipsen : Figuren
    {
        public Ellipsen(Ellipse E) : base(E,"Ellpise")
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
