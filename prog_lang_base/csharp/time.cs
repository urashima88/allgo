// calculate execution time via Stopwatch
Stopwatch watch = new Stopwatch();
watch.Start();
// ...
watch.Stop();
// вывести время выполнения в миллисекундах
Console.WriteLine(watch.ElapsedMilliseconds);

