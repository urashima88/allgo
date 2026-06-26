using System;
using System.Collections.Generic;

List<int> EratosthenesSieve(int n)
{
    if (n < 2)
        return new List<int>();

    bool[] isPrime = new bool[n + 1];
    for (int i = 2; i <= n; i++)
        isPrime[i] = true;

    for (int i = 2; i * i <= n; i++)
    {
        if (isPrime[i])
        {
            for (int j = i * i; j <= n; j += i)
                isPrime[j] = false;
        }
    }

    List<int> primes = new List<int>();
    for (int i = 2; i <= n; i++)
        if (isPrime[i])
            primes.Add(i);

    return primes;
}

void Test(Func<bool> condition, string description)
{
    if (!condition())
    {
        Console.WriteLine($"Test failed: {description}");
        Environment.Exit(1);
    }
}

Test(() => EratosthenesSieve(0).Count == 0, "n=0 returns empty");
Test(() => EratosthenesSieve(1).Count == 0, "n=1 returns empty");

List<int> primes10 = EratosthenesSieve(10);
Test(() => primes10.Count == 4
          && primes10[0] == 2
          && primes10[1] == 3
          && primes10[2] == 5
          && primes10[3] == 7,
          "primes up to 10");

List<int> primes100 = EratosthenesSieve(100);
Test(() => primes100.Count == 25, "25 primes up to 100");

Console.WriteLine("All tests passed.");