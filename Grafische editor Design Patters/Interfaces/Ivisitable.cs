namespace Grafische_editor_Design_Patters
{
    /// <summary>
    /// Interface voor de visitor pattern.
    /// Maakt een object bezoekbaar
    /// </summary>
   public interface IVisitable
    {
        void Accept(IVisitor v);
    }
}
