from typing import List

# Standard implementation
def build_prefix_sum_5d(a: List[List[List[List[List[int]]]]]) -> \
        List[List[List[List[List[int]]]]]:
    n1 = len(a)
    n2 = len(a[0])
    n3 = len(a[0][0])
    n4 = len(a[0][0][0])
    n5 = len(a[0][0][0][0])

    pref = [[[[[0] * (n5 + 1) for _ in range(n4 + 1)]
        for _ in range(n3 + 1)]
        for _ in range(n2 + 1)]
        for _ in range(n1 + 1)]

    for i1 in range(1, n1 + 1):
        for i2 in range(1, n2 + 1):
            for i3 in range(1, n3 + 1):
                for i4 in range(1, n4 + 1):
                    for i5 in range(1, n5 + 1):
                        pref[i1][i2][i3][i4][i5] = a[i1-1][i2-1][i3-1][i4-1][i5-1] \
                            + pref[i1-1][i2][i3][i4][i5] \
                            + pref[i1][i2-1][i3][i4][i5] \
                            + pref[i1][i2][i3-1][i4][i5] \
                            + pref[i1][i2][i3][i4-1][i5] \
                            + pref[i1][i2][i3][i4][i5-1] \
                            - pref[i1-1][i2-1][i3][i4][i5] \
                            - pref[i1-1][i2][i3-1][i4][i5] \
                            - pref[i1-1][i2][i3][i4-1][i5] \
                            - pref[i1-1][i2][i3][i4][i5-1] \
                            - pref[i1][i2-1][i3-1][i4][i5] \
                            - pref[i1][i2-1][i3][i4-1][i5] \
                            - pref[i1][i2-1][i3][i4][i5-1] \
                            - pref[i1][i2][i3-1][i4-1][i5] \
                            - pref[i1][i2][i3-1][i4][i5-1] \
                            - pref[i1][i2][i3][i4-1][i5-1] \
                            + pref[i1-1][i2-1][i3-1][i4][i5] \
                            + pref[i1-1][i2-1][i3][i4-1][i5] \
                            + pref[i1-1][i2-1][i3][i4][i5-1] \
                            + pref[i1-1][i2][i3-1][i4-1][i5] \
                            + pref[i1-1][i2][i3-1][i4][i5-1] \
                            + pref[i1-1][i2][i3][i4-1][i5-1] \
                            + pref[i1][i2-1][i3-1][i4-1][i5] \
                            + pref[i1][i2-1][i3-1][i4][i5-1] \
                            + pref[i1][i2-1][i3][i4-1][i5-1] \
                            + pref[i1][i2][i3-1][i4-1][i5-1] \
                            - pref[i1-1][i2-1][i3-1][i4-1][i5] \
                            - pref[i1-1][i2-1][i3-1][i4][i5-1] \
                            - pref[i1-1][i2-1][i3][i4-1][i5-1] \
                            - pref[i1-1][i2][i3-1][i4-1][i5-1] \
                            - pref[i1][i2-1][i3-1][i4-1][i5-1] \
                            + pref[i1-1][i2-1][i3-1][i4-1][i5-1]
    return pref

# Standard implementation
# left borders - inclusive, right - exclusive
def query_prefix_sum_5d(pref: List[List[List[List[List[int]]]]],
                        l1: int, l2: int, l3: int, l4: int, l5: int,
                        r1: int, r2: int, r3: int, r4: int, r5: int) -> int:
    return pref[r1][r2][r3][r4][r5] \
        - pref[l1][r2][r3][r4][r5] \
        - pref[r1][l2][r3][r4][r5] \
        - pref[r1][r2][l3][r4][r5] \
        - pref[r1][r2][r3][l4][r5] \
        - pref[r1][r2][r3][r4][l5] \
        + pref[l1][l2][r3][r4][r5] \
        + pref[l1][r2][l3][r4][r5] \
        + pref[l1][r2][r3][l4][r5] \
        + pref[l1][r2][r3][r4][l5] \
        + pref[r1][l2][l3][r4][r5] \
        + pref[r1][l2][r3][l4][r5] \
        + pref[r1][l2][r3][r4][l5] \
        + pref[r1][r2][l3][l4][r5] \
        + pref[r1][r2][l3][r4][l5] \
        + pref[r1][r2][r3][l4][l5] \
        - pref[l1][l2][l3][r4][r5] \
        - pref[l1][l2][r3][l4][r5] \
        - pref[l1][l2][r3][r4][l5] \
        - pref[l1][r2][l3][l4][r5] \
        - pref[l1][r2][l3][r4][l5] \
        - pref[l1][r2][r3][l4][l5] \
        - pref[r1][l2][l3][l4][r5] \
        - pref[r1][l2][l3][r4][l5] \
        - pref[r1][l2][r3][l4][l5] \
        - pref[r1][r2][l3][l4][l5] \
        + pref[l1][l2][l3][l4][r5] \
        + pref[l1][l2][l3][r4][l5] \
        + pref[l1][l2][r3][l4][l5] \
        + pref[l1][r2][l3][l4][l5] \
        + pref[r1][l2][l3][l4][l5] \
        - pref[l1][l2][l3][l4][l5]
        
