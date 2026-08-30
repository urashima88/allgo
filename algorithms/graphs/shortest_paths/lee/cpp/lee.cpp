#include <iostream>
#include <vector>
#include <queue>
#include <cassert>
#include <algorithm>

// Directions: up, down, left, right
const std::vector<std::pair<int, int>> directions = {
    {-1, 0}, {1, 0}, {0, -1}, {0, 1}
};

// Returns the shortest path from start to target as a list of (row, col) pairs.
// If target is unreachable, returns an empty vector.
std::vector<std::pair<int, int>> shortestPath(
    const std::vector<std::vector<int>>& grid,
    std::pair<int, int> start,
    std::pair<int, int> target) {

    int rows = grid.size();
    int cols = grid[0].size();

    // Distance array initialized with -1 (unvisited)
    std::vector<std::vector<int>> dist(rows, std::vector<int>(cols, -1));
    // Parent array for path reconstruction
    std::vector<std::vector<std::pair<int, int>>> parent(rows,
        std::vector<std::pair<int, int>>(cols, {-1, -1}));

    std::queue<std::pair<int, int>> q;
    dist[start.first][start.second] = 0;
    q.push(start);

    bool found = false;
    while (!q.empty() && !found) {
        std::pair<int, int> u = q.front();
        q.pop();

        for (const std::pair<int, int>& dir : directions) {
            int nr = u.first + dir.first;
            int nc = u.second + dir.second;

            // Check bounds and obstacles
            if (nr < 0 || nr >= rows || nc < 0 || nc >= cols)
                continue;
            if (grid[nr][nc] == 1 || dist[nr][nc] != -1)
                continue;

            dist[nr][nc] = dist[u.first][u.second] + 1;
            parent[nr][nc] = u;
            q.push({nr, nc});

            if (nr == target.first && nc == target.second) {
                found = true;
                break;
            }
        }
    }

    if (!found) return {};  // unreachable

    // Reconstruct path from target to start
    std::vector<std::pair<int, int>> path;
    std::pair<int, int> cur = target;
    while (!(cur.first == start.first && cur.second == start.second)) {
        path.push_back(cur);
        cur = parent[cur.first][cur.second];
    }
    path.push_back(start);
    std::reverse(path.begin(), path.end());
    return path;
}

// ---------- Tests ----------
int main() {
    // 5x5 grid with obstacles
    // 0 = free, 1 = obstacle
    std::vector<std::vector<int>> grid = {
        {0, 0, 0, 0, 0},
        {0, 1, 1, 1, 0},
        {0, 0, 0, 0, 0},
        {0, 1, 1, 1, 0},
        {0, 0, 0, 0, 0}
    };

    std::pair<int, int> start = {0, 0};
    std::pair<int, int> target = {4, 4};

    std::vector<std::pair<int, int>> path = shortestPath(grid, start, target);

    // Check path is not empty
    assert(!path.empty());

    // Check start and end points
    assert(path.front() == start);
    assert(path.back() == target);

    // Check path length: in this grid, the shortest path length should be 8 steps
    // Path length = distance + 1 (number of cells)
    assert(path.size() == 9);   // 8 edges, 9 vertices

    std::cout << "Shortest path length: " << path.size() - 1 << std::endl;
    std::cout << "All tests passed.\n";
    return 0;
}