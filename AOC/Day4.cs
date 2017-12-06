using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Day4
    {
        public static void  Day4init()
        {
            string[] pws = File.ReadAllLines(@"F:\Projects\AOC2017\AOCREpo\AOC2017\AOC\datasets\Day4.txt");
            int counter = 0;
            foreach (string pw in pws)
            {
                Console.WriteLine("this " + pw + " is valid " + Day4.isValidPassword(pw));
                if (Day4.isValidPassword(pw))
                    counter++;
            }
            Console.WriteLine("num of valid ones " + counter);
            Console.Read();
        }

        public static void Day4init0()
        {
            string[] pws = File.ReadAllLines(@"C:\Users\pkrysiak\source\repos\AOC2017\AOC2017\datasets\Day40.txt");
            int counter = 0;
            foreach (string pw in pws)
            {
                Console.WriteLine("this " + pw + " is valid " + Day4.isValidANNPassword(pw));
                if (Day4.isValidANNPassword(pw))
                    counter++;
            }
            Console.WriteLine("num of valid ones " + counter);
            Console.Read();
        }
        public static bool isValidPassword(string pw)
        {
            string[] things = pw.Split(' ');
            for (int x = 0; x < things.Length; x++)
            {
                for (int y = 0; y < things.Length; y++)
                {
                    if (x != y && things[x] == things[y])
                        return false;
                }
            }
            return true;
        }

        public static bool isValidANNPassword(string pw)
        {
            string[] things = pw.Split(' ');
            for (int x = 0; x < things.Length; x++)
            {
                for (int y = 0; y < things.Length; y++)
                {
                    string check0 = String.Concat(things[x].OrderBy(c => c));
                    string check1 = String.Concat(things[y].OrderBy(c => c));
                    if (x != y && check0 == check1)
                        return false;
                }
            }
            return true;
        }
    }
}
