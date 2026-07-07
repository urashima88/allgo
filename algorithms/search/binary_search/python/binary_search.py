from typing import Callable, List

def first_true(low: int, high: int, P: Callable[[int], bool]) -> int:
    """
    Return the first index x in [low, high] where P(x) is True.
    Returns high + 1 if all values are False.
    """
    l = low - 1        # P(l) is False
    r = high + 1       # P(r) is True
    while r - l > 1:
        mid = l + (r - l) // 2          # lower mid
        if P(mid):
            r = mid
        else:
            l = mid
    return r

def last_true(low: int, high: int, P: Callable[[int], bool]) -> int:
    """
    Return the last index x in [low, high] where P(x) is True.
    Returns low - 1 if all values are False.
    """
    l = low - 1        # P(l) is True
    r = high + 1       # P(r) is False
    while r - l > 1:
        mid = l + (r - l + 1) // 2      # upper mid
        if P(mid):
            l = mid
        else:
            r = mid
    return l

def lower_bound(a: List[int], target: int) -> int:
    """First index i such that a[i] >= target. Returns len(a) if not found."""
    return first_true(0, len(a) - 1, lambda i: a[i] >= target)

def upper_bound(a: List[int], target: int) -> int:
    """First index i such that a[i] > target. Returns len(a) if not found."""
    return first_true(0, len(a) - 1, lambda i: a[i] > target)

if __name__ == "__main__":
    a = [1, 3, 5, 7, 9]

    assert lower_bound(a, 5) == 2
    assert lower_bound(a, 6) == 3
    assert lower_bound(a, 10) == len(a)   # 5

    assert upper_bound(a, 5) == 3
    assert upper_bound(a, 0) == 0
    assert upper_bound(a, 9) == len(a)

    # last_true: last element <= 5
    last_le = last_true(0, len(a) - 1, lambda i: a[i] <= 5)
    assert last_le == 2

    # first_true: smallest x with x*x >= 25
    ans = first_true(0, 10, lambda x: x * x >= 25)
    assert ans == 5

    print("All binary search tests passed.")