#include <iostream>
#include <vector>
#include <cassert>

// ========== Undirected graph cycle detection ==========
bool hasCycleUndirectedDfs(int u, int parent,
                              const std::vector<std::vector<int>>& graph,
                              std::vector<bool>& visited) {
    visited[u] = true;
    for (int v : graph[u]) {
        if (!visited[v]) {
            if (hasCycleUndirectedDfs(v, u, graph, visited))
                return true;
        } else if (v != parent) {
            return true;   // found a back edge to a visited vertex that is not parent
        }
    }
    return false;
}

bool hasCycleUndirected(const std::vector<std::vector<int>>& graph) {
    int n = graph.size();
    std::vector<bool> visited(n, false);
    for (int i = 0; i < n; ++i) {
        if (!visited[i]) {
            if (hasCycleUndirectedDfs(i, -1, graph, visited))
                return true;
        }
    }
    return false;
}

// ========== Directed graph cycle detection ==========
// Colors: 0 = white (unvisited), 1 = gray (in stack), 2 = black (done)
bool hasCycleDirectedDfs(int u,
                            const std::vector<std::vector<int>>& graph,
                            std::vector<int>& color) {
    color[u] = 1;  // gray
    for (int v : graph[u]) {
        if (color[v] == 1) {
            return true;   // back edge found
        } else if (color[v] == 0) {
            if (hasCycleDirectedDfs(v, graph, color))
                return true;
        }
    }
    color[u] = 2;  // black
    return false;
}

bool hasCycleDirected(const std::vector<std::vector<int>>& graph) {
    int n = graph.size();
    std::vector<int> color(n, 0);
    for (int i = 0; i < n; ++i) {
        if (color[i] == 0) {
            if (hasCycleDirectedDfs(i, graph, color))
                return true;
        }
    }
    return false;
}

// ---------- Tests ----------
int main() {
    // Undirected graph with cycle
    std::vector<std::vector<int>> undirected_cyclic = {
        {1, 2},    // 0
        {0, 2},    // 1
        {0, 1}     // 2  (triangle)
    };
    assert(hasCycleUndirected(undirected_cyclic) == true);

    // Undirected graph without cycle (tree)
    std::vector<std::vector<int>> undirected_acyclic = {
        {1},       // 0
        {0, 2},    // 1
        {1}        // 2
    };
    assert(hasCycleUndirected(undirected_acyclic) == false);

    // Directed graph with cycle
    std::vector<std::vector<int>> directed_cyclic = {
        {1},       // 0 -> 1
        {2},       // 1 -> 2
        {0}        // 2 -> 0 (cycle)
    };
    assert(hasCycleDirected(directed_cyclic) == true);

    // Directed acyclic graph (DAG)
    std::vector<std::vector<int>> directed_acyclic = {
        {1, 2},    // 0 -> 1,2
        {3},       // 1 -> 3
        {3},       // 2 -> 3
        {}         // 3
    };
    assert(hasCycleDirected(directed_acyclic) == false);

    std::cout << "All cycle detection tests passed.\n";
    return 0;
}