using System;
using System.Collections.Generic;
using System.Linq;

// Euler's function phi(n)
// Phi(n): O(sqrt(n)) for a single number
// PhiSieve(n): O(n log log n), returns phi(0..n)

int Phi(int n)
{
    int ans = n;
    for (int i = 2; (long)i * i <= n; i++)
    {
        if (n % i == 0)
        {
            ans -= ans / i;                // equivalent to ans = ans / i * (i - 1)
            while (n % i == 0) n /= i;
        }
    }
    if (n > 1) ans -= ans / n;
    return ans;
}

List<int> PhiSieve(int n)
{
    List<int> phi = Enumerable.Range(0, n + 1).ToList();
    for (int i = 2; i <= n; i++)
    {
        if (phi[i] == i)                   // i is prime
        {
            for (int j = i; j <= n; j += i)
                phi[j] -= phi[j] / i;
        }
    }
    return phi;
}

// ---------- Tests ----------
void Assert(bool condition)
{
    if (!condition)
    {
        Console.WriteLine("Test failed");
        Environment.Exit(1);
    }
}

int BrutePhi(int n)
{
    int cnt = 0;
    for (int k = 1; k <= n; k++)
    {
        int a = k, b = n;
        while (b != 0) { int t = a % b; a = b; b = t; }
        if (a == 1) cnt++;
    }
    return cnt;
}

// small cases
Assert(Phi(1) == 1);
Assert(Phi(2) == 1);
Assert(Phi(5) == 4);
Assert(Phi(10) == 4);
Assert(Phi(36) == 12);
Assert(Phi(97) == 96);
Assert(Phi(100) == 40);

// stress test: single-number version vs brute force
for (int i = 1; i <= 1000; i++)
    Assert(Phi(i) == BrutePhi(i));

// sieve vs single
List<int> phiArr = PhiSieve(1000);
for (int i = 1; i <= 1000; i++)
    Assert(phiArr[i] == Phi(i));

Console.WriteLine("All euler_phi tests passed.");