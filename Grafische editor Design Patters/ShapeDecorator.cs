using System.Collections.Generic;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Abstracte classe voor OrnamentDecorator 
    /// </summary>
    abstract class ShapeDecorator : IDecorator
    {
        protected IDecorator DecoratorShape;

        public ShapeDecorator(IDecorator DecoratedShape)
        {
            DecoratorShape = DecoratedShape;
        }
        virtual public List<Ornament> GetOrnament()
        {
            return DecoratorShape.GetOrnament();
        }

    }
}
