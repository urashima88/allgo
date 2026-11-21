# Euler's function phi(n)
# euler_phi(n): O(sqrt(n)) for a single number
# phi_sieve(n): O(n log log n), returns phi(0..n)
import math


def euler_phi(n: int) -> int:
    ans = n
    i = 2
    while i * i <= n:
        if n % i == 0:
            ans -= ans // i          # equivalent to ans = ans // i * (i - 1)
            while n % i == 0:
                n //= i
        i += 1
    if n > 1:
        ans -= ans // n
    return ans


def phi_sieve(n: int) -> list[int]:
    phi = list(range(n + 1))
    for i in range(2, n + 1):
        if phi[i] == i:               # i is prime
            for j in range(i, n + 1, i):
                phi[j] -= phi[j] // i
    return phi


# ---------- Tests ----------
def brute_phi(n: int) -> int:
    return sum(1 for k in range(1, n + 1) if math.gcd(k, n) == 1)


def _run_tests() -> None:
    # small cases
    assert euler_phi(1) == 1
    assert euler_phi(2) == 1
    assert euler_phi(5) == 4
    assert euler_phi(10) == 4
    assert euler_phi(36) == 12
    assert euler_phi(97) == 96
    assert euler_phi(100) == 40

    # stress test: single-number version vs brute force
    for n in range(1, 1001):
        assert euler_phi(n) == brute_phi(n)

    # sieve vs single
    phi = phi_sieve(1000)
    for n in range(1, 1001):
        assert phi[n] == euler_phi(n)


if __name__ == "__main__":
    _run_tests()
    print("All euler_phi tests passed.")