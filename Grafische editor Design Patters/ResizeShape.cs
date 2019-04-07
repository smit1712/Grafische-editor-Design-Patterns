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
    class ResizeShape : Ivisitable
    {
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
    }
}
