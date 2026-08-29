from collections import deque
from typing import List

def is_bipartite(graph: List[List[int]]) -> bool:
    """
    Return True if the graph is bipartite, False otherwise.
    Graph is an adjacency list.
    """
    n = len(graph)
    color = [-1] * n          # -1 means uncolored

    for start in range(n):
        if color[start] != -1:
            continue

        q = deque([start])
        color[start] = 0

        while q:
            u = q.popleft()
            for v in graph[u]:
                if color[v] == -1:
                    color[v] = 1 - color[u]
                    q.append(v)
                elif color[v] == color[u]:
                    return False

    return True

# ---------- Tests ----------
if __name__ == "__main__":
    # Bipartite: even cycle
    bipartite_graph = [
        [1, 3],
        [0, 2],
        [1, 3],
        [2, 0]
    ]
    assert is_bipartite(bipartite_graph) == True

    # Non-bipartite: triangle
    non_bipartite_graph = [
        [1, 2],
        [0, 2],
        [0, 1]
    ]
    assert is_bipartite(non_bipartite_graph) == False

    # Mixed disconnected graph
    mixed_graph = [
        [1],
        [0],
        [3, 4],
        [2, 4],
        [2, 3]
    ]
    assert is_bipartite(mixed_graph) == False

    print("All bipartite tests passed.")