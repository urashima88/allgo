using System;
using System.Collections.Generic;
using System.Linq;

bool IsBipartite(List<List<int>> graph)
{
    int n = graph.Count;
    int[] color = Enumerable.Repeat(-1, n).ToArray();

    for (int start = 0; start < n; start++)
    {
        if (color[start] != -1) continue;

        Queue<int> q = new Queue<int>();
        color[start] = 0;
        q.Enqueue(start);

        while (q.Count > 0)
        {
            int u = q.Dequeue();
            foreach (int v in graph[u])
            {
                if (color[v] == -1)
                {
                    color[v] = 1 - color[u];
                    q.Enqueue(v);
                }
                else if (color[v] == color[u])
                {
                    return false;
                }
            }
        }
    }
    return true;
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

// Bipartite graph: even cycle
var bipartiteGraph = new List<List<int>>
{
    new List<int> { 1, 3 },
    new List<int> { 0, 2 },
    new List<int> { 1, 3 },
    new List<int> { 2, 0 }
};
Assert(IsBipartite(bipartiteGraph) == true);

// Non-bipartite graph: triangle
var nonBipartiteGraph = new List<List<int>>
{
    new List<int> { 1, 2 },
    new List<int> { 0, 2 },
    new List<int> { 0, 1 }
};
Assert(IsBipartite(nonBipartiteGraph) == false);

// Disconnected mixed graph
var mixedGraph = new List<List<int>>
{
    new List<int> { 1 },
    new List<int> { 0 },
    new List<int> { 3, 4 },
    new List<int> { 2, 4 },
    new List<int> { 2, 3 }
};
Assert(IsBipartite(mixedGraph) == false);

Console.WriteLine("All bipartite tests passed.");