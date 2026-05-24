#include <vector>
#include <iostream>
#include <cassert>

using namespace std;

using vec1i = vector<int>;
using vec2i = vector<vector<int>>;
using vec3i = vector<vector<vector<int>>>;
using vec4i = vector<vector<vector<vector<int>>>>;
using vec5i = vector<vector<vector<vector<vector<int>>>>>;

using vec1ll = vector<long long>;
using vec2ll = vector<vector<long long>>;
using vec3ll = vector<vector<vector<long long>>>;
using vec4ll = vector<vector<vector<vector<long long>>>>;
using vec5ll = vector<vector<vector<vector<vector<long long>>>>>;

// Standard implementation
vec5ll buildPrefixSum5D(const vec5i& a) {
    int n1 = a.size();
    int n2 = a[0].size();
    int n3 = a[0][0].size();
    int n4 = a[0][0][0].size();
    int n5 = a[0][0][0][0].size();

    vec5ll pref(n1 + 1, vec4ll(n2 + 1, vec3ll(n3 + 1, vec2ll(n4 + 1, vec1ll(n5 + 1, 0LL)))));

    for (int i1 = 1; i1 <= n1; ++i1) {
        for (int i2 = 1; i2 <= n2; ++i2) {
            for (int i3 = 1; i3 <= n3; ++i3) {
                for (int i4 = 1; i4 <= n4; ++i4) {
                    for (int i5 = 1; i5 <= n5; ++i5) {
                        pref[i1][i2][i3][i4][i5] = a[i1-1][i2-1][i3-1][i4-1][i5-1]
                            + pref[i1-1][i2][i3][i4][i5]
                            + pref[i1][i2-1][i3][i4][i5]
                            + pref[i1][i2][i3-1][i4][i5]
                            + pref[i1][i2][i3][i4-1][i5]
                            + pref[i1][i2][i3][i4][i5-1]

                            - pref[i1-1][i2-1][i3][i4][i5]
                            - pref[i1-1][i2][i3-1][i4][i5]
                            - pref[i1-1][i2][i3][i4-1][i5]
                            - pref[i1-1][i2][i3][i4][i5-1]
                            - pref[i1][i2-1][i3-1][i4][i5]
                            - pref[i1][i2-1][i3][i4-1][i5]
                            - pref[i1][i2-1][i3][i4][i5-1]
                            - pref[i1][i2][i3-1][i4-1][i5]
                            - pref[i1][i2][i3-1][i4][i5-1]
                            - pref[i1][i2][i3][i4-1][i5-1]

                            + pref[i1-1][i2-1][i3-1][i4][i5]
                            + pref[i1-1][i2-1][i3][i4-1][i5]
                            + pref[i1-1][i2-1][i3][i4][i5-1]
                            + pref[i1-1][i2][i3-1][i4-1][i5]
                            + pref[i1-1][i2][i3-1][i4][i5-1]
                            + pref[i1-1][i2][i3][i4-1][i5-1]
                            + pref[i1][i2-1][i3-1][i4-1][i5]
                            + pref[i1][i2-1][i3-1][i4][i5-1]
                            + pref[i1][i2-1][i3][i4-1][i5-1]
                            + pref[i1][i2][i3-1][i4-1][i5-1]

                            - pref[i1-1][i2-1][i3-1][i4-1][i5]
                            - pref[i1-1][i2-1][i3-1][i4][i5-1]
                            - pref[i1-1][i2-1][i3][i4-1][i5-1]
                            - pref[i1-1][i2][i3-1][i4-1][i5-1]
                            - pref[i1][i2-1][i3-1][i4-1][i5-1]

                            + pref[i1-1][i2-1][i3-1][i4-1][i5-1];
                    }
                }
            }
        }
    }
    return pref;
}

