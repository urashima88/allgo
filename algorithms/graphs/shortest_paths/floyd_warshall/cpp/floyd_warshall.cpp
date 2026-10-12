#include <iostream>
#include <vector>
#include <limits>
#include <cassert>
#include <algorithm>

// Computes all-pairs shortest paths using Floyd-Warshall.
// If directed = false, edges are treated as undirected.
std::vector<std::vector<long long>> floydWarshall(
    int n, const std::vector<std::tuple<int, int, int>>& edges, bool directed = false) {

    const long long INF = std::numeric_limits<long long>::max() / 4;
    std::vector<std::vector<long long>> dist(n, std::vector<long long>(n, INF));

    for (int i = 0; i < n; ++i) {
        dist[i][i] = 0;
    }

    for (const std::tuple<int, int, int>& edge : edges) {
        int u = std::get<0>(edge);
        int v = std::get<1>(edge);
        int w = std::get<2>(edge);
        if (w < dist[u][v]) {
            dist[u][v] = w;
            if (!directed) {
                dist[v][u] = w;
            }
        }
    }

    for (int k = 0; k < n; ++k) {
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                if (dist[i][k] + dist[k][j] < dist[i][j]) {
                    dist[i][j] = dist[i][k] + dist[k][j];
                }
            }
        }
    }

    return dist;
}

// Same algorithm, also builds the next matrix for path reconstruction.
std::pair<std::vector<std::vector<long long>>, std::vector<std::vector<int>>>
floydWarshallWithPath(int n, const std::vector<std::tuple<int, int, int>>& edges,
                         bool directed = false) {

    const long long INF = std::numeric_limits<long long>::max() / 4;
    std::vector<std::vector<long long>> dist(n, std::vector<long long>(n, INF));
    std::vector<std::vector<int>> next(n, std::vector<int>(n, -1));

    for (int i = 0; i < n; ++i) {
        dist[i][i] = 0;
        next[i][i] = i;
    }

    for (const std::tuple<int, int, int>& edge : edges) {
        int u = std::get<0>(edge);
        int v = std::get<1>(edge);
        int w = std::get<2>(edge);
        if (w < dist[u][v]) {
            dist[u][v] = w;
            next[u][v] = v;
            if (!directed) {
                dist[v][u] = w;
                next[v][u] = u;
            }
        }
    }

    for (int k = 0; k < n; ++k) {
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                if (dist[i][k] + dist[k][j] < dist[i][j]) {
                    dist[i][j] = dist[i][k] + dist[k][j];
                    next[i][j] = next[i][k];
                }
            }
        }
    }

    return {dist, next};
}

// Reconstructs path from u to v using the next matrix.
std::vector<int> reconstruct_path(int u, int v,
                                  const std::vector<std::vector<int>>& next) {
    if (next[u][v] == -1) {
        return {};
    }
    std::vector<int> path;
    path.push_back(u);
    while (u != v) {
        u = next[u][v];
        path.push_back(u);
    }
    return path;
}

// ---------- Tests ----------
int main() {
    int n = 4;
    std::vector<std::tuple<int, int, int>> edges = {
        {0, 1, 3},
        {1, 2, 2},
        {2, 3, 5},
        {3, 0, 8}
    };

    std::vector<std::vector<long long>> dist = floydWarshall(n, edges, false);
    assert(dist[0][0] == 0);
    assert(dist[0][1] == 3);
    assert(dist[0][2] == 5);    // 0->1->2 = 3+2
    assert(dist[0][3] == 8);    // direct edge
    assert(dist[1][3] == 7);    // 1->2->3 = 2+5
    assert(dist[2][2] == 0);

    std::pair<std::vector<std::vector<long long>>, std::vector<std::vector<int>>> result =
        floydWarshallWithPath(n, edges, false);
    std::vector<std::vector<int>> next = result.second;

    // Correct shortest path from 0 to 3 is the direct edge: [0, 3]
    std::vector<int> path = reconstruct_path(0, 3, next);
    assert((path == std::vector<int>{0, 3}));

    // Shortest path from 1 to 3: [1, 2, 3]
    std::vector<int> path2 = reconstruct_path(1, 3, next);
    assert((path2 == std::vector<int>{1, 2, 3}));

    // Directed graph with negative cycle: 0 -> 1 -> 2 -> 0
    std::vector<std::tuple<int, int, int>> neg_edges = {
        {0, 1, 1},
        {1, 2, -3},
        {2, 0, 1}
    };
    std::vector<std::vector<long long>> neg_dist = floydWarshall(3, neg_edges, true);
    bool has_negative_cycle = false;
    for (int i = 0; i < 3; ++i) {
        if (neg_dist[i][i] < 0) {
            has_negative_cycle = true;
        }
    }
    assert(has_negative_cycle);

    std::cout << "All Floyd-Warshall tests passed.\n";
    return 0;
}