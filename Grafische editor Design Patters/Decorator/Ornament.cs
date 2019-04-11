using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Ornament object. Wordt door de decorator gemaakt en aan een figuur gekoppeld
    /// </summary>
    public class Ornament
    {
        protected string Text;
        protected string Location;
        private readonly Shape Mother;
        private TextBlock OrnamentShape = new TextBlock();
        public Ornament(Canvas c, String T, string L, Shape M)
        {
            OrnamentShape.Foreground = new SolidColorBrush(Colors.Black);
            Mother = M;
            OrnamentShape.Text = T;
            Text = T;
            Location = L;
            c.Children.Add(OrnamentShape);
            ChangeLocation();
        }

        public void ChangeLocation()
        {
            double left = 0, top = 0;
            switch (Location)
            {
                case "Top":
                    left = Canvas.GetLeft(Mother);
                    top = Canvas.GetTop(Mother) - 20;
                    break;
                case "Bot":
                    left = Canvas.GetLeft(Mother);
                    top = Canvas.GetBottom(Mother) + 20;
                    break;
                case "Left":
                    left = Canvas.GetLeft(Mother) - 100;
                    top = Canvas.GetTop(Mother);
                    break;
                case "Right":
                    left = Canvas.GetRight(Mother) + 20;
                    top = Canvas.GetTop(Mother);
                    break;
                default:
                    break;
            }
            Canvas.SetLeft(OrnamentShape, left);
            Canvas.SetTop(OrnamentShape, top);
        }
        public string GetText()
        {
            return Text;
        }
        public string GetLocation()
        {
            return Location;
        }
    }
}
