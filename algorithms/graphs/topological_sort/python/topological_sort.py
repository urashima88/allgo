from collections import deque
from typing import List

# ========== Kahn's algorithm (BFS-based) ==========
def topological_sort_kahn(graph: List[List[int]]) -> List[int]:
    """
    Return a topological order of the DAG.
    If the graph has a cycle, return an empty list.
    """
    n = len(graph)
    in_degree = [0] * n
    for u in range(n):
        for v in graph[u]:
            in_degree[v] += 1

    q = deque([v for v in range(n) if in_degree[v] == 0])
    order: List[int] = []

    while q:
        u = q.popleft()
        order.append(u)
        for v in graph[u]:
            in_degree[v] -= 1
            if in_degree[v] == 0:
                q.append(v)

    if len(order) != n:
        return []   # cycle detected
    return order

# ========== DFS-based algorithm ==========
def topological_sort_dfs(graph: List[List[int]]) -> List[int]:
    """
    Return a topological order of the DAG via DFS.
    If the graph has a cycle, return an empty list.
    """
    n = len(graph)
    color = [0] * n   # 0=white, 1=gray, 2=black
    order: List[int] = []

    def dfs(u: int) -> bool:
        color[u] = 1
        for v in graph[u]:
            if color[v] == 1:
                return False   # back edge -> cycle
            if color[v] == 0:
                if not dfs(v):
                    return False
        color[u] = 2
        order.append(u)
        return True

    for u in range(n):
        if color[u] == 0:
            if not dfs(u):
                return []

    order.reverse()
    return order

# ---------- Tests ----------
if __name__ == "__main__":
    graph = [
        [4],
        [],
        [3],
        [1],
        [3],
        [0, 2]
    ]

    kahn = topological_sort_kahn(graph)
    assert len(kahn) == 6
    pos = [0] * 6
    for i, v in enumerate(kahn):
        pos[v] = i

    for u in range(6):
        for v in graph[u]:
            assert pos[u] < pos[v]

    dfs = topological_sort_dfs(graph)
    assert len(dfs) == 6
    for i, v in enumerate(dfs):
        pos[v] = i
    for u in range(6):
        for v in graph[u]:
            assert pos[u] < pos[v]

    cyclic = [[1], [2], [0]]
    assert topological_sort_kahn(cyclic) == []
    assert topological_sort_dfs(cyclic) == []

    print("All topological sort tests passed.")