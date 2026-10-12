using System;
using System.Collections.Generic;
using System.Linq;

// ========== Kahn's algorithm (BFS-based) ==========
// Returns a topological order of the DAG.
// If the graph has a cycle, returns an empty list.
List<int> TopologicalSortKahn(List<List<int>> graph)
{
    int n = graph.Count;
    int[] inDegree = new int[n];
    for (int u = 0; u < n; u++)
        foreach (int v in graph[u])
            inDegree[v]++;

    Queue<int> q = new Queue<int>();
    for (int v = 0; v < n; v++)
        if (inDegree[v] == 0)
            q.Enqueue(v);

    List<int> order = new List<int>();
    while (q.Count > 0)
    {
        int u = q.Dequeue();
        order.Add(u);
        foreach (int v in graph[u])
        {
            inDegree[v]--;
            if (inDegree[v] == 0)
                q.Enqueue(v);
        }
    }

    if (order.Count != n)
        return new List<int>();   // cycle detected
    return order;
}

// ========== DFS-based algorithm ==========
bool DfsTopSort(int u, List<List<int>> graph, int[] color, List<int> order)
{
    color[u] = 1;   // gray
    foreach (int v in graph[u])
    {
        if (color[v] == 1)
            return false;   // back edge -> cycle
        if (color[v] == 0)
            if (!DfsTopSort(v, graph, color, order))
                return false;
    }
    color[u] = 2;   // black
    order.Add(u);
    return true;
}

List<int> TopologicalSortDfs(List<List<int>> graph)
{
    int n = graph.Count;
    int[] color = new int[n];   // 0=white, 1=gray, 2=black
    List<int> order = new List<int>();

    for (int u = 0; u < n; u++)
    {
        if (color[u] == 0)
        {
            if (!DfsTopSort(u, graph, color, order))
                return new List<int>();
        }
    }

    order.Reverse();
    return order;
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
    new List<int> { 4 },
    new List<int> { },
    new List<int> { 3 },
    new List<int> { 1 },
    new List<int> { 3 },
    new List<int> { 0, 2 }
};

// Verify Kahn's algorithm
var kahn = TopologicalSortKahn(graph);
Assert(kahn.Count == 6);
int[] pos = new int[6];
for (int i = 0; i < 6; i++) pos[kahn[i]] = i;
for (int u = 0; u < 6; u++)
    foreach (int v in graph[u])
        Assert(pos[u] < pos[v]);

// Verify DFS-based algorithm
var dfs = TopologicalSortDfs(graph);
Assert(dfs.Count == 6);
for (int i = 0; i < 6; i++) pos[dfs[i]] = i;
for (int u = 0; u < 6; u++)
    foreach (int v in graph[u])
        Assert(pos[u] < pos[v]);

// Graph with cycle
var cyclic = new List<List<int>>
{
    new List<int> { 1 },
    new List<int> { 2 },
    new List<int> { 0 }
};
Assert(TopologicalSortKahn(cyclic).Count == 0);
Assert(TopologicalSortDfs(cyclic).Count == 0);

Console.WriteLine("All topological sort tests passed.");