namespace Grafische_editor_Design_Patters
{
    class ResizeShape : IVisitable
    {
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
    }
}