# Short implementation (cycle by masks)
def build_prefix_sum_5d_short(a: List[List[List[List[List[int]]]]]) -> \
        List[List[List[List[List[int]]]]]:
    n1 = len(a)
    n2 = len(a[0])
    n3 = len(a[0][0])
    n4 = len(a[0][0][0])
    n5 = len(a[0][0][0][0])

    pref = [[[[[0] * (n5 + 1) for _ in range(n4 + 1)]
        for _ in range(n3 + 1)]
        for _ in range(n2 + 1)]
        for _ in range(n1 + 1)]

    for i1 in range(1, n1 + 1):
        for i2 in range(1, n2 + 1):
            for i3 in range(1, n3 + 1):
                for i4 in range(1, n4 + 1):
                    for i5 in range(1, n5 + 1):
                        # idx holds the current coordinates in the prefix array
                        idx = (i1, i2, i3, i4, i5)

                        # value will accumulate the inclusion–exclusion sum over all sub‑boxes
                        value = 0
                        # Iterate over all 32 subsets of dimensions that are "reduced by 1"
                        for mask in range(32):
                            # Build the corner coordinates:
                            # if the bit is set, use idx[d] - 1; otherwise use idx[d]
                            cur = tuple(idx[d] - ((mask >> d) & 1) for d in range(5))
                            term = pref[cur[0]][cur[1]][cur[2]][cur[3]][cur[4]]
                            # Even number of bits set -> add, odd -> subtract
                            if mask.bit_count() % 2 == 1:
                                value += term
                            else:
                                value -= term

                        # Finally add the current element from the original array
                        pref[i1][i2][i3][i4][i5] = value + a[i1-1][i2-1][i3-1][i4-1][i5-1]
    return pref

# Short implementation (cycle by masks)
# left borders - inclusive, right - exclusive
def query_prefix_sum_5d_short(pref: List[List[List[List[List[int]]]]],
                        l1: int, l2: int, l3: int, l4: int, l5: int,
                        r1: int, r2: int, r3: int, r4: int, r5: int) -> int:
    total = 0
    # The query rectangle is [l1, r1) × [l2, r2) × … × [l5, r5).
    # Its sum is the alternating sum over all 32 corner values.
    # Each corner is determined by a 5‑bit mask:
    #   bit d = 0  → take the right bound r_d
    #   bit d = 1  → take the left bound  l_d
    # Sign = + if the number of left bounds (bit_count) is even, else -.
    for mask in range(32):
        # Determine the index along each dimension
        i1 = l1 if (mask & 1) else r1
        i2 = l2 if (mask & 2) else r2
        i3 = l3 if (mask & 4) else r3
        i4 = l4 if (mask & 8) else r4
        i5 = l5 if (mask & 16) else r5

        corner = pref[i1][i2][i3][i4][i5]
        if mask.bit_count() % 2 == 0:
            total += corner
        else:
            total -= corner
    return total

if __name__ == "__main__":
    a = [[[[[0]*2 for _ in range(2)] for _ in range(2)] for _ in range(2)] for _ in range(2)]
    val = 1
    for i1 in range(2):
        for i2 in range(2):
            for i3 in range(2):
                for i4 in range(2):
                    for i5 in range(2):
                        a[i1][i2][i3][i4][i5] = val
                        val += 1

    pref = build_prefix_sum_5d(a)
    assert query_prefix_sum_5d(pref, 0,0,0,0,0, 1,1,1,1,1) == 1
    assert query_prefix_sum_5d(pref, 0,0,0,0,0, 2,2,2,2,2) == 528
    assert query_prefix_sum_5d(pref, 0,1,0,1,0, 1,2,1,2,1) == 11
    
    print("All 5D tests passed for standard implementation.")
    
    pref_short = build_prefix_sum_5d_short(a)
    assert query_prefix_sum_5d_short(pref, 0,0,0,0,0, 1,1,1,1,1) == 1
    assert query_prefix_sum_5d_short(pref, 0,0,0,0,0, 2,2,2,2,2) == 528
    assert query_prefix_sum_5d_short(pref, 0,1,0,1,0, 1,2,1,2,1) == 11
    
    print("All 5D tests passed for short implementation.")
    
    