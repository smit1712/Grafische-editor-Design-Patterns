using System.Collections.Generic;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Interface voor de decoratorpattern
    /// </summary>
    interface IDecorator
    {
        void Decorate(Figuren F, string text);
    }
}
