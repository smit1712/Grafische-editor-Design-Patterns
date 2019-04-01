using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    class Load : IShapeVisitor
    {
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
    }
}
