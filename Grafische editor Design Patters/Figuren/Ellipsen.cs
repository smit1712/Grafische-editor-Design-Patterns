using System.Windows.Controls;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Hierin staan de Ellipse specefieke dingen, Voornamelijk het Ellipse object in de constructor
    /// </summary>
    class Ellipsen : Idelegatefiguur
    {
        readonly Ellipse ellipse;
        private static Ellipsen _instance;
        private Canvas canvas;
        public static Ellipsen Instance(Ellipse e, Canvas c)
        {
            if (_instance == null)
            {
                _instance = new Ellipsen(e, c);

            }
            return _instance;
        }

        private Ellipsen(Ellipse e, Canvas c)
        {
            ellipse = e;
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
            return ellipse;
        }
    }
}
