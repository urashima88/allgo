#include <iostream>
#include <vector>
#include <queue>
#include <cassert>
#include <algorithm>

// Returns shortest distances from start to all vertices.
// Unreachable vertices get distance -1.
std::vector<int> bfsShortestDistances(const std::vector<std::vector<int>>& graph, int start) {
    int n = graph.size();
    std::vector<int> dist(n, -1);
    std::queue<int> q;
    dist[start] = 0;
    q.push(start);

    while (!q.empty()) {
        int u = q.front();
        q.pop();
        for (int v : graph[u]) {
            if (dist[v] == -1) {   // not visited yet
                dist[v] = dist[u] + 1;
                q.push(v);
            }
        }
    }
    return dist;
}

// Reconstructs the path from start to target using the parent array.
// Returns an empty vector if target is unreachable.
std::vector<int> reconstructPath(int start, int target, const std::vector<int>& parent) {
    if (parent[target] == -1 && target != start) {
        return {};  // unreachable
    }
    std::vector<int> path;
    for (int v = target; v != -1; v = parent[v]) {
        path.push_back(v);
    }
    std::reverse(path.begin(), path.end());
    return path;
}

// BFS that also stores parents for path reconstruction.
std::pair<std::vector<int>, std::vector<int>> bfsWithParents(
    const std::vector<std::vector<int>>& graph, int start) {
    int n = graph.size();
    std::vector<int> dist(n, -1);
    std::vector<int> parent(n, -1);
    std::queue<int> q;

    dist[start] = 0;
    q.push(start);

    while (!q.empty()) {
        int u = q.front();
        q.pop();
        for (int v : graph[u]) {
            if (dist[v] == -1) {
                dist[v] = dist[u] + 1;
                parent[v] = u;
                q.push(v);
            }
        }
    }
    return {dist, parent};
}

// ---------- Tests ----------
int main() {
    // Example graph: 0--1--2
    //                |  |
    //                3--4
    std::vector<std::vector<int>> graph = {
        {1, 3},       // 0
        {0, 2, 4},    // 1
        {1},          // 2
        {0, 4},       // 3
        {1, 3}        // 4
    };

    std::vector<int> dist = bfsShortestDistances(graph, 0);
    assert(dist[0] == 0);
    assert(dist[1] == 1);
    assert(dist[2] == 2);
    assert(dist[3] == 1);
    assert(dist[4] == 2);

    auto [dist2, parent] = bfsWithParents(graph, 0);
    std::vector<int> path_to_2 = reconstructPath(0, 2, parent);
    assert((path_to_2 == std::vector<int>{0, 1, 2}));

    std::cout << "All BFS tests passed.\n";
    return 0;
}