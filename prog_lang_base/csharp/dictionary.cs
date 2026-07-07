// verify that the key exists in the dictionary
Dictionary<string, int> map = new Dictionary<string, int>()
{
    {"a", 0}, {"b", 1}, {"c", 2}
};
bool exists = map.ContainsKey("b"); // True


// iterating through key-value pairs in the dictionary
Dictionary<int, int> map2 = new Dictionary<int, int>() {{1, 2}, {2, 3}};
foreach (var x in map2)
{
    var key = x.Key;
    var value = x.Value;
}


// getting dictionary values and keys
Dictionary<int, int> map3 = new Dictionary<int, int>() {{1, 2}, {3, 4}};
var keys = map3.Keys;
var values = map3.Values; 


// sort the dictionary by descending value, then by ascending lexicographic order
Dictionary<string, int> counter = new Dictionary<string, int>() {
    {"c d", 3}, {"a b", 3}, {"a c", 5}
};
var sorted = counter.OrderByDescending(kvp => kvp.Value).ThenBy(kvp => kvp.Key);


// a short implementation for counting elements and writing values to a dictionary
string s3 = "arrbitsfbbrggb";
Dictionary<char, int> counter2 = new Dictionary<char, int>();
foreach (char ch in s3)
{
    counter2[ch] = counter2.GetValueOrDefault(ch) + 1; 
}

