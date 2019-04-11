namespace Grafische_editor_Design_Patters
{

    class MoveShape : Ivisitable
    {
        public void Accept(IVisitor V)
        {
            V.Visit(this);
        }
    }
}
