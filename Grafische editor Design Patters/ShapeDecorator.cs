using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    class ShapeDecorator : IShape
    {
        protected IShape DecoratorShape;

        public ShapeDecorator(IShape DecoratedShape)
        {
            DecoratorShape = DecoratedShape;
        }
        virtual public List<Ornament> GetOrnament()
        {
           return DecoratorShape.GetOrnament();

        }

    }
}
