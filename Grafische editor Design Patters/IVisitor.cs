namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Interface voor de visitor.
    /// Voor het object dat de bezoekbare objecten Bezoekt
    /// </summary>
    interface IVisitor
    {
        void Visit(Save S);
        void Visit(Load L);
        void Visit(ResizeShape R);
        void Visit(MoveShape M);
    }
}
