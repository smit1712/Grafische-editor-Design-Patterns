using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    class VisitorCollector :IShapeVisitor
    {
        IShapeVisitor[] parts;
        public VisitorCollector()
        {
            parts = new IShapeVisitor[] { new Save() };

        }
        public void Accept(IVisitor V)
        {
            for (int i = 0; i < parts.Count(); i++)
            {
                parts[i].Accept(V);
            }
            V.Visit(this);
        }

        
    }
}
