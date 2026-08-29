#include <iostream>
#include <vector>
#include <cassert>
#include <algorithm>

// Helper DFS function: recursively visits all vertices in the same component.
void dfs(int u, const std::vector<std::vector<int>>& graph,
         std::vector<bool>& visited, std::vector<int>& component) {
    visited[u] = true;
    component.push_back(u);
    for (int v : graph[u]) {
        if (!visited[v]) {
            dfs(v, graph, visited, component);
        }
    }
}

// Returns a list of all connected components in the undirected graph.
// graph is an adjacency list.
std::vector<std::vector<int>> findConnectedComponents(
    const std::vector<std::vector<int>>& graph) {
    int n = graph.size();
    std::vector<bool> visited(n, false);
    std::vector<std::vector<int>> components;

    for (int start = 0; start < n; ++start) {
        if (!visited[start]) {
            std::vector<int> component;
            dfs(start, graph, visited, component);
            components.push_back(component);
        }
    }
    return components;
}

// ---------- Tests ----------
int main() {
    // Example graph with 3 components:
    // Component 1: 0-1-2
    // Component 2: 3-4
    // Component 3: 5 (isolated)
    std::vector<std::vector<int>> graph = {
        {1, 2},       // 0
        {0, 2},       // 1
        {0, 1},       // 2
        {4},          // 3
        {3},          // 4
        {}            // 5
    };

    std::vector<std::vector<int>> components = findConnectedComponents(graph);

    assert(components.size() == 3);   // three components

    // Verify the sizes (order may vary depending on DFS start order)
    // In our DFS from 0, we get component 0-1-2; from 3 we get 3-4; from 5 we get 5.
    assert(components[0].size() == 3);
    assert(components[1].size() == 2);
    assert(components[2].size() == 1);

    // Check that vertices are correctly grouped (by sorting each component and comparing)
    std::vector<std::vector<int>> expected = { {0,1,2}, {3,4}, {5} };
    for (std::vector<int>& comp : components) {
        std::sort(comp.begin(), comp.end());
    }
    // Compare with expected
    // Since components are stored in order of discovery, expected should match exactly.
    // But to be safe, we can sort components by first element.
    std::sort(components.begin(), components.end(),
              [](const std::vector<int>& a, const std::vector<int>& b) {
                  return a[0] < b[0];
              });
    assert(components == expected);

    std::cout << "All connected components tests passed.\n";
    return 0;
}