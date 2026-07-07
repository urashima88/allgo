// sorting a list of lists (each inner list has a length of 2, i.e. a pair) by the first value, and then by the second
var data = new List<List<int>>
{
    new List<int> { 2, 5 },
    new List<int> { 1, 3 },
    new List<int> { 2, 1 },
    new List<int> { 1, 2 }
};

// method 1: Sort method with lambda comparison, changes the original list
data.Sort((a, b) =>
{
    int cmp = a[0].CompareTo(b[0]);
    if (cmp != 0) return cmp;
    return a[1].CompareTo(b[1]); 
});
// or
data.Sort((a, b) =>
{
    int cmp = a[0].CompareTo(b[0]);
    return cmp != 0 ? cmp : a[1].CompareTo(b[1]);
});

// method 2: LINQ – without changing the original list, creates a new sorted sequence
var sorted1 = data
    .OrderBy(x => x[0])
    .ThenBy(x => x[1])
    .ToList(); // if you need List


// create a List of a certain number of zeros
List<int> list = new List<int>(new int[10]);


// concatenate a List with another List
List<int> list1 = new List<int>() {1, 2, 3};
List<int> list2 = new List<int>() {4, 5, 6};
list1.AddRange(list2);


