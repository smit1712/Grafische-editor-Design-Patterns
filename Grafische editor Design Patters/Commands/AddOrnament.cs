using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafische_editor_Design_Patters
{
    class AddOrnament : ICommand
    {
        private readonly string text;
        private readonly Figuren Fig;
        IDecorator decorator;
        public AddOrnament(Figuren F,string T,  IDecorator D)
        {
            text = T;
            Fig = F;
            decorator = D;
        }
        public void Execute()
        {
            decorator.Decorate(Fig,text);
        }
    }
}
