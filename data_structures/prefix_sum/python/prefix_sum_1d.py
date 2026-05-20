from typing import List

def build_1d_prefix_sum(a: List[int]) -> List[int]:
    n = len(a)
    pref = [0] * (n+1)
    for i in range(n):
        pref[i+1] = pref[i] + a[i]
    return pref

def query_sum(pref: List[int], l: int, r: int) -> int:
    return pref[r+1] - pref[l]

if __name__ == "__main__":
    a = [3, 1, 4, 1, 5]
    pref = build_1d_prefix_sum(a)
    assert query_sum(pref, 1, 3) == 6
    assert query_sum(pref, 0, 4) == 14
    print("1D prefix sum examples passed.")