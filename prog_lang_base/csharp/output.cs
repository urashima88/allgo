// carriage return
Console.WriteLine("10%\r20%"); // -> 20%


// a string in which special characters are not allowed
Console.WriteLine(@"C:\Users\admin");
// the only character that needs to be escaped inside a special string is quotation marks
// they are escaped by doubling
Console.WriteLine(@"This is "" quotes");


// formatted output
int a1 = 13;
double a2 = 14.3456789;
Console.WriteLine("{0} {1}", a1, a2); // 13 14,3456789
// or
string formattedString = string.Format("{0} {1}", a1, a2);
// setting the rounding accuracy
Console.WriteLine("{0:0.000} {1:0.0000}", 1.23456, 1.23456); // 1,235 1,2346
// output of terminating zeros
Console.WriteLine("{0:0.000} {1:0.###}", 1.2, 1.2); // 1,200 1,2
// finishing with zeros on the left
Console.WriteLine("{0:D4}", 42); //0042
// columnization and right edge alignment
Console.WriteLine("{0,10}|\n{1,10}|", 12345, 123);
//		12345|
//		  123|
// or left edge alignment
Console.WriteLine("{0,-10}|\n{1,-10}|", 12345, 123);
// 12345	|
// 123		|
// combinations of alignment and rounding
Console.WriteLine("{0,10:0.00}|\n{1,10:0.000}|", 1.45, 21.345);
//		1,45|
//	  21,345|
// formatting time and date
Console.WriteLine("{0:hh:mm:ss}", DateTime.Now); // 06:01:54
// MM and mm are Months and minutes. They differ only in case
Console.WriteLine("{0:yy-MM-dd}", DateTime.Now); // 17-07-19
// can change the number of letters and the order
Console.WriteLine("{0:d-MM-yyyy}", DateTime.Now); // 1-12-2014