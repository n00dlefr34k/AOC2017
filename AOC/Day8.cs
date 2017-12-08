using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Day8
    {
        public static void day8init()
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            string[] fileinfo = File.ReadAllLines(@"F:\Projects\AOC2017\AOCREpo\AOC2017\AOC\datasets\Day8.txt");
            int maxvalHeld = 0;
            //fill everythgin with zed just because
            foreach (string instruction in fileinfo)
            {
                registers[instruction.Split(' ')[0]] = 0;
                registers[instruction.Split(' ')[4]] = 0;
            }
            //pprocess all the things!!
            for (int x = 0; x < fileinfo.Length; x++)
            {
                string[] temp = fileinfo[x].Split(' ');
                switch (temp[5])
                {
                    case "<":
                        if (registers[temp[4]] < ConvertInt(temp[6]))
                            processRegister(registers,temp[0], temp[1], ConvertInt(temp[2]));
                        break;
                    case ">":
                        if (registers[temp[4]] > ConvertInt(temp[6]))
                            processRegister(registers,temp[0], temp[1], ConvertInt(temp[2]));
                        break;
                    case "<=":
                        if (registers[temp[4]] <= ConvertInt(temp[6]))
                            processRegister(registers,temp[0], temp[1], ConvertInt(temp[2]));
                        break;
                    case ">=":
                        if (registers[temp[4]] >= ConvertInt(temp[6]))
                            processRegister(registers,temp[0], temp[1], ConvertInt(temp[2]));
                        break;
                    case "==":
                        if (registers[temp[4]] == ConvertInt(temp[6]))
                            processRegister(registers,temp[0], temp[1], ConvertInt(temp[2]));
                        break;
                    case "!=":
                        if (registers[temp[4]] != ConvertInt(temp[6]))
                            processRegister(registers,temp[0], temp[1], ConvertInt(temp[2]));
                        break;
                    default:
                        break;

                }
                if (maxvalHeld < registers.Values.Max())
                    maxvalHeld = registers.Values.Max();
            }
            //answers!! i need answers!!
            int maxc = registers.Values.Max();
            Console.WriteLine("maxVAl :" + maxc);
            Console.WriteLine("maxVAlheld :" + maxvalHeld);
            Console.WriteLine("key value of max val :" + JsonConvert.SerializeObject(registers.Where(x=>x.Value == maxc)));
            Console.WriteLine("The resulting dictionary");
            Console.WriteLine(JsonConvert.SerializeObject(registers));
        }

        private static void processRegister(Dictionary<string, int> registers, string registerToModify, string incDec, int? ValueToMove)
        {
            registers[registerToModify] += (incDec == "inc" ? 1 : -1) * (int)ValueToMove;
        }

       

        public static int? ConvertInt(string v)
        {
            try
            {
                return Convert.ToInt32(v);
            }
            catch
            {
                return null;
            }
        }
    }
    
}
