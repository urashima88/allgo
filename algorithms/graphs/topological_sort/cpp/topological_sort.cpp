#include <iostream>
#include <vector>
#include <queue>
#include <stack>
#include <cassert>
#include <algorithm>

// ========== Kahn's algorithm (BFS-based) ==========
// Returns a topological order of the DAG.
// If the graph has a cycle, returns an empty vector.
std::vector<int> topologicalSortKahn(const std::vector<std::vector<int>>& graph) {
    int n = graph.size();
    std::vector<int> in_degree(n, 0);
    for (int u = 0; u < n; ++u) {
        for (int v : graph[u]) {
            ++in_degree[v];
        }
    }

    std::queue<int> q;
    for (int v = 0; v < n; ++v) {
        if (in_degree[v] == 0) {
            q.push(v);
        }
    }

    std::vector<int> order;
    while (!q.empty()) {
        int u = q.front();
        q.pop();
        order.push_back(u);
        for (int v : graph[u]) {
            --in_degree[v];
            if (in_degree[v] == 0) {
                q.push(v);
            }
        }
    }

    // If not all vertices are in the order, the graph has a cycle.
    if ((int)order.size() != n) {
        return {};
    }
    return order;
}

// ========== DFS-based algorithm ==========
// color: 0 = white (unvisited), 1 = gray (in stack), 2 = black (done)
bool dfsTopSort(int u, const std::vector<std::vector<int>>& graph,
                  std::vector<int>& color, std::vector<int>& order) {
    color[u] = 1;   // gray
    for (int v : graph[u]) {
        if (color[v] == 1) {
            return false;   // back edge -> cycle
        }
        if (color[v] == 0) {
            if (!dfsTopSort(v, graph, color, order)) {
                return false;
            }
        }
    }
    color[u] = 2;   // black
    order.push_back(u);
    return true;
}

// Returns a topological order of the DAG.
// If the graph has a cycle, returns an empty vector.
std::vector<int> topologicalSortDfs(const std::vector<std::vector<int>>& graph) {
    int n = graph.size();
    std::vector<int> color(n, 0);
    std::vector<int> order;

    for (int u = 0; u < n; ++u) {
        if (color[u] == 0) {
            if (!dfsTopSort(u, graph, color, order)) {
                return {};
            }
        }
    }

    // DFS adds vertices in reverse topological order; reverse the result.
    std::reverse(order.begin(), order.end());
    return order;
}

// ---------- Tests ----------
int main() {
    // Example DAG:
    // 5 -> 2 -> 3 -> 1
    // |         ^
    // v         |
    // 0 -> 4 ---
    std::vector<std::vector<int>> graph = {
        {4},        // 0
        {},         // 1
        {3},        // 2
        {1},        // 3
        {3},        // 4
        {0, 2}      // 5
    };

    std::vector<int> kahn = topologicalSortKahn(graph);
    assert(kahn.size() == 6);
    // Verify topological property: for every edge u->v, u appears before v.
    std::vector<int> pos(6);
    for (int i = 0; i < 6; ++i) pos[kahn[i]] = i;
    for (int u = 0; u < 6; ++u)
        for (int v : graph[u])
            assert(pos[u] < pos[v]);

    std::vector<int> dfs = topologicalSortDfs(graph);
    assert(dfs.size() == 6);
    for (int i = 0; i < 6; ++i) pos[dfs[i]] = i;
    for (int u = 0; u < 6; ++u)
        for (int v : graph[u])
            assert(pos[u] < pos[v]);

    // Graph with cycle: 0 -> 1 -> 2 -> 0
    std::vector<std::vector<int>> cyclic = {
        {1},        // 0
        {2},        // 1
        {0}         // 2
    };
    assert(topologicalSortKahn(cyclic).empty());
    assert(topologicalSortDfs(cyclic).empty());

    std::cout << "All topological sort tests passed.\n";
    return 0;
}