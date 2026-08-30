from collections import deque
from typing import List, Tuple

# Directions: up, down, left, right
DIRECTIONS = [(-1, 0), (1, 0), (0, -1), (0, 1)]

def shortest_path(grid: List[List[int]],
                  start: Tuple[int, int],
                  target: Tuple[int, int]) -> List[Tuple[int, int]]:
    """
    Return the shortest path from start to target as a list of (row, col) pairs.
    If target is unreachable, return an empty list.
    grid uses 0 for free cells and 1 for obstacles.
    """
    rows, cols = len(grid), len(grid[0])
    dist = [[-1] * cols for _ in range(rows)]
    parent = [[None] * cols for _ in range(rows)]

    q = deque()
    q.append(start)
    dist[start[0]][start[1]] = 0

    while q:
        r, c = q.popleft()
        if (r, c) == target:
            break

        for dr, dc in DIRECTIONS:
            nr, nc = r + dr, c + dc
            if 0 <= nr < rows and 0 <= nc < cols and grid[nr][nc] == 0 and dist[nr][nc] == -1:
                dist[nr][nc] = dist[r][c] + 1
                parent[nr][nc] = (r, c)
                q.append((nr, nc))

    if dist[target[0]][target[1]] == -1:
        return []   # unreachable

    # Reconstruct path
    path = []
    cur = target
    while cur is not None:
        path.append(cur)
        cur = parent[cur[0]][cur[1]]
    path.reverse()
    return path

# ---------- Tests ----------
if __name__ == "__main__":
    grid = [
        [0, 0, 0, 0, 0],
        [0, 1, 1, 1, 0],
        [0, 0, 0, 0, 0],
        [0, 1, 1, 1, 0],
        [0, 0, 0, 0, 0]
    ]
    start = (0, 0)
    target = (4, 4)
    path = shortest_path(grid, start, target)
    assert path, "Path should not be empty"
    assert path[0] == start
    assert path[-1] == target
    assert len(path) == 9   # 8 steps, 9 cells
    print("All tests passed.")