using System;
using System.Collections.Generic;

List<List<List<long>>> BuildPrefixSum3D(List<List<List<int>>> a)
{
    int n = a.Count;
    int m = a[0].Count;
    int p = a[0][0].Count;

    List<List<List<long>>> pref = new List<List<List<long>>>(n + 1);
    for (int i = 0; i <= n; i++)
    {
        List<List<long>> layer = new List<List<long>>(m + 1);
        for (int j = 0; j <= m; j++)
        {
            List<long> row = new List<long>(new long[p + 1]);
            layer.Add(row);
        }
        pref.Add(layer);
    }

    for (int i = 1; i <= n; i++)
    {
        for (int j = 1; j <= m; j++)
        {
            for (int k = 1; k <= p; k++)
            {
                pref[i][j][k] = a[i - 1][j - 1][k - 1]
                    + pref[i - 1][j][k]
                    + pref[i][j - 1][k]
                    + pref[i][j][k - 1]
                    - pref[i - 1][j - 1][k]
                    - pref[i - 1][j][k - 1]
                    - pref[i][j - 1][k - 1]
                    + pref[i - 1][j - 1][k - 1];
            }
        }
    }
    return pref;
}

// left borders inclusive, right borders exclusive
long QueryPrefixSum3D(List<List<List<long>>> pref, int lx, int ly, int lz, int rx, int ry, int rz)
{
    return pref[rx][ry][rz]
        - pref[lx][ry][rz] - pref[rx][ly][rz] - pref[rx][ry][lz]
        + pref[lx][ly][rz] + pref[lx][ry][lz] + pref[rx][ly][lz]
        - pref[lx][ly][lz];
}

void Test(Func<bool> condition, string description)
{
    if (!condition())
    {
        Console.WriteLine($"Test failed: {description}");
        Environment.Exit(1);
    }
}

List<List<List<int>>> a = new List<List<List<int>>>
{
    new List<List<int>>
    {
        new List<int> { 1, 2 },
        new List<int> { 3, 4 }
    },
    new List<List<int>>
    {
        new List<int> { 5, 6 },
        new List<int> { 7, 8 }
    }
};

List<List<List<long>>> pref = BuildPrefixSum3D(a);

Test(() => QueryPrefixSum3D(pref, 0, 0, 0, 2, 2, 2) == 36, "Total sum should be 36");
Test(() => QueryPrefixSum3D(pref, 1, 1, 1, 2, 2, 2) == 8,  "Single element (1,1,1) should be 8");
Test(() => QueryPrefixSum3D(pref, 0, 0, 0, 1, 1, 1) == 1,  "Corner (0,0,0)..(0,0,0) should be 1");
Test(() => QueryPrefixSum3D(pref, 0, 0, 0, 1, 2, 2) == 10, "First z-slab should be 10");

Console.WriteLine("3D prefix sum examples passed.");