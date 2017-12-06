using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Day3
    {
        public static int[,] Spiral(int n)
        {
            int[,] result = new int[n,n];

            int pos = 0;
            int count = n;
            int value = -n;
            int sum = -1;

            do
            {
                value = -1 * value / n;
                for (int i = 0; i < count; i++)
                {
                    sum += value;
                    result[sum / n, sum % n] = pos++;
                }
                value *= n;
                count--;
                for (int i = 0; i < count; i++)
                {
                    sum += value;
                    result[sum / n, sum % n] = pos++;
                }
            } while (count > 0);

            return result;
        }


        // Method to print arrays, pads numbers so they line up in columns
        public static void PrintArray(int[,] array)
        {
            int n = (array.GetLength(0) * array.GetLength(1) - 1).ToString().Length + 1;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j].ToString().PadLeft(n, ' '));
                }
                Console.WriteLine();
            }
        }

        public static int getAnswer(int target)
        {
            int x = 1;
            int targetnum = target;
            int xt = 0;
            int yt = 0;
            bool check = false;
            while (!check)
            {
                x++;
                if (x * x >= targetnum)
                    check = true;
            }

            int[,] result = Day3.Spiral(x);
            int max = 0;
            for (int z = 0; z < x; z++)
            {
                for (int q = 0; q < x; q++)
                {
                    if (result[z, q] > max)
                        max = result[z, q];
                }
            }


            for (int z = 0; z < x; z++)
            {
                for (int q = 0; q < x; q++)
                {

                    result[z, q] = max - result[z, q];
                }
            }

            for (int z = 0; z < x; z++)
            {
                for (int q = 0; q < x; q++)
                {
                    if (targetnum - 1 == result[z, q])
                    {
                        xt = z;
                        yt = q;
                    }

                }
            }

            

            Console.WriteLine("max " + max);
            Console.WriteLine("target " + targetnum);
            Console.WriteLine("x/2 " + result[(x) / 2, ((x) - 1) / 2]);
            Console.WriteLine("origin cords :" + (x + 10) / 2 + ", " + ((x + 10) - 1) / 2);
            Console.WriteLine("Target cords :" + xt + ", " + yt);
            Console.WriteLine("answer : " + (Math.Abs(((((x) / 2) - xt)) + Math.Abs(((((x) - 1) / 2) - yt)))));
            return (Math.Abs(((((x) / 2) - xt)) + Math.Abs(((((x) - 1) / 2) - yt))));


        }
       

        public static int ForumlaThatIcantremember(int itterations)  
        {
            int summ = 1;
            for(int x = 0; x < itterations; x++)
            {
                summ += summ ^ 2;

                Console.WriteLine(summ);
            }
            return summ;
        }
    }

    class gridNode{

        public int value { get; set; }
       

    }
}
