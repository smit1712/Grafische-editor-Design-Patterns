using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    interface IVisitor
    {
        void Visit(Save S);
        void Visit(Load L);
        void Visit(ResizeShape R);
        void Visit(MoveShape M);
        void Visit(VisitorCollector VC);
    }
}
