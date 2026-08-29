#include <vector>
#include <cassert>
#include <iostream>

std::vector<int> eratosthenesSieve(int n) {
    std::vector<bool> isPrime(n+1, true);
    isPrime[0] = isPrime[1] = false;
    for (int i = 2; i * i <= n; ++i) {
        if (isPrime[i]) {
            for (int j = i * i; j <= n; j += i) {
                isPrime[j] = false;
            }
        }
    }
    std::vector<int> primes;
    for (int i = 2; i <=n; ++i) {
        if (isPrime[i]) {
            primes.push_back(i);
        }
    }
    return primes;
}

int main() {

    assert(eratosthenesSieve(0).empty());
    assert(eratosthenesSieve(1).empty());

    std::vector<int> expected = {2, 3, 5, 7};
    assert(eratosthenesSieve(10) == expected);

    std::vector<int> primes_20 = eratosthenesSieve(20);
    assert(primes_20.size() == 8);
    assert(primes_20.back() == 19);

    assert(eratosthenesSieve(100).size() == 25);
    std::cout << "Eratosthenes sieve tests passed.\n";
    return 0;
}