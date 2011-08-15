namespace Ducks
{
    public interface IDuck
    {
        string Color { get; set; }
        Direction Fly();
        Direction Fly(Direction direction);
        string Quack();
        string Quack(int times);
        string Quack(string sound);
        string Quack(object sound);
        string Quack(params object[] sounds);
        object this[string attribute] { get; set; }
    }
}
