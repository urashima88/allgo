#include <vector>
#include <iostream>
#include <cassert>

using namespace std;

vector<vector<long long>> buildPrefixSum2D(const vector<vector<int>>& a) {
    int n = a.size(), m = a[0].size();
    vector<vector<long long>> pref(n+1, vector<long long>(m+1, 0));
    for (int i = 1; i <= n; ++i) {
        for (int j = 1; j <= m; ++j) {
            pref[i][j] = a[i-1][j-1] + pref[i-1][j] + pref[i][j-1] - pref[i-1][j-1];
        }
    }
    return pref;
}

// left borders - inclusive, right - exclusive
long long queryPrefixSum2D(const vector<vector<long long>>& pref, int lx, int ly, int rx, int ry) {
    return pref[rx][ry] - pref[lx][ry] - pref[rx][ly] + pref[lx][ly];
}

int main() {
    vector<vector<int>> a = {
        {1, 2, 3},
        {4, 5, 6},
        {7, 8, 9}
    };

    auto pref = buildPrefixSum2D(a);
    assert(queryPrefixSum2D(pref, 0, 0, 3, 3) == 45);
    assert(queryPrefixSum2D(pref, 1, 1, 2, 2) == 5);
    assert(queryPrefixSum2D(pref, 0, 1, 2, 3) == 16);
    cout << "2D prefix sum examples passed.\n"; 
}