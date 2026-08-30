from typing import List

# ========== Undirected graph cycle detection ==========
def has_cycle_undirected(graph: List[List[int]]) -> bool:
    def dfs(u: int, parent: int) -> bool:
        visited[u] = True
        for v in graph[u]:
            if not visited[v]:
                if dfs(v, u):
                    return True
            elif v != parent:
                return True
        return False

    n = len(graph)
    visited = [False] * n
    for i in range(n):
        if not visited[i]:
            if dfs(i, -1):
                return True
    return False

# ========== Directed graph cycle detection ==========
def has_cycle_directed(graph: List[List[int]]) -> bool:
    # 0 = white, 1 = gray, 2 = black
    def dfs(u: int) -> bool:
        color[u] = 1
        for v in graph[u]:
            if color[v] == 1:
                return True
            elif color[v] == 0:
                if dfs(v):
                    return True
        color[u] = 2
        return False

    n = len(graph)
    color = [0] * n
    for i in range(n):
        if color[i] == 0:
            if dfs(i):
                return True
    return False

# ---------- Tests ----------
if __name__ == "__main__":
    # Undirected cycle (triangle)
    undirected_cyclic = [[1,2], [0,2], [0,1]]
    assert has_cycle_undirected(undirected_cyclic) == True

    # Undirected tree
    undirected_acyclic = [[1], [0,2], [1]]
    assert has_cycle_undirected(undirected_acyclic) == False

    # Directed cycle
    directed_cyclic = [[1], [2], [0]]
    assert has_cycle_directed(directed_cyclic) == True

    # Directed acyclic graph (DAG)
    directed_acyclic = [[1,2], [3], [3], []]
    assert has_cycle_directed(directed_acyclic) == False

    print("All cycle detection tests passed.")