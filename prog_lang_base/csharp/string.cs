// reverse string
string s = "abcde";
char[] chars = s.ToCharArray();
Array.Reverse(chars);
string reversedS = new string(chars); // "edcba"


// get the index of the beginning of a substring in a string
string s1 = "abcdefg";
int idx = s1.IndexOf("f"); // 5


// split the string by the space character ' '
string text1 = "one two three ";
string[] parts1 = text1.Split(' '); // ["one", "two", "three", ""]


// split the string by the space character ' ' and remove the empty elements
string text2 = "one two three ";
string[] parts2 = text2.Split(' ', StringSplitOptions.RemoveEmptyEntries); // ["one", "two", "three"]


// check if the letter is capitalized
string s2 = "bAcc";
bool isUpper = char.IsUpper(s2[1]);


// StringBuilder
StringBuilder builder = new StringBuilder();
// add string to StringBuilder
builder.Append("abcd"); // "abcd"
// delete characters from StringBuilder
builder.Remove(1, 3); // "a"
// insert a row in a specific place by index
builder.Insert(0, "def"); // "defa"
// assign a new index value
builder[0] = 'r'; // "refa"
// turn StringBuilder into a string
string str = builder.ToString();



        