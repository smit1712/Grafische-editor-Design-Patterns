using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Ellipsen erft van Figuren
    /// Hierin staan de Ellipse specefieke dingen, Voornamelijk het Ellipse object in de constructor
    /// </summary>
    class Ellipsen : Figuren
    {
        public Ellipsen(Ellipse E, Canvas C) : base(E, "Ellipse", C)
        {

        }
        public override void Select()
        {
            MyFigure.Stroke = Brushes.Black;
        }
        public override void Deselect()
        {
            MyFigure.Stroke = Brushes.Green;
        }
    }
}
