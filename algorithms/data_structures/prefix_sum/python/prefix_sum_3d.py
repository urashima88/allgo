from typing import List

def build_prefix_sum_3d(a: List[List[List[int]]]) -> List[List[List[int]]]:
    n = len(a)
    m = len(a[0])
    p = len(a[0][0])
    
    pref = [[[0] * (p + 1) for _ in range(m + 1)] for __ in range(n + 1)]

    for i in range(1, n + 1):
        for j in range(1, m + 1):
            for k in range(1, p + 1):
                pref[i][j][k] = (a[i - 1][j - 1][k - 1]
                    + pref[i-1][j][k]
                    + pref[i][j-1][k]
                    + pref[i][j][k-1]
                    - pref[i-1][j-1][k]
                    - pref[i-1][j][k-1]
                    - pref[i][j-1][k-1]
                    + pref[i-1][j-1][k-1])
    return pref

# left borders - inclusive, right - exclusive
def query_prefix_sum_3d(
    pref: List[List[List[int]]],
    lx: int, ly: int, lz: int,
    rx: int, ry: int, rz: int
) -> int:
    return (pref[rx][ry][rz]
        - pref[lx][ry][rz] - pref[rx][ly][rz] - pref[rx][ry][lz]
        + pref[lx][ly][rz] + pref[lx][ry][lz] + pref[rx][ly][lz]
        - pref[lx][ly][lz])
    
if __name__ == "__main__":
    a = [
        [[1, 2], [3, 4]],
        [[5, 6], [7, 8]]
    ]
    pref = build_prefix_sum_3d(a)
    
    assert query_prefix_sum_3d(pref, 0,0,0, 2,2,2) == 36
    assert query_prefix_sum_3d(pref, 1,1,1, 2,2,2) == 8
    assert query_prefix_sum_3d(pref, 0,0,0, 1,1,1) == 1
    assert query_prefix_sum_3d(pref, 0,0,0, 1,2,2) == 10
    print("3D prefix sum examples passed.")