#include <iostream>
#include <vector>
#include <functional>
#include <cassert>

// Returns the first index x in [low, high] where P(x) is true.
// If all values are false, returns high + 1.
int firstTrue(int low, int high, const std::function<bool(int)>& P) {
    int l = low - 1;      // P(l) is false
    int r = high + 1;     // P(r) is true
    while (r - l > 1) {
        int mid = l + (r - l) / 2;   // lower mid
        if (P(mid))
            r = mid;
        else
            l = mid;
    }
    return r;  // first true
}

// Returns the last index x in [low, high] where P(x) is true.
// If all values are false, returns low - 1.
int lastTrue(int low, int high, const std::function<bool(int)>& P) {
    int l = low - 1;      // P(l) is true
    int r = high + 1;     // P(r) is false
    while (r - l > 1) {
        int mid = l + (r - l + 1) / 2;   // upper mid
        if (P(mid))
            l = mid;
        else
            r = mid;
    }
    return l;  // last true
}

// First index i such that a[i] >= target.
// Returns a.size() if no such element.
int lower_bound(const std::vector<int>& a, int target) {
    return firstTrue(0, (int)a.size() - 1,
        [&](int i) { return a[i] >= target; });
}

// First index i such that a[i] > target.
// Returns a.size() if no such element.
int upper_bound(const std::vector<int>& a, int target) {
    return firstTrue(0, (int)a.size() - 1,
        [&](int i) { return a[i] > target; });
}

int main() {
    std::vector<int> a = {1, 3, 5, 7, 9};

    assert(lower_bound(a, 5) == 2);
    assert(lower_bound(a, 6) == 3);
    assert(lower_bound(a, 10) == 5);   // a.size()

    assert(upper_bound(a, 5) == 3);
    assert(upper_bound(a, 0) == 0);
    assert(upper_bound(a, 9) == 5);

    // last_true: last element <= 5
    int last_le = lastTrue(0, (int)a.size() - 1,
        [&](int i) { return a[i] <= 5; });
    assert(last_le == 2);

    // first_true: smallest x with x*x >= 25
    int ans = firstTrue(0, 10,
        [](int x) { return x * x >= 25; });
    assert(ans == 5);

    std::cout << "All binary search tests passed.\n";
    return 0;
}