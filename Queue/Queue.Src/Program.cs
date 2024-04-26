using DataStructures;

var queue = new DataStructures.Queue<int>();
int[] a = new int[10];
Console.WriteLine(a.Length);
Console.WriteLine(queue.CurrentCapacity);
queue.Enqueue(1, 1);
Console.WriteLine(queue.CurrentCapacity);
queue.Enqueue(2, 2);
Console.WriteLine(queue.CurrentCapacity);
queue.Enqueue(6, 6);
Console.WriteLine(queue.CurrentCapacity);
queue.Enqueue(8, 8);
Console.WriteLine(queue.CurrentCapacity);
queue.Enqueue(3, 3);
Console.WriteLine(queue.CurrentCapacity);
Console.WriteLine();

Console.WriteLine(queue.Dequeue());
Console.WriteLine(queue.Dequeue());
Console.WriteLine(queue.Dequeue());
Console.WriteLine(queue.Dequeue());
Console.WriteLine(queue.Dequeue());
Console.WriteLine(queue.Dequeue());