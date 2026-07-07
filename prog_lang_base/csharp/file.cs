// write text to a file
File.WriteAllText("1.txt", "Hello, world!");


// find out the path relative to the "current directory"
Console.WriteLine(Environment.CurrentDirectory);


// find out the location of the running exe file
Console.WriteLine(Assembly.GetExecutingAssembly().Location);


// form an absolute path based on a relative one
Console.WriteLine(Path.Combine(Environment.CurrentDirectory, "1.txt"));


// write lines to a file,
// separating them with an end-of-line character (\r\n for Windows and \n for Linux and macOS)
File.WriteAllLines("2.txt", new[] {"Hello", "world"});


// write an array of bytes in binary format to a file
File.WriteAllBytes("3.dat", new byte[10240]);


// read data from a file
string text = File.ReadAllText("1.txt");
string[] lines = File.ReadAllLines("2.txt");
byte[] bytes = File.ReadAllBytes("3.dat");


// all files in the current directory (the dot in the path means the current directory)
foreach (var file in Directory.GetFiles("."))
    Console.WriteLine(file);