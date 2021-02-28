using System;
using System.Threading.Tasks;

namespace CreatingAndStarting
{
    internal class Program
    {
        public static void Write(object o)
        {
            var i = 50;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }

        public static void Main(string[] args)
        {
            // Task.Factory.StartNew(() => Write("1"));
            // Write("2");
            // var task = new Task(() => Write(3));
            // task.Start();
            string text1 = "Testing";
            string text2 = "this";
            var task1 = new Task<int>(TextLength, text1);
            task1.Start();
            Task<int> task2 = Task.Factory.StartNew(TextLength, text2);
            Console.WriteLine($"Length of '{text1}' is {task1.Result}");
            Console.WriteLine($"Length of '{text2}' is {task2.Result}");
            Console.WriteLine("Main Program is Done");
            Console.ReadKey();
        }
    }
}