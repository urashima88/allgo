using System;
using System.Collections.Generic;

List<long> BuildPrefixSum1D(List<int> a)
{
    int n = a.Count;
    List<long> pref = new List<long>(new long[n + 1]);
    for (int i = 0; i < n; i++)
        pref[i + 1] = pref[i] + a[i];
    return pref;
}

long QueryPrefixSum1D(List<long> pref, int l, int r)
{
    return pref[r + 1] - pref[l];
}

void Test(Func<bool> condition, string description)
{
    if (!condition())
    {
        Console.WriteLine($"Test failed: {description}");
        Environment.Exit(1);
    }
}

List<int> a = new List<int> { 3, 1, 4, 1, 5 };
List<long> pref = BuildPrefixSum1D(a);

Test(() => QueryPrefixSum1D(pref, 1, 3) == 6, "Sum from index 1 to 3 should be 6");
Test(() => QueryPrefixSum1D(pref, 0, 4) == 14, "Sum from index 0 to 4 should be 14");

Console.WriteLine("1D prefix sum examples passed.");