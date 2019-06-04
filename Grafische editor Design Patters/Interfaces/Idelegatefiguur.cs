using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    interface Idelegatefiguur
    {
        void Draw(Shape S);
        Shape GetShape();
    }
}
