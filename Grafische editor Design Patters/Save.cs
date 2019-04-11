namespace Grafische_editor_Design_Patters
{
    class Save : Ivisitable
    {
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
    }
}
