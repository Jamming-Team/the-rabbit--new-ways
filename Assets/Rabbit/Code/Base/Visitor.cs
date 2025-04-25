namespace Rabbit {
    public interface IVisitor
    {
        void Visit(object o);
    }
    
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}