using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    class AddOrnament : Command
    {
        private string Or;
        private string Loc;
        private Figuren Fig;
        public AddOrnament(Figuren F,string O, string L)
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
