#include <iostream>
#include <vector>
#include <queue>
#include <utility>
#include <limits>
#include <cassert>
#include <algorithm>

// Graph: adjacency list of pairs (neighbor, weight)
std::pair<std::vector<long long>, std::vector<int>> dijkstra(
    const std::vector<std::vector<std::pair<int, int>>>& graph, int start) {

    const long long INF = std::numeric_limits<long long>::max() / 4;
    int n = graph.size();
    std::vector<long long> dist(n, INF);
    std::vector<int> parent(n, -1);

    // min-heap: pair (distance, vertex)
    std::priority_queue<std::pair<long long, int>,
                        std::vector<std::pair<long long, int>>,
                        std::greater<std::pair<long long, int>>> pq;

    dist[start] = 0;
    pq.push({0, start});

    while (!pq.empty()) {
        std::pair<long long, int> cur = pq.top();
        pq.pop();
        long long d = cur.first;
        int u = cur.second;

        // Skip outdated entry
        if (d != dist[u]) continue;

        for (const std::pair<int, int>& edge : graph[u]) {
            int v = edge.first;
            int w = edge.second;
            if (dist[u] + w < dist[v]) {
                dist[v] = dist[u] + w;
                parent[v] = u;
                pq.push({dist[v], v});
            }
        }
    }

    return {dist, parent};
}

// ---------- Tests ----------
int main() {
    // Example graph:
    // 0 --(4)-- 1
    // |         |
    // (1)       (2)
    // |         |
    // 2 --(5)-- 3
    //  \       /
    //   (3)  (1)
    //     4
    std::vector<std::vector<std::pair<int, int>>> graph = {
        {{1, 4}, {2, 1}},        // 0
        {{0, 4}, {3, 2}},        // 1
        {{0, 1}, {3, 5}, {4, 3}},// 2
        {{1, 2}, {2, 5}, {4, 1}},// 3
        {{2, 3}, {3, 1}}         // 4
    };

    int start = 0;
    std::pair<std::vector<long long>, std::vector<int>> result = dijkstra(graph, start);
    std::vector<long long> dist = result.first;
    std::vector<int> parent = result.second;

    // Expected distances:
    // to 0: 0
    // to 1: 4
    // to 2: 1
    // to 3: 5 (0->2->4->3 = 1+3+1 = 5)
    // to 4: 4 (0->2->4 = 1+3 = 4)
    assert(dist[0] == 0);
    assert(dist[1] == 4);
    assert(dist[2] == 1);
    assert(dist[3] == 5);
    assert(dist[4] == 4);

    // Reconstruct path to 3: 0 -> 2 -> 4 -> 3
    std::vector<int> path;
    int cur = 3;
    while (cur != -1) {
        path.push_back(cur);
        cur = parent[cur];
    }
    std::vector<int> expected_path = {3, 4, 2, 0};
    std::reverse(path.begin(), path.end());
    assert(path == std::vector<int>({0, 2, 4, 3}));

    std::cout << "All Dijkstra tests passed.\n";
    return 0;
}