using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareAttempts : IComparer<Attempt>
{
    //-1; first < second, 0 equal, 1: first > second
    public int Compare(Attempt first, Attempt second)
    {
        if (first.score > second.score)
        {
            return 1;
        }
        else if (first.score < second.score)
        {
            return -1;
        }

        return 0;
    }
}
