// add an item to the HashSet
HashSet<int> numbers = new HashSet<int> { 1, 2, 3 };
numbers.Add(4); // {1, 2, 3, 4}
numbers.Add(2); // {1, 2, 3, 4}
// get the number of elements in a HashSet
int numberCount = numbers.Count; // 4
// check that there is such an element
bool contains = numbers.Contains(3); // True

