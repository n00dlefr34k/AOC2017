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
           
            SqlConnection conn =  new SqlConnection(@"server=AMBROSIA\CHARLOTTE;database=AOC2017;user=AOC2017;pwd=AOC2017;MultipleActiveResultSets=True;");

            string cleanTable = "truncate table[day6]";

            DatabaseFactory.UpdateDeleteProcedure(cleanTable, conn);

            int[] block = new int[] { 0, 2, 7, 0 };

            string insertStatement = "INSERT INTO[day6]([arrayNunber])VALUES('{0}')";

            int[] blocks = new int[] { 2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14 };

            List<int[]> existing = new List<int[]>();
            bool match = false;
            int count = 0;
            //while (count < 30000)
            while (!match)
            {
                existing.Add((int[])blocks.Clone());

                int max = blocks.Max();
                int index = Array.IndexOf(blocks, max);
                blocks[index] = 0;
                while (max > 0)
                {
                    index++;
                    int temp3 = Math.Abs(index % (blocks.Length));
                    blocks[temp3] += 1;
                    max--;
                }
                // fill the db with all the values, much faster 
                DatabaseFactory.UpdateDeleteProcedure(String.Format(insertStatement, JsonConvert.SerializeObject(blocks)), conn);
                int[][] temp = existing.ToArray();
                for (int x = 0; x < temp.Length; x++)
                {
                    for (int y = 0; y < temp.Length; y++)
                    {
                        // this didn't work with large arrays, i had to do it in the database
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

        private static void printblocks(int[] blocks)
        {
            for (int x = 0; x < blocks.Length; x++)
            {
                Console.Write(blocks[x] + " ");
            }
            Console.WriteLine(" ");
        }

        /* sql aggregate the links */
        //      select* ,
        //(Select top 3 CAST(f.id as nvarchar)+ ',' AS[text()],
        //ROW_NUMBER() OVER(ORDER BY columnid) AS ROW_NUM
        //              From dbo.[day6] as f
        //              Where f.[arrayNunber] = table3.[arrayNunber] and f.ROW_NUM > 1
        //              ORDER BY f.id
        //              For XML PATH ('')
        //          ) as ids

        //from(SELECT[arrayNunber],
        //              COUNT(*) as countt
        //          FROM

        //              [day6]
        //          GROUP BY

        //              [arrayNunber]
        //          HAVING
        //              COUNT(*) > 1 
        //		) as table3 order by ids

    }
}
