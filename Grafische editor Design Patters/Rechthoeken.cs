using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Rechthoeken erft van Figuren
    /// Hierin staan de Rechthoek specefieke dingen, Voornamelijk het Rechthoek object in de constructor
    /// </summary>
    class Rechthoeken : Figuren
    {
        public Rechthoeken(Rectangle R, Canvas C) : base(R, "Rechthoek", C)
        {

        }
        public override void Select()
        {
            MyFigure.Stroke = Brushes.Gray;
        }
        public override void Deselect()
        {
            MyFigure.Stroke = Brushes.DarkGray;
        }
    }
}
