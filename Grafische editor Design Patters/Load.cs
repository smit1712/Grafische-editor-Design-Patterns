namespace Grafische_editor_Design_Patters
{
    class Load : Ivisitable
    {
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
    }
}
