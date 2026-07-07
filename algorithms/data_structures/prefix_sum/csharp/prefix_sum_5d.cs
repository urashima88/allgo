using System;
using System.Collections.Generic;
using System.Numerics;


List<List<List<List<List<long>>>>> BuildPrefixSum5D(List<List<List<List<List<int>>>>> a)
{
    int n1 = a.Count;
    int n2 = a[0].Count;
    int n3 = a[0][0].Count;
    int n4 = a[0][0][0].Count;
    int n5 = a[0][0][0][0].Count;

    // pref[n1+1][n2+1][n3+1][n4+1][n5+1]
    var pref = new List<List<List<List<List<long>>>>>(n1 + 1);
    for (int i1 = 0; i1 <= n1; i1++)
    {
        var dim2 = new List<List<List<List<long>>>>(n2 + 1);
        for (int i2 = 0; i2 <= n2; i2++)
        {
            var dim3 = new List<List<List<long>>>(n3 + 1);
            for (int i3 = 0; i3 <= n3; i3++)
            {
                var dim4 = new List<List<long>>(n4 + 1);
                for (int i4 = 0; i4 <= n4; i4++)
                {
                    var dim5 = new List<long>(new long[n5 + 1]);
                    dim4.Add(dim5);
                }
                dim3.Add(dim4);
            }
            dim2.Add(dim3);
        }
        pref.Add(dim2);
    }

    for (int i1 = 1; i1 <= n1; i1++)
        for (int i2 = 1; i2 <= n2; i2++)
            for (int i3 = 1; i3 <= n3; i3++)
                for (int i4 = 1; i4 <= n4; i4++)
                    for (int i5 = 1; i5 <= n5; i5++)
                    {
                        pref[i1][i2][i3][i4][i5] =
                            a[i1 - 1][i2 - 1][i3 - 1][i4 - 1][i5 - 1]
                            + pref[i1 - 1][i2][i3][i4][i5]
                            + pref[i1][i2 - 1][i3][i4][i5]
                            + pref[i1][i2][i3 - 1][i4][i5]
                            + pref[i1][i2][i3][i4 - 1][i5]
                            + pref[i1][i2][i3][i4][i5 - 1]

                            - pref[i1 - 1][i2 - 1][i3][i4][i5]
                            - pref[i1 - 1][i2][i3 - 1][i4][i5]
                            - pref[i1 - 1][i2][i3][i4 - 1][i5]
                            - pref[i1 - 1][i2][i3][i4][i5 - 1]
                            - pref[i1][i2 - 1][i3 - 1][i4][i5]
                            - pref[i1][i2 - 1][i3][i4 - 1][i5]
                            - pref[i1][i2 - 1][i3][i4][i5 - 1]
                            - pref[i1][i2][i3 - 1][i4 - 1][i5]
                            - pref[i1][i2][i3 - 1][i4][i5 - 1]
                            - pref[i1][i2][i3][i4 - 1][i5 - 1]

                            + pref[i1 - 1][i2 - 1][i3 - 1][i4][i5]
                            + pref[i1 - 1][i2 - 1][i3][i4 - 1][i5]
                            + pref[i1 - 1][i2 - 1][i3][i4][i5 - 1]
                            + pref[i1 - 1][i2][i3 - 1][i4 - 1][i5]
                            + pref[i1 - 1][i2][i3 - 1][i4][i5 - 1]
                            + pref[i1 - 1][i2][i3][i4 - 1][i5 - 1]
                            + pref[i1][i2 - 1][i3 - 1][i4 - 1][i5]
                            + pref[i1][i2 - 1][i3 - 1][i4][i5 - 1]
                            + pref[i1][i2 - 1][i3][i4 - 1][i5 - 1]
                            + pref[i1][i2][i3 - 1][i4 - 1][i5 - 1]

                            - pref[i1 - 1][i2 - 1][i3 - 1][i4 - 1][i5]
                            - pref[i1 - 1][i2 - 1][i3 - 1][i4][i5 - 1]
                            - pref[i1 - 1][i2 - 1][i3][i4 - 1][i5 - 1]
                            - pref[i1 - 1][i2][i3 - 1][i4 - 1][i5 - 1]
                            - pref[i1][i2 - 1][i3 - 1][i4 - 1][i5 - 1]

                            + pref[i1 - 1][i2 - 1][i3 - 1][i4 - 1][i5 - 1];
                    }
    return pref;
}

