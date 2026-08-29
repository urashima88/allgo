from typing import List

def dfs(u: int, graph: List[List[int]], visited: List[bool], component: List[int]) -> None:
    """
    Depth-first search from vertex u, marking all reachable vertices as visited
    and appending them to component.
    """
    visited[u] = True
    component.append(u)
    for v in graph[u]:
        if not visited[v]:
            dfs(v, graph, visited, component)

def find_connected_components(graph: List[List[int]]) -> List[List[int]]:
    """
    Return a list of connected components in the undirected graph.
    Each component is a list of vertex indices.
    """
    n = len(graph)
    visited = [False] * n
    components: List[List[int]] = []

    for start in range(n):
        if not visited[start]:
            component: List[int] = []
            dfs(start, graph, visited, component)
            components.append(component)

    return components

# ---------- Tests ----------
if __name__ == "__main__":
    # Example graph with 3 components:
    graph = [
        [1, 2],   # 0
        [0, 2],   # 1
        [0, 1],   # 2
        [4],      # 3
        [3],      # 4
        []        # 5
    ]

    components = find_connected_components(graph)

    assert len(components) == 3, "Number of components should be 3"

    # Sort each component and the list of components for easy comparison
    components = [sorted(comp) for comp in components]
    components.sort(key=lambda comp: comp[0] if comp else -1)

    expected = [[0, 1, 2], [3, 4], [5]]
    assert components == expected, f"Components mismatch: {components}"

    print("All connected components tests passed.")