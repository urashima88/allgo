using System;
using System.Collections.Generic;
using System.Linq;

// Computes all-pairs shortest paths using Floyd-Warshall.
// If directed = false, edges are treated as undirected.
List<List<long>> FloydWarshall(int n, List<(int u, int v, int w)> edges, bool directed = false)
{
    const long INF = long.MaxValue / 4;
    List<List<long>> dist = new List<List<long>>();
    for (int i = 0; i < n; i++)
    {
        List<long> row = Enumerable.Repeat(INF, n).ToList();
        row[i] = 0;
        dist.Add(row);
    }

    foreach ((int u, int v, int w) in edges)
    {
        if (w < dist[u][v])
        {
            dist[u][v] = w;
            if (!directed)
                dist[v][u] = w;
        }
    }

    for (int k = 0; k < n; k++)
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (dist[i][k] + dist[k][j] < dist[i][j])
                    dist[i][j] = dist[i][k] + dist[k][j];

    return dist;
}

// Same algorithm, also builds the "next" matrix for path reconstruction.
(List<List<long>> dist, List<List<int>> next) FloydWarshallWithPath(
    int n, List<(int u, int v, int w)> edges, bool directed = false)
{
    const long INF = long.MaxValue / 4;
    List<List<long>> dist = new List<List<long>>();
    List<List<int>> nextVertex = new List<List<int>>();

    for (int i = 0; i < n; i++)
    {
        List<long> row = Enumerable.Repeat(INF, n).ToList();
        List<int> nextRow = Enumerable.Repeat(-1, n).ToList();
        row[i] = 0;
        nextRow[i] = i;
        dist.Add(row);
        nextVertex.Add(nextRow);
    }

    foreach ((int u, int v, int w) in edges)
    {
        if (w < dist[u][v])
        {
            dist[u][v] = w;
            nextVertex[u][v] = v;
            if (!directed)
            {
                dist[v][u] = w;
                nextVertex[v][u] = u;
            }
        }
    }

    for (int k = 0; k < n; k++)
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (dist[i][k] + dist[k][j] < dist[i][j])
                {
                    dist[i][j] = dist[i][k] + dist[k][j];
                    nextVertex[i][j] = nextVertex[i][k];
                }

    return (dist, nextVertex);
}

// Reconstructs the shortest path from u to v using the next matrix.
// Returns an empty list if unreachable.
List<int> ReconstructPath(int u, int v, List<List<int>> nextVertex)
{
    if (nextVertex[u][v] == -1)
        return new List<int>();

    List<int> path = new List<int> { u };
    while (u != v)
    {
        u = nextVertex[u][v];
        path.Add(u);
    }
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

int n = 4;
var edges = new List<(int u, int v, int w)>
{
    (0, 1, 3),
    (1, 2, 2),
    (2, 3, 5),
    (3, 0, 8)
};

List<List<long>> dist = FloydWarshall(n, edges, directed: false);
Assert(dist[0][0] == 0);
Assert(dist[0][1] == 3);
Assert(dist[0][2] == 5);
Assert(dist[0][3] == 8);
Assert(dist[1][3] == 7);

(List<List<long>> dist2, List<List<int>> nextVertex) = FloydWarshallWithPath(n, edges, directed: false);
List<int> path = ReconstructPath(0, 3, nextVertex);
Assert(path.SequenceEqual(new[] { 0, 3 }));
List<int> path2 = ReconstructPath(1, 3, nextVertex);
Assert(path2.SequenceEqual(new[] { 1, 2, 3 }));

// Directed graph with a negative cycle: 0 -> 1 -> 2 -> 0
var negEdges = new List<(int u, int v, int w)>
{
    (0, 1, 1),
    (1, 2, -3),
    (2, 0, 1)
};
List<List<long>> negDist = FloydWarshall(3, negEdges, directed: true);
bool hasNegativeCycle = false;
for (int i = 0; i < 3; i++)
    if (negDist[i][i] < 0)
        hasNegativeCycle = true;
Assert(hasNegativeCycle);

Console.WriteLine("All Floyd-Warshall tests passed.");