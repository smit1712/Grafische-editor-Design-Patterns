namespace Grafische_editor_Design_Patters
{

    class MoveShape : IVisitable
    {
        public void Accept(IVisitor V)
        {
            V.Visit(this);
        }
    }
}
