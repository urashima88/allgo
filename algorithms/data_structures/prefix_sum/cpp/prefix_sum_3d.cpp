#include <vector>
#include <iostream>
#include <cassert>

using namespace std;

vector<vector<vector<long long>>> buildPrefixSum3D(const vector<vector<vector<int>>>& a) {
    int n = a.size();
    int m = a[0].size();
    int p = a[0][0].size();

    vector<vector<vector<long long>>> pref(
        n+1,
        vector<vector<long long>>(m+1, vector<long long>(p+1, 0))
    );

    for (int i = 1; i < n+1; ++i) {
        for (int j = 1; j < m+1; ++j) {
            for (int k = 1; k < p+1; ++k) {
                pref[i][j][k] = a[i-1][j-1][k-1]
                    + pref[i-1][j][k]
                    + pref[i][j-1][k]
                    + pref[i][j][k-1]
                    - pref[i-1][j-1][k]
                    - pref[i-1][j][k-1]
                    - pref[i][j-1][k-1]
                    + pref[i-1][j-1][k-1];
            }
        }
    }
    return pref;
}

// left borders - inclusive, right - exclusive
long long queryPrefixSum3D(
    const vector<vector<vector<long long>>>& pref,
    int lx, int ly, int lz,
    int rx, int ry, int rz
) {
    return pref[rx][ry][rz]
        - pref[lx][ry][rz] - pref[rx][ly][rz] - pref[rx][ry][lz]
        + pref[lx][ly][rz] + pref[lx][ry][lz] + pref[rx][ly][lz]
        - pref[lx][ly][lz];
}

int main() {
    vector<vector<vector<int>>> a = {
        {{1, 2}, {3, 4}},
        {{5, 6}, {7, 8}}
    };

    vector<vector<vector<long long>>> pref = buildPrefixSum3D(a);
    assert(queryPrefixSum3D(pref, 0, 0, 0, 2, 2, 2) == 36);
    assert(queryPrefixSum3D(pref, 1, 1, 1, 2, 2, 2) == 8);
    assert(queryPrefixSum3D(pref, 0, 0, 0, 1, 1, 1) == 1);
    assert(queryPrefixSum3D(pref, 0, 0, 0, 1, 2, 2) == 10);

    cout << "3D prefix sum examples passed.\n";
}