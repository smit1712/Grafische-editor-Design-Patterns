namespace Grafische_editor_Design_Patters
{
    class Load : IVisitable
    {
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
    }
}
