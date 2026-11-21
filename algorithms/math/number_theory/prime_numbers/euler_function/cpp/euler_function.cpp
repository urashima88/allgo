// Euler's function φ(n)
// euler_phi(n): O(sqrt(n)) for one number
// phi_sieve(n): O(n log log n), returns φ(0..n)

#include <cassert>
#include <vector>
using namespace std;

int eulerPhi(int n) {
    int ans = n;
    for (int i = 2; (long long)i * i <= n; ++i) {
        if (n % i == 0) {
            ans -= ans / i;          // equivalent to ans = ans / i * (i - 1)
            while (n % i == 0) n /= i;
        }
    }
    if (n > 1) ans -= ans / n;
    return ans;
}

vector<int> phiSieve(int n) {
    vector<int> phi(n + 1);
    for (int i = 0; i <= n; ++i) phi[i] = i;
    for (int i = 2; i <= n; ++i) {
        if (phi[i] == i) {           // i is prime
            for (int j = i; j <= n; j += i) {
                phi[j] -= phi[j] / i;
            }
        }
    }
    return phi;
}

// ---------- Tests ----------
static int brutePhi(int n) {
    int cnt = 0;
    for (int k = 1; k <= n; ++k) {
        int a = k, b = n;
        while (b) { int t = a % b; a = b; b = t; }
        if (a == 1) ++cnt;
    }
    return cnt;
}

int main() {
    // small cases
    assert(eulerPhi(1) == 1);
    assert(eulerPhi(2) == 1);
    assert(eulerPhi(5) == 4);
    assert(eulerPhi(10) == 4);
    assert(eulerPhi(36) == 12);
    assert(eulerPhi(97) == 96);
    assert(eulerPhi(100) == 40);

    // stress test: single-number version vs brute force
    for (int n = 1; n <= 1000; ++n) {
        assert(eulerPhi(n) == brutePhi(n));
    }

    // sieve vs single
    vector<int> s = phiSieve(1000);
    for (int n = 1; n <= 1000; ++n) {
        assert(s[n] == eulerPhi(n));
    }

    return 0;
}