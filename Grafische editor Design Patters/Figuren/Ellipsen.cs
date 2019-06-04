using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Hierin staan de Ellipse specefieke dingen, Voornamelijk het Ellipse object in de constructor
    /// </summary>
    class Ellipsen : Idelegatefiguur
    {
        Ellipse ellipse;
        private static Ellipsen _instance;
        public static Ellipsen Instance(Ellipse e)
        {
            if (_instance == null)
            {
                _instance = new Ellipsen(e);
                
            }
            return _instance;
        }

        private Ellipsen(Ellipse e)
        {
            ellipse = e;
        }

        public string toString()
        {
            return "Ellipse";
        }
        public void draw(int top,int left, int bot, int right)
        {
            Canvas.SetLeft(ellipse, left);
            Canvas.SetTop(ellipse, top);
            Canvas.SetRight(ellipse, right);
            Canvas.SetBottom(ellipse, bot);
        }
    }
}
