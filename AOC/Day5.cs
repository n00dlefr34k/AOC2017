using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Day5
    {
        public static void getInts()
        {
            string[] pws = File.ReadAllLines(@"F:\Projects\AOC2017\AOCREpo\AOC2017\AOC\datasets\Day5e.txt");
            int[] numbers =  Array.ConvertAll(pws, s => int.Parse(s));
            int Index = 0;
            int steps = 0;
            while (Index <= numbers.Length-1)
            {
                int tmp = numbers[Index];
                numbers[Index] += 1;
                steps++;
                Index += tmp;
            }
            Console.WriteLine("steps " + steps);
        }

        public static void getInts3()
        {
            string[] pws = File.ReadAllLines(@"C:\Users\pkrysiak\source\repos\AOC2017\AOC2017\datasets\Day5e.txt");
            int[] numbers = Array.ConvertAll(pws, s => int.Parse(s));
            int Index = 0;
            int steps = 0;
            while (Index <= numbers.Length - 1)
            {
                int tmp = numbers[Index];
                if (tmp > 2) {
                    numbers[Index] -= 1;
                }
                else
                {
                    numbers[Index] += 1;
                }
                steps++;
                Index += tmp;
            }
            Console.WriteLine("steps " + steps);
        }
    }
}
