using System;
using System.Collections.Generic;

List<List<long>> BuildPrefixSum2D(List<List<int>> a)
{
    int n = a.Count;
    int m = a[0].Count;
    List<List<long>> pref = new List<List<long>>(n + 1);
    for (int i = 0; i <= n; i++)
    {
        pref.Add(new List<long>(new long[m + 1]));
    }

    for (int i = 1; i <= n; i++)
    {
        for (int j = 1; j <= m; j++)
        {
            pref[i][j] = a[i - 1][j - 1]
                        + pref[i - 1][j]
                        + pref[i][j - 1]
                        - pref[i - 1][j - 1];
        }
    }
    return pref;
}

// left borders inclusive, right borders exclusive
long QueryPrefixSum2D(List<List<long>> pref, int lx, int ly, int rx, int ry)
{
    return pref[rx][ry] - pref[lx][ry] - pref[rx][ly] + pref[lx][ly];
}

void Test(Func<bool> condition, string description)
{
    if (!condition())
    {
        Console.WriteLine($"Test failed: {description}");
        Environment.Exit(1);
    }
}

List<List<int>> a = new List<List<int>>
{
    new List<int> { 1, 2, 3 },
    new List<int> { 4, 5, 6 },
    new List<int> { 7, 8, 9 }
};

List<List<long>> pref = BuildPrefixSum2D(a);

Test(() => QueryPrefixSum2D(pref, 0, 0, 3, 3) == 45, "Full sum should be 45");
Test(() => QueryPrefixSum2D(pref, 1, 1, 2, 2) == 5,  "Submatrix (1,1)-(1,1) should be 5");
Test(() => QueryPrefixSum2D(pref, 0, 1, 2, 3) == 16, "Submatrix cols 1..2, rows 0..1 should be 16");

Console.WriteLine("2D prefix sum examples passed.");