long QueryPrefixSum5D(
    List<List<List<List<List<long>>>>> pref,
    int l1, int l2, int l3, int l4, int l5,
    int r1, int r2, int r3, int r4, int r5)
{
    return pref[r1][r2][r3][r4][r5]
        - pref[l1][r2][r3][r4][r5]
        - pref[r1][l2][r3][r4][r5]
        - pref[r1][r2][l3][r4][r5]
        - pref[r1][r2][r3][l4][r5]
        - pref[r1][r2][r3][r4][l5]

        + pref[l1][l2][r3][r4][r5]
        + pref[l1][r2][l3][r4][r5]
        + pref[l1][r2][r3][l4][r5]
        + pref[l1][r2][r3][r4][l5]
        + pref[r1][l2][l3][r4][r5]
        + pref[r1][l2][r3][l4][r5]
        + pref[r1][l2][r3][r4][l5]
        + pref[r1][r2][l3][l4][r5]
        + pref[r1][r2][l3][r4][l5]
        + pref[r1][r2][r3][l4][l5]

        - pref[l1][l2][l3][r4][r5]
        - pref[l1][l2][r3][l4][r5]
        - pref[l1][l2][r3][r4][l5]
        - pref[l1][r2][l3][l4][r5]
        - pref[l1][r2][l3][r4][l5]
        - pref[l1][r2][r3][l4][l5]
        - pref[r1][l2][l3][l4][r5]
        - pref[r1][l2][l3][r4][l5]
        - pref[r1][l2][r3][l4][l5]
        - pref[r1][r2][l3][l4][l5]

        + pref[l1][l2][l3][l4][r5]
        + pref[l1][l2][l3][r4][l5]
        + pref[l1][l2][r3][l4][l5]
        + pref[l1][r2][l3][l4][l5]
        + pref[r1][l2][l3][l4][l5]

        - pref[l1][l2][l3][l4][l5];
}


List<List<List<List<List<long>>>>> BuildPrefixSum5DShort(List<List<List<List<List<int>>>>> a)
{
    int n1 = a.Count, n2 = a[0].Count, n3 = a[0][0].Count, n4 = a[0][0][0].Count, n5 = a[0][0][0][0].Count;

    var pref = new List<List<List<List<List<long>>>>>(n1 + 1);
    for (int i1 = 0; i1 <= n1; i1++)
    {
        var dim2 = new List<List<List<List<long>>>>(n2 + 1);
        for (int i2 = 0; i2 <= n2; i2++)
        {
            var dim3 = new List<List<List<long>>>(n3 + 1);
            for (int i3 = 0; i3 <= n3; i3++)
            {
                var dim4 = new List<List<long>>(n4 + 1);
                for (int i4 = 0; i4 <= n4; i4++)
                {
                    var dim5 = new List<long>(new long[n5 + 1]);
                    dim4.Add(dim5);
                }
                dim3.Add(dim4);
            }
            dim2.Add(dim3);
        }
        pref.Add(dim2);
    }

    for (int i1 = 1; i1 <= n1; i1++)
        for (int i2 = 1; i2 <= n2; i2++)
            for (int i3 = 1; i3 <= n3; i3++)
                for (int i4 = 1; i4 <= n4; i4++)
                    for (int i5 = 1; i5 <= n5; i5++)
                    {
                        int[] idx = { i1, i2, i3, i4, i5 };
                        long value = 0;
                        for (int mask = 1; mask < 32; mask++)
                        {
                            int[] cur = new int[5];
                            for (int d = 0; d < 5; d++)
                                cur[d] = idx[d] - ((mask >> d) & 1);
                            long term = pref[cur[0]][cur[1]][cur[2]][cur[3]][cur[4]];
                            if (BitOperations.PopCount((uint)mask) % 2 == 1)
                                value += term;
                            else
                                value -= term;
                        }
                        pref[i1][i2][i3][i4][i5] = value + a[i1 - 1][i2 - 1][i3 - 1][i4 - 1][i5 - 1];
                    }
    return pref;
}

