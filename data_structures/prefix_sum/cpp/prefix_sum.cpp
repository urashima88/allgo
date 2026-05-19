#include <vector>

using namespace std;

vector<long long> build_prefix_sum(const vector<int>& a) {
    int n = a.size();
    vector<long long> pref(n+1, 0);
    for (int i = 0; i < n; ++i) {
        pref[i+1] = pref[i] + a[i];
    }
    return pref;
}

long long query_sum(const vector<long long>& pref, int l, int r) {
    return pref[r+1] - pref[l];
}