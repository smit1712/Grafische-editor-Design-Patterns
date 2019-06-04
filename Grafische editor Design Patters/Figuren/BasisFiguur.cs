using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    class BasisFiguur
    {
       
        public Figuur figuur { get; set; }//delegate
        
        public BasisFiguur(Shape s,  Canvas c)
        {
            if (s.GetType() == typeof(Rectangle))            
                figuur = Rechthoeken.Instance((Rectangle) s, c);
            

            if (s.GetType() == typeof(Ellipse))
                figuur = Ellipsen.Instance((Ellipse)s, c);

        }

        public String ToString()
        {
            return figuur.type;
        }

    }
}
