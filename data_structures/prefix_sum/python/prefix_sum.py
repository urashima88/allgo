from typing import List

def build_prefix_sum(a: List[int]) -> List[int]:
    n = len(a)
    pref = [0] * (n+1)
    for i in range(n):
        pref[i+1] = pref[i] + a[i]
    return pref

def query_sum(pref: List[int], l: int, r: int) -> int:
    return pref[r+1] - pref[l]

