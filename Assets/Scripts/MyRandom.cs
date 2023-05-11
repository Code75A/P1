using System;
public static class MyRandom
{
    private static Random _random = new Random();

    public static int random()
    {
        return _random.Next();
    }

    public static int random(int maxValue)
    {
        return _random.Next(maxValue);
    }

    public static int random(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }

    public static void seed(int seed)
    {
        _random = new Random(seed);
    }
}
