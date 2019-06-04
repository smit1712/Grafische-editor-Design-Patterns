using System.Windows;

namespace Grafische_editor_Design_Patters
{
    class ResizeShape : IVisitor
    {
        Point start, end;
        public ResizeShape(Point s, Point e)
        {
            start = s;
            end = e;
        }
        public void Visit(Figuren F)
        {
            F.Resize(start, end);
        }


    }
}
