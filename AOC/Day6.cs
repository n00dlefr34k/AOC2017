using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoTaskCostReport.DAL;
using Newtonsoft.Json;

namespace AOC
{
    class Day6
    {
        public static void day6Init()
        {
            /*sql degbugging 
             SqlConnection conn =  new SqlConnection(@"server=AMBROSIA\CHARLOTTE;database=AOC2017;user=AOC2017;pwd=AOC2017;MultipleActiveResultSets=True;");
             string insertStatement = "INSERT INTO[day6]([arrayNunber])VALUES('{0}')";
             string cleanTable = "truncate table[day6]";
             DatabaseFactory.UpdateDeleteProcedure(cleanTable, conn);*/

            int[] block = new int[] { 0, 2, 7, 0 };
            int[] blocks = new int[] { 2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14 };

            List<int[]> existing = new List<int[]>();
            bool match = false;
            int count = 0;
            // for sql debugging
            //while (count < 30000)
            while (!match)
            {
                // must do the clone thing or all the values in the list get chnaged when you change the array values
                existing.Add((int[])blocks.Clone());

                int max = blocks.Max();
                int index = Array.IndexOf(blocks, max);
                // set  the max index to 0
                blocks[index] = 0;
                //dispurse the max value among the rest of the 
                //indexes startign at the next index from the max value
                while (max > 0)
                {
                    index++;
                    int temp3 = Math.Abs(index % (blocks.Length));
                    blocks[temp3] += 1;
                    max--;
                }
                // fill the db with all the values, much faster then code just to see if i was on the right track
                //DatabaseFactory.UpdateDeleteProcedure(String.Format(insertStatement, JsonConvert.SerializeObject(blocks)), conn);

                //drop the list into an arraqy for preformance and list can't do what i need them to do contain didn't do a thing on this one.
                int[][] temp = existing.ToArray();
                for (int x = 0; x < temp.Length; x++)
                {
                    for (int y = 0; y < temp.Length; y++)
                    {
                        // check if the sequesnce is hte same
                        if (x != y && temp[x].SequenceEqual(temp[y]))
                        {
                            match = true;
                            Console.WriteLine("distance between " + Math.Abs(x - y));
                        }

                    }
                }

                count++;
                if (count % 10 == 0)
                    Console.WriteLine("ittereations  " + count.ToString());
            }
            Console.WriteLine("steps " + --count);
        }

    }
}
