using System;
using System.Collections.Generic;
using System.Linq;

// Returns shortest distances from start to all vertices.
// Unreachable vertices get distance -1.
List<int> BfsShortestDistances(List<List<int>> graph, int start)
{
    int n = graph.Count;
    List<int> dist = Enumerable.Repeat(-1, n).ToList();
    Queue<int> q = new Queue<int>();
    dist[start] = 0;
    q.Enqueue(start);

    while (q.Count > 0)
    {
        int u = q.Dequeue();
        foreach (int v in graph[u])
        {
            if (dist[v] == -1)
            {
                dist[v] = dist[u] + 1;
                q.Enqueue(v);
            }
        }
    }
    return dist;
}

// BFS that also stores parents.
(List<int> dist, List<int> parent) BfsWithParents(List<List<int>> graph, int start)
{
    int n = graph.Count;
    List<int> dist = Enumerable.Repeat(-1, n).ToList();
    List<int> parent = Enumerable.Repeat(-1, n).ToList();
    Queue<int> q = new Queue<int>();
    dist[start] = 0;
    q.Enqueue(start);

    while (q.Count > 0)
    {
        int u = q.Dequeue();
        foreach (int v in graph[u])
        {
            if (dist[v] == -1)
            {
                dist[v] = dist[u] + 1;
                parent[v] = u;
                q.Enqueue(v);
            }
        }
    }
    return (dist, parent);
}

// Reconstructs path from start to target using parent array.
List<int> ReconstructPath(int start, int target, List<int> parent)
{
    if (parent[target] == -1 && target != start)
        return new List<int>();
    List<int> path = new List<int>();
    int v = target;
    while (v != -1)
    {
        path.Add(v);
        v = parent[v];
    }
    path.Reverse();
    return path;
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

var graph = new List<List<int>>
{
    new List<int> { 1, 3 },
    new List<int> { 0, 2, 4 },
    new List<int> { 1 },
    new List<int> { 0, 4 },
    new List<int> { 1, 3 }
};

List<int> dist = BfsShortestDistances(graph, 0);
Assert(dist.SequenceEqual(new[] { 0, 1, 2, 1, 2 }));

(var dist2, var parent) = BfsWithParents(graph, 0);
List<int> pathTo2 = ReconstructPath(0, 2, parent);
Assert(pathTo2.SequenceEqual(new[] { 0, 1, 2 }));

Console.WriteLine("All BFS tests passed.");