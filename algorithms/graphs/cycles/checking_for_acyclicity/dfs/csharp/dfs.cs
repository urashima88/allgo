using System;
using System.Collections.Generic;
using System.Linq;

// Undirected graph cycle detection
bool HasCycleUndirectedDfs(int u, int parent, List<List<int>> graph, bool[] visited)
{
    visited[u] = true;
    foreach (int v in graph[u])
    {
        if (!visited[v])
        {
            if (HasCycleUndirectedDfs(v, u, graph, visited))
                return true;
        }
        else if (v != parent)
        {
            return true;
        }
    }
    return false;
}

bool HasCycleUndirected(List<List<int>> graph)
{
    int n = graph.Count;
    bool[] visited = new bool[n];
    for (int i = 0; i < n; i++)
        if (!visited[i])
            if (HasCycleUndirectedDfs(i, -1, graph, visited))
                return true;
    return false;
}

// Directed graph cycle detection
bool HasCycleDirectedDfs(int u, List<List<int>> graph, int[] color)
{
    color[u] = 1; // gray
    foreach (int v in graph[u])
    {
        if (color[v] == 1)
            return true;
        else if (color[v] == 0)
            if (HasCycleDirectedDfs(v, graph, color))
                return true;
    }
    color[u] = 2; // black
    return false;
}

bool HasCycleDirected(List<List<int>> graph)
{
    int n = graph.Count;
    int[] color = new int[n]; // 0 = white, 1 = gray, 2 = black
    for (int i = 0; i < n; i++)
        if (color[i] == 0)
            if (HasCycleDirectedDfs(i, graph, color))
                return true;
    return false;
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

// Undirected cycle (triangle)
var undirectedCyclic = new List<List<int>>
{
    new List<int> {1,2},
    new List<int> {0,2},
    new List<int> {0,1}
};
Assert(HasCycleUndirected(undirectedCyclic) == true);

// Undirected tree
var undirectedAcyclic = new List<List<int>>
{
    new List<int> {1},
    new List<int> {0,2},
    new List<int> {1}
};
Assert(HasCycleUndirected(undirectedAcyclic) == false);

// Directed cycle
var directedCyclic = new List<List<int>>
{
    new List<int> {1},
    new List<int> {2},
    new List<int> {0}
};
Assert(HasCycleDirected(directedCyclic) == true);

// Directed acyclic graph (DAG)
var directedAcyclic = new List<List<int>>
{
    new List<int> {1,2},
    new List<int> {3},
    new List<int> {3},
    new List<int> {}
};
Assert(HasCycleDirected(directedAcyclic) == false);

Console.WriteLine("All cycle detection tests passed.");