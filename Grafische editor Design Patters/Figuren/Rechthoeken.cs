using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Rechthoeken erft van Figuren
    /// Hierin staan de Rechthoek specefieke dingen, Voornamelijk het Rechthoek object in de constructor
    /// </summary>
    class Rechthoeken : Idelegatefiguur
    {
        Rectangle rectangle;
        private static Rechthoeken _instance;
        public static Rechthoeken Instance(Rectangle r)
        {
            if (_instance == null)
            {
                _instance = new Rechthoeken(r);
            }
            return _instance;
        }
        private Rechthoeken(Rectangle r)
        {
            rectangle = r;
        }

        public string toString()
        {
            return "Ellipse";
        }
        public void draw(int top, int left, int bot, int right)
        {
            Canvas.SetLeft(rectangle, left);
            Canvas.SetTop(rectangle, top);
            Canvas.SetRight(rectangle, right);
            Canvas.SetBottom(rectangle, bot);
        }
    }
}
