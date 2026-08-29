using System;
using System.Collections.Generic;
using System.Linq;

// Recursive DFS helper.
void Dfs(int u, List<List<int>> graph, bool[] visited, List<int> component)
{
    visited[u] = true;
    component.Add(u);
    foreach (int v in graph[u])
    {
        if (!visited[v])
        {
            Dfs(v, graph, visited, component);
        }
    }
}

// Returns a list of connected components in the undirected graph.
List<List<int>> FindConnectedComponents(List<List<int>> graph)
{
    int n = graph.Count;
    bool[] visited = new bool[n];
    List<List<int>> components = new List<List<int>>();

    for (int start = 0; start < n; start++)
    {
        if (!visited[start])
        {
            List<int> component = new List<int>();
            Dfs(start, graph, visited, component);
            components.Add(component);
        }
    }
    return components;
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
    new List<int> { 1, 2 },
    new List<int> { 0, 2 },
    new List<int> { 0, 1 },
    new List<int> { 4 },
    new List<int> { 3 },
    new List<int> { }
};

List<List<int>> components = FindConnectedComponents(graph);

Assert(components.Count == 3);

// Sort each component and order components by first element for deterministic comparison
for (int i = 0; i < components.Count; i++)
    components[i].Sort();
components.Sort((a, b) => (a.Count > 0 ? a[0] : -1).CompareTo(b.Count > 0 ? b[0] : -1));

var expected = new List<List<int>>
{
    new List<int> { 0, 1, 2 },
    new List<int> { 3, 4 },
    new List<int> { 5 }
};

for (int i = 0; i < expected.Count; i++)
{
    Assert(components[i].SequenceEqual(expected[i]));
}

Console.WriteLine("All connected components tests passed.");