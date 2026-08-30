using System;
using System.Collections.Generic;
using System.Linq;

// Directions: up, down, left, right
(int dr, int dc)[] directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };

List<(int r, int c)> ShortestPath(List<List<int>> grid, (int r, int c) start, (int r, int c) target)
{
    int rows = grid.Count;
    int cols = grid[0].Count;
    int[,] dist = new int[rows, cols];
    (int r, int c)[,] parent = new (int r, int c)[rows, cols];

    // Initialize distances with -1
    for (int i = 0; i < rows; i++)
        for (int j = 0; j < cols; j++)
            dist[i, j] = -1;

    Queue<(int r, int c)> q = new Queue<(int r, int c)>();
    q.Enqueue(start);
    dist[start.r, start.c] = 0;
    parent[start.r, start.c] = (-1, -1);

    bool found = false;
    while (q.Count > 0 && !found)
    {
        (int r, int c) u = q.Dequeue();
        foreach ((int dr, int dc) in directions)
        {
            int nr = u.r + dr;
            int nc = u.c + dc;
            if (nr < 0 || nr >= rows || nc < 0 || nc >= cols)
                continue;
            if (grid[nr][nc] == 1 || dist[nr, nc] != -1)
                continue;

            dist[nr, nc] = dist[u.r, u.c] + 1;
            parent[nr, nc] = u;
            q.Enqueue((nr, nc));
            if (nr == target.r && nc == target.c)
            {
                found = true;
                break;
            }
        }
    }

    if (!found)
        return new List<(int r, int c)>();

    // Reconstruct path
    List<(int r, int c)> path = new List<(int r, int c)>();
    (int r, int c) cur = target;
    while (cur != start)
    {
        path.Add(cur);
        cur = parent[cur.r, cur.c];
    }
    path.Add(start);
    path.Reverse();
    return path;
}

// ---------- Tests ----------
void Assert(bool condition)
{
    if (!condition)
    {
        Console.WriteLine("Test failed");
        Environment.Exit(1);
    }
}

var grid = new List<List<int>>
{
    new List<int> { 0, 0, 0, 0, 0 },
    new List<int> { 0, 1, 1, 1, 0 },
    new List<int> { 0, 0, 0, 0, 0 },
    new List<int> { 0, 1, 1, 1, 0 },
    new List<int> { 0, 0, 0, 0, 0 }
};
var start = (0, 0);
var target = (4, 4);
var path = ShortestPath(grid, start, target);

Assert(path.Count > 0);
Assert(path[0] == start);
Assert(path[^1] == target);
Assert(path.Count == 9);  // 8 steps

Console.WriteLine("All tests passed.");