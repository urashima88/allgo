from typing import List, Tuple
import heapq

def dijkstra(graph: List[List[Tuple[int, int]]], start: int) -> Tuple[List[int], List[int]]:
    """
    Return (distances, parents) from start using Dijkstra's algorithm.
    graph is adjacency list: graph[u] = [(v, weight), ...]
    Distances to unreachable vertices are set to a large number (10**18).
    """
    n = len(graph)
    INF = 10**18
    dist = [INF] * n
    parent = [-1] * n
    dist[start] = 0

    # min-heap of (distance, vertex)
    pq = [(0, start)]

    while pq:
        d, u = heapq.heappop(pq)
        if d != dist[u]:
            continue
        for v, w in graph[u]:
            if dist[u] + w < dist[v]:
                dist[v] = dist[u] + w
                parent[v] = u
                heapq.heappush(pq, (dist[v], v))

    return dist, parent

# ---------- Tests ----------
if __name__ == "__main__":
    graph = [
        [(1,4), (2,1)],
        [(0,4), (3,2)],
        [(0,1), (3,5), (4,3)],
        [(1,2), (2,5), (4,1)],
        [(2,3), (3,1)]
    ]
    start = 0
    dist, parent = dijkstra(graph, start)

    assert dist[0] == 0
    assert dist[1] == 4
    assert dist[2] == 1
    assert dist[3] == 5
    assert dist[4] == 4

    # Reconstruct path to 3
    path = []
    cur = 3
    while cur != -1:
        path.append(cur)
        cur = parent[cur]
    path.reverse()
    assert path == [0, 2, 4, 3]

    print("All Dijkstra tests passed.")