using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Hierin staan de Rechthoek specefieke dingen, Voornamelijk het Rechthoek object in de constructor
    /// </summary>
    class Rechthoeken : Idelegatefiguur
    {
        Rectangle rectangle;
        private static Rechthoeken _instance;
        Canvas canvas;
        public static Rechthoeken Instance( Rectangle r, Canvas c)
        {
            if (_instance == null)
            {
                _instance = new Rechthoeken(r,c);
            }
            return _instance;
        }
        private Rechthoeken(Rectangle r, Canvas c )
        {
            rectangle = r;
            canvas = c;
        }

        public string toString()
        {
            return "Ellipse";
        }
        public void Draw(Shape S)
        {
            canvas.Children.Add(S);
        }

        public Shape GetShape()
        {
            return rectangle;
        }
    }
}
