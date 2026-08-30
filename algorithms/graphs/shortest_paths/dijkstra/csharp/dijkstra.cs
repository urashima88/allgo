using System;
using System.Collections.Generic;
using System.Linq;

// Graph: adjacency list: graph[u] = List<(int v, int weight)>
(List<long> dist, List<int> parent) Dijkstra(List<List<(int v, int weight)>> graph, int start)
{
    int n = graph.Count;
    const long INF = long.MaxValue / 4;
    List<long> dist = Enumerable.Repeat(INF, n).ToList();
    List<int> parent = Enumerable.Repeat(-1, n).ToList();

    // PriorityQueue<(long distance, int vertex), long> min-heap by distance
    var pq = new PriorityQueue<(long dist, int vertex), long>();
    dist[start] = 0;
    pq.Enqueue((0, start), 0);

    while (pq.Count > 0)
    {
        (long d, int u) = pq.Dequeue();
        if (d != dist[u]) continue;

        foreach ((int v, int weight) in graph[u])
        {
            if (dist[u] + weight < dist[v])
            {
                dist[v] = dist[u] + weight;
                parent[v] = u;
                pq.Enqueue((dist[v], v), dist[v]);
            }
        }
    }

    return (dist, parent);
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

var graph = new List<List<(int v, int weight)>>
{
    new List<(int v, int weight)> { (1,4), (2,1) },
    new List<(int v, int weight)> { (0,4), (3,2) },
    new List<(int v, int weight)> { (0,1), (3,5), (4,3) },
    new List<(int v, int weight)> { (1,2), (2,5), (4,1) },
    new List<(int v, int weight)> { (2,3), (3,1) }
};

int start = 0;
(List<long> dist, List<int> parent) = Dijkstra(graph, start);

Assert(dist[0] == 0);
Assert(dist[1] == 4);
Assert(dist[2] == 1);
Assert(dist[3] == 5);
Assert(dist[4] == 4);

// Reconstruct path to 3
List<int> path = new List<int>();
int cur = 3;
while (cur != -1)
{
    path.Add(cur);
    cur = parent[cur];
}
path.Reverse();
Assert(path.SequenceEqual(new[] { 0, 2, 4, 3 }));

Console.WriteLine("All Dijkstra tests passed.");