// Standard implementation 
// left borders - inclusive, right - exclusive
long long queryPrefixSum5D(
    const vec5ll& pref,
    int l1, int l2, int l3, int l4, int l5,
    int r1, int r2, int r3, int r4, int r5
) {
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

// Short implementation (cycle by masks)
vec5ll buildPrefixSum5DShort(const vec5i& a) {
    int n1 = a.size();
    int n2 = a[0].size();
    int n3 = a[0][0].size();
    int n4 = a[0][0][0].size();
    int n5 = a[0][0][0][0].size();

    vec5ll pref(n1 + 1, vec4ll(n2 + 1, vec3ll(n3 + 1, vec2ll(n4 + 1, vec1ll(n5 + 1, 0LL)))));

    for (int i1 = 1; i1 <= n1; ++i1) {
        for (int i2 = 1; i2 <= n2; ++i2) {
            for (int i3 = 1; i3 <= n3; ++i3) {
                for (int i4 = 1; i4 <= n4; ++i4) {
                    for (int i5 = 1; i5 <= n5; ++i5) {
                        // Coordinates of current cell
                        int idx[5] = {i1, i2, i3, i4, i5};
                        // Sum over all masks: each bit indicates whether we move
                        // one step back in that dimension (i.e., use i_k - 1).
                        // The sign is + for even number of steps back (even popcount),
                        // and - for odd popcount. This is the inclusion–exclusion principle.
                        long long value = 0;
                        for (int mask = 0; mask < 32; ++mask) {
                            int cur[5];
                            for (int d = 0; d < 5; ++d) {
                                cur[d] = idx[d] - ((mask >> d) & 1);
                            }
                            long long term = pref[cur[0]][cur[1]][cur[2]][cur[3]][cur[4]];
                            if (__builtin_popcount(mask) % 2 == 1)
                                value += term;
                            else
                                value -= term;
                        }
                        pref[i1][i2][i3][i4][i5] = value + a[i1-1][i2-1][i3-1][i4-1][i5-1];
                    }
                }
            }
        }
    }
    return pref;
}

// Short implementation (cycle by masks)
// left borders - inclusive, right - exclusive
long long queryPrefixSum5DShort(
    const vector<vector<vector<vector<vector<long long>>>>>& pref,
    int l1, int l2, int l3, int l4, int l5,
    int r1, int r2, int r3, int r4, int r5
) {
    long long sum = 0;
    // There are 2^5 = 32 corners of the 5D box.
    // Each corner is defined by a mask: bit d = 0 means right bound r_d, bit = 1 means left bound l_d.
    // Sign is + if the number of left bounds (popcount) is even, - otherwise.
    for (int mask = 0; mask < 32; ++mask) {
        int idx[5] = {
            (mask & 1) ? l1 : r1,
            (mask & 2) ? l2 : r2,
            (mask & 4) ? l3 : r3,
            (mask & 8) ? l4 : r4,
            (mask & 16) ? l5 : r5
        };
        long long corner = pref[idx[0]][idx[1]][idx[2]][idx[3]][idx[4]];
        if (__builtin_popcount(mask) % 2 == 0)
            sum += corner;
        else
            sum -= corner;
    }
    return sum;
}

int main() {
    const int n1 = 2, n2 = 2, n3 = 2, n4 = 2, n5 = 2;
    vec5i a(n1, vec4i(n2, vec3i(n3, vec2i(n4, vec1i(n5, 0)))));

    int val = 1;
    for (int i1 = 0; i1 < n1; ++i1)
        for (int i2 = 0; i2 < n2; ++i2)
            for (int i3 = 0; i3 < n3; ++i3)
                for (int i4 = 0; i4 < n4; ++i4)
                    for (int i5 = 0; i5 < n5; ++i5)
                        a[i1][i2][i3][i4][i5] = val++;

    vec5ll pref = buildPrefixSum5D(a);
    {
        int q1[] = {1,1,1,1,1, 1,1,1,1,1};
        int l1 = q1[0]-1, l2 = q1[1]-1, l3 = q1[2]-1, l4 = q1[3]-1, l5 = q1[4]-1;
        int r1 = q1[5], r2 = q1[6], r3 = q1[7], r4 = q1[8], r5 = q1[9];
        long long res = queryPrefixSum5D(pref, l1,l2,l3,l4,l5, r1,r2,r3,r4,r5);
        assert(res == 1);
    }
    {
        int q2[] = {1,1,1,1,1, 2,2,2,2,2};
        int l1 = q2[0]-1, l2 = q2[1]-1, l3 = q2[2]-1, l4 = q2[3]-1, l5 = q2[4]-1;
        int r1 = q2[5], r2 = q2[6], r3 = q2[7], r4 = q2[8], r5 = q2[9];
        long long res = queryPrefixSum5D(pref, l1,l2,l3,l4,l5, r1,r2,r3,r4,r5);
        assert(res == 528);
    }
    {
        int q3[] = {1,2,1,2,1, 1,2,1,2,1};
        int l1 = q3[0]-1, l2 = q3[1]-1, l3 = q3[2]-1, l4 = q3[3]-1, l5 = q3[4]-1;
        int r1 = q3[5], r2 = q3[6], r3 = q3[7], r4 = q3[8], r5 = q3[9];
        long long res = queryPrefixSum5D(pref, l1,l2,l3,l4,l5, r1,r2,r3,r4,r5);
        assert(res == 11);
    }

    cout << "All 5D tests passed for standard implementation.\n";

    vec5ll pref_short = buildPrefixSum5DShort(a);
    {
        int q1[] = {1,1,1,1,1, 1,1,1,1,1};
        int l1 = q1[0]-1, l2 = q1[1]-1, l3 = q1[2]-1, l4 = q1[3]-1, l5 = q1[4]-1;
        int r1 = q1[5], r2 = q1[6], r3 = q1[7], r4 = q1[8], r5 = q1[9];
        long long res = queryPrefixSum5DShort(pref_short, l1,l2,l3,l4,l5, r1,r2,r3,r4,r5);
        assert(res == 1);
    }
    {
        int q2[] = {1,1,1,1,1, 2,2,2,2,2};
        int l1 = q2[0]-1, l2 = q2[1]-1, l3 = q2[2]-1, l4 = q2[3]-1, l5 = q2[4]-1;
        int r1 = q2[5], r2 = q2[6], r3 = q2[7], r4 = q2[8], r5 = q2[9];
        long long res = queryPrefixSum5DShort(pref_short, l1,l2,l3,l4,l5, r1,r2,r3,r4,r5);
        assert(res == 528);
    }
    {
        int q3[] = {1,2,1,2,1, 1,2,1,2,1};
        int l1 = q3[0]-1, l2 = q3[1]-1, l3 = q3[2]-1, l4 = q3[3]-1, l5 = q3[4]-1;
        int r1 = q3[5], r2 = q3[6], r3 = q3[7], r4 = q3[8], r5 = q3[9];
        long long res = queryPrefixSum5DShort(pref_short, l1,l2,l3,l4,l5, r1,r2,r3,r4,r5);
        assert(res == 11);
    }

    cout << "All 5D tests passed for short implementation.\n";
}