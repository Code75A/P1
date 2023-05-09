using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRandom
{
    private long seed = 0;
    public void SetSeed(long n)
    {
        seed = n;
    }
    public long GetSeed()
    {
        return seed;
    }

    public long Random(long left,long right)
    {
        seed = (seed * 9301 + 49297) % 114514233280;
        return seed % (right - left + 1) + left;
    }
}
