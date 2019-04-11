namespace Grafische_editor_Design_Patters
{
    class Save : IVisitable
    {
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
    }
}
