using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    class AddOrnament : ICommand
    {
        private readonly string Or;
        private readonly string Loc;
        private readonly BasisFiguur Fig;
        public AddOrnament(BasisFiguur F,string O, string L)
        {
            Or = O;
            Loc = L;
            Fig = F;
        }
        public void Execute()
        {
            IDecorator ShapeDC = new OrnamentDecorator(Fig, Or,Loc);
        }
    }
}
