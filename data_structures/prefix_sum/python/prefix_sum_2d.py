from typing import List

def buildPrefixSum2D(a: List[List[int]]) -> List[List[int]]:
    n = len(a)
    m = len(a[0])
    pref = [[0] * (m+1) for _ in range(n+1)]
    for i in range(1, n+1):
        for j in range(1, m+1):
            pref[i][j] = pref[i-1][j] + pref[i][j-1] - pref[i-1][j-1] + a[i-1][j-1]
    return pref

# left borders - inclusive, right - exclusive
def queryPrefixSum2D(pref: List[List[int]], lx: int, ly: int, rx: int, ry: int) -> int:
    return pref[rx][ry] - pref[lx][ry] - pref[rx][ly] + pref[lx][ly]

if __name__ == "__main__":
    a = [
        [1, 2, 3],
        [4, 5, 6],
        [7, 8, 9]
    ]
    pref = buildPrefixSum2D(a)
    assert queryPrefixSum2D(pref, 0, 0, 3, 3) == 45
    assert queryPrefixSum2D(pref, 1, 1, 2, 2) == 5
    assert queryPrefixSum2D(pref, 0, 1, 2, 3) == 16
    print("2D prefix sum examples passed.")