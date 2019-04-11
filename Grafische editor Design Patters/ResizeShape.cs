namespace Grafische_editor_Design_Patters
{
    class ResizeShape : Ivisitable
    {
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
    }
}