long QueryPrefixSum5DShort(
    List<List<List<List<List<long>>>>> pref,
    int l1, int l2, int l3, int l4, int l5,
    int r1, int r2, int r3, int r4, int r5)
{
    long sum = 0;
    for (int mask = 0; mask < 32; mask++)
    {
        int i1 = (mask & 1) != 0 ? l1 : r1;
        int i2 = (mask & 2) != 0 ? l2 : r2;
        int i3 = (mask & 4) != 0 ? l3 : r3;
        int i4 = (mask & 8) != 0 ? l4 : r4;
        int i5 = (mask & 16) != 0 ? l5 : r5;
        long corner = pref[i1][i2][i3][i4][i5];
        if (BitOperations.PopCount((uint)mask) % 2 == 0)
            sum += corner;
        else
            sum -= corner;
    }
    return sum;
}


void Test(Func<bool> condition, string description)
{
    if (!condition())
    {
        Console.WriteLine($"Test failed: {description}");
        Environment.Exit(1);
    }
}

var a = new List<List<List<List<List<int>>>>>();
for (int i1 = 0; i1 < 2; i1++)
{
    var dim2 = new List<List<List<List<int>>>>();
    for (int i2 = 0; i2 < 2; i2++)
    {
        var dim3 = new List<List<List<int>>>();
        for (int i3 = 0; i3 < 2; i3++)
        {
            var dim4 = new List<List<int>>();
            for (int i4 = 0; i4 < 2; i4++)
            {
                var dim5 = new List<int>();
                for (int i5 = 0; i5 < 2; i5++)
                    dim5.Add(0);
                dim4.Add(dim5);
            }
            dim3.Add(dim4);
        }
        dim2.Add(dim3);
    }
    a.Add(dim2);
}
int val = 1;
for (int i1 = 0; i1 < 2; i1++)
    for (int i2 = 0; i2 < 2; i2++)
        for (int i3 = 0; i3 < 2; i3++)
            for (int i4 = 0; i4 < 2; i4++)
                for (int i5 = 0; i5 < 2; i5++)
                    a[i1][i2][i3][i4][i5] = val++;


var pref = BuildPrefixSum5D(a);
Test(() => QueryPrefixSum5D(pref, 0,0,0,0,0, 1,1,1,1,1) == 1, "Standard query 1");
Test(() => QueryPrefixSum5D(pref, 0,0,0,0,0, 2,2,2,2,2) == 528, "Standard query 2");
Test(() => QueryPrefixSum5D(pref, 0,1,0,1,0, 1,2,1,2,1) == 11, "Standard query 3");

Console.WriteLine("All 5D tests passed for standard implementation.");

var prefShort = BuildPrefixSum5DShort(a);
Test(() => QueryPrefixSum5DShort(prefShort, 0,0,0,0,0, 1,1,1,1,1) == 1, "Short query 1");
Test(() => QueryPrefixSum5DShort(prefShort, 0,0,0,0,0, 2,2,2,2,2) == 528, "Short query 2");
Test(() => QueryPrefixSum5DShort(prefShort, 0,1,0,1,0, 1,2,1,2,1) == 11, "Short query 3");

Console.WriteLine("All 5D tests passed for short implementation.");