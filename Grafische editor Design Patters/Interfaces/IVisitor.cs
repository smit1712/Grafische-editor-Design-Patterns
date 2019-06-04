namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Interface voor de visitor.
    /// Voor het object dat de bezoekbare objecten Bezoekt
    /// </summary>
   public interface IVisitor
    {
         void Visit(Figuren F);       
    }
}
