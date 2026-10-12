from typing import List, Tuple
import math


def floyd_warshall(n: int, edges: List[Tuple[int, int, int]],
                   directed: bool = False) -> List[List[float]]:
    """
    Return the all-pairs shortest path distance matrix using Floyd-Warshall.
    If directed=False, edges are treated as undirected.
    Distance between unreachable vertices is math.inf.
    """
    INF = math.inf
    dist = [[INF] * n for _ in range(n)]

    for i in range(n):
        dist[i][i] = 0

    for u, v, w in edges:
        if w < dist[u][v]:
            dist[u][v] = w
            if not directed:
                dist[v][u] = w

    for k in range(n):
        for i in range(n):
            for j in range(n):
                if dist[i][k] + dist[k][j] < dist[i][j]:
                    dist[i][j] = dist[i][k] + dist[k][j]

    return dist


def floyd_warshall_with_path(n: int, edges: List[Tuple[int, int, int]],
                             directed: bool = False):
    """
    Return (dist, next_vertex) where next_vertex[i][j] is the next vertex
    on the shortest path from i to j, or -1 if unreachable.
    """
    INF = math.inf
    dist = [[INF] * n for _ in range(n)]
    next_vertex = [[-1] * n for _ in range(n)]

    for i in range(n):
        dist[i][i] = 0
        next_vertex[i][i] = i

    for u, v, w in edges:
        if w < dist[u][v]:
            dist[u][v] = w
            next_vertex[u][v] = v
            if not directed:
                dist[v][u] = w
                next_vertex[v][u] = u

    for k in range(n):
        for i in range(n):
            for j in range(n):
                if dist[i][k] + dist[k][j] < dist[i][j]:
                    dist[i][j] = dist[i][k] + dist[k][j]
                    next_vertex[i][j] = next_vertex[i][k]

    return dist, next_vertex


def reconstruct_path(u: int, v: int, next_vertex: List[List[int]]) -> List[int]:
    """
    Reconstruct the shortest path from u to v using the next_vertex matrix.
    Returns an empty list if unreachable.
    """
    if next_vertex[u][v] == -1:
        return []
    path = [u]
    while u != v:
        u = next_vertex[u][v]
        path.append(u)
    return path


# ---------- Tests ----------
if __name__ == "__main__":
    n = 4
    edges = [
        (0, 1, 3),
        (1, 2, 2),
        (2, 3, 5),
        (3, 0, 8)
    ]

    dist = floyd_warshall(n, edges, directed=False)
    assert dist[0][0] == 0
    assert dist[0][1] == 3
    assert dist[0][2] == 5
    assert dist[0][3] == 8
    assert dist[1][3] == 7

    dist2, next_vertex = floyd_warshall_with_path(n, edges, directed=False)
    assert reconstruct_path(0, 3, next_vertex) == [0, 3]
    assert reconstruct_path(1, 3, next_vertex) == [1, 2, 3]

    # Directed graph with a negative cycle: 0 -> 1 -> 2 -> 0
    neg_edges = [(0, 1, 1), (1, 2, -3), (2, 0, 1)]
    neg_dist = floyd_warshall(3, neg_edges, directed=True)
    has_negative_cycle = any(neg_dist[i][i] < 0 for i in range(3))
    assert has_negative_cycle

    print("All Floyd-Warshall tests passed.")