from typing import List

def eratosthenes_sieve(n: int) -> List[int]:
    if n < 2:
        return []
    is_prime = [True] * (n + 1)
    is_prime[0] = is_prime[1] = False
    for i in range(2, int(n ** 0.5) + 1):
        if is_prime[i]:
            for j in range(i * i, n + 1, i):
                is_prime[j] = False
    return [i for i in range(2, n + 1) if is_prime[i]]

if __name__ == "__main__":
    assert eratosthenes_sieve(0) == []
    assert eratosthenes_sieve(1) == []
    assert eratosthenes_sieve(10) == [2, 3, 5, 7]
    primes_20 = eratosthenes_sieve(20)
    assert len(primes_20) == 8 and primes_20[-1] == 19
    assert len(eratosthenes_sieve(100)) == 25
    print("Eratosthenes sieve tests passed.")