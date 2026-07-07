using System;
using System.Collections.Generic;

// Returns the first index x in [low, high] where P(x) is true.
// If all false, returns high + 1.
int FirstTrue(int low, int high, Func<int, bool> P)
{
    int l = low - 1;      // P(l) is false
    int r = high + 1;     // P(r) is true
    while (r - l > 1)
    {
        int mid = l + (r - l) / 2;       // lower mid
        if (P(mid))
            r = mid;
        else
            l = mid;
    }
    return r;
}

// Returns the last index x in [low, high] where P(x) is true.
// If all false, returns low - 1.
int LastTrue(int low, int high, Func<int, bool> P)
{
    int l = low - 1;      // P(l) is true
    int r = high + 1;     // P(r) is false
    while (r - l > 1)
    {
        int mid = l + (r - l + 1) / 2;   // upper mid
        if (P(mid))
            l = mid;
        else
            r = mid;
    }
    return l;
}

int LowerBound(List<int> a, int target)
{
    return FirstTrue(0, a.Count - 1, i => a[i] >= target);
}

int UpperBound(List<int> a, int target)
{
    return FirstTrue(0, a.Count - 1, i => a[i] > target);
}

var a = new List<int> { 1, 3, 5, 7, 9 };

void Assert(bool condition)
{
    if (!condition)
    {
        Console.WriteLine("Test failed");
        Environment.Exit(1);
    }
}

Assert(LowerBound(a, 5) == 2);
Assert(LowerBound(a, 6) == 3);
Assert(LowerBound(a, 10) == a.Count);   // 5

Assert(UpperBound(a, 5) == 3);
Assert(UpperBound(a, 0) == 0);
Assert(UpperBound(a, 9) == a.Count);

// last_true: last element <= 5
int lastLe = LastTrue(0, a.Count - 1, i => a[i] <= 5);
Assert(lastLe == 2);

// first_true: smallest x with x*x >= 25
int ans = FirstTrue(0, 10, x => x * x >= 25);
Assert(ans == 5);

Console.WriteLine("All binary search tests passed.");