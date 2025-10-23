namespace WebApplication2.Services2;

public interface IRandomInt
{
    public int GetRandomInt();
}

public class RandomInt : IRandomInt
{
    private readonly int _randomInt;

    public RandomInt()
    {
        _randomInt = new Random().Next();
    }

    public int GetRandomInt()
    {
        return _randomInt;
    }
}
