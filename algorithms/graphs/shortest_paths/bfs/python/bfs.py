from collections import deque
from typing import List, Tuple, Dict

def bfs_shortest_distances(graph: List[List[int]], start: int) -> List[int]:
    """
    Return the shortest distances from start to all vertices.
    Unreachable vertices get distance -1.
    """
    n = len(graph)
    dist = [-1] * n
    q = deque([start])
    dist[start] = 0

    while q:
        u = q.popleft()
        for v in graph[u]:
            if dist[v] == -1:
                dist[v] = dist[u] + 1
                q.append(v)
    return dist

def bfs_with_parents(graph: List[List[int]], start: int) -> Tuple[List[int], List[int]]:
    """
    Return (distances, parents) from BFS starting at start.
    Parent of start is -1.
    """
    n = len(graph)
    dist = [-1] * n
    parent = [-1] * n
    q = deque([start])
    dist[start] = 0

    while q:
        u = q.popleft()
        for v in graph[u]:
            if dist[v] == -1:
                dist[v] = dist[u] + 1
                parent[v] = u
                q.append(v)
    return dist, parent

def reconstruct_path(start: int, target: int, parent: List[int]) -> List[int]:
    """
    Reconstruct the path from start to target using the parent array.
    Returns empty list if target is unreachable.
    """
    if parent[target] == -1 and target != start:
        return []
    path = []
    v = target
    while v != -1:
        path.append(v)
        v = parent[v]
    path.reverse()
    return path

# ---------- Tests ----------
if __name__ == "__main__":
    graph = [
        [1, 3],
        [0, 2, 4],
        [1],
        [0, 4],
        [1, 3]
    ]
    dist = bfs_shortest_distances(graph, 0)
    assert dist == [0, 1, 2, 1, 2]

    dist2, parent = bfs_with_parents(graph, 0)
    path_to_2 = reconstruct_path(0, 2, parent)
    assert path_to_2 == [0, 1, 2]

    print("All BFS tests passed.")