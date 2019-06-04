﻿using System.Collections.Generic;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Decorator classe voor de Ornamenten
    /// maakt een Ornament aan en koppeld deze aan een figuur
    /// </summary>
    class OrnamentDecorator : ShapeDecorator
    {
        public OrnamentDecorator(Figuren DecoratedShape, string Or, string Loc) : base(DecoratedShape)
        {
            DecoratedShape.AddNewOrnament(Or, Loc);
        }
        public override List<Ornament> GetOrnament()
        {
            return DecoratorShape.GetOrnament();
        }

    }
}