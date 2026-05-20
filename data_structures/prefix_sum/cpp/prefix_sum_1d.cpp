#include <vector>
#include <iostream>
#include <cassert>

using namespace std;

vector<long long> buildPrefixSum1D(const vector<int>& a) {
    int n = a.size();
    vector<long long> pref(n+1, 0);
    for (int i = 0; i < n; ++i) {
        pref[i+1] = pref[i] + a[i];
    }
    return pref;
}

long long queryPrefixSum1D(const vector<long long>& pref, int l, int r) {
    return pref[r+1] - pref[l];
} 

int main() {
    vector<int> a = {3, 1, 4, 1, 5};
    auto pref = buildPrefixSum1D(a);
    assert(queryPrefixSum1D(pref, 1, 3) == 6);
    assert(queryPrefixSum1D(pref, 0, 4) == 14);
    cout << "1D prefix sum examples passed." << endl;
}
