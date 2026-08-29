#include <iostream>
#include <vector>
#include <queue>
#include <cassert>

// Returns true if the graph is bipartite, false otherwise.
// graph is an adjacency list.
bool isBipartite(const std::vector<std::vector<int>>& graph) {
    int n = graph.size();
    std::vector<int> color(n, -1);   // -1 means uncolored

    for (int start = 0; start < n; ++start) {
        if (color[start] != -1) continue;

        std::queue<int> q;
        color[start] = 0;
        q.push(start);

        while (!q.empty()) {
            int u = q.front();
            q.pop();

            for (int v : graph[u]) {
                if (color[v] == -1) {
                    color[v] = 1 - color[u];   // opposite color
                    q.push(v);
                } else if (color[v] == color[u]) {
                    return false;              // conflict found
                }
            }
        }
    }
    return true;
}

// ---------- Tests ----------
int main() {
    // Bipartite example: even cycle (0-1-2-3-0)
    std::vector<std::vector<int>> bipartite_graph = {
        {1, 3},       // 0
        {0, 2},       // 1
        {1, 3},       // 2
        {2, 0}        // 3
    };
    assert(isBipartite(bipartite_graph) == true);

    // Non-bipartite example: triangle (0-1-2-0)
    std::vector<std::vector<int>> non_bipartite_graph = {
        {1, 2},
        {0, 2},
        {0, 1}
    };
    assert(isBipartite(non_bipartite_graph) == false);

    // Disconnected graph with one bipartite and one non-bipartite component
    std::vector<std::vector<int>> mixed_graph = {
        {1},          // 0
        {0},          // 1  (component 0-1 is bipartite)
        {3, 4},       // 2
        {2, 4},       // 3
        {2, 3}        // 4  (component 2-3-4 is a triangle)
    };
    assert(isBipartite(mixed_graph) == false);

    std::cout << "All bipartite tests passed.\n";
    return 0;
}