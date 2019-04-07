using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    class OrnamentDecorator : ShapeDecorator
    {
        public OrnamentDecorator(Figuren DecoratedShape, string Or, string Loc) : base(DecoratedShape)
        {
            DecoratedShape.AddNewOrniment(Or,Loc);
        }
        public override List<Ornament> GetOrnament()
        {
           return DecoratorShape.GetOrnament();
        }
        
    }
}
