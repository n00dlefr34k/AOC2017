using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Day7
    {
        public static void day7init()
        {
          var  nodeTree = createNodeTree();

            //get the rootnode
            day7TowerNode rootNode = nodeTree.Single(n => n.nodepointer == null);
            List<day7TowerNode> children = nodeTree.Where(n => n.nodepointer == rootNode.id).ToList();
            List<double> summs = new List<double>();
            int count = 0;
            foreach(day7TowerNode child in children)
            {
                Console.WriteLine("-" + child.name);
               double x = 0;
                summs.Add(AddTheWeights(nodeTree,child, x, count));
            }
            
           // File.AppendAllText("json.json", JsonConvert.SerializeObject(nodeTree));
            Console.WriteLine(JsonConvert.SerializeObject(rootNode));
            Console.WriteLine(JsonConvert.SerializeObject(summs));
            //Console.WriteLine(JsonConvert.SerializeObject(parent));
        }

        private static double AddTheWeights(List<day7TowerNode> nodeTree,day7TowerNode child, double x,int level)
        {

            level++;
            x += child.weight;
            string indent = "-";
            for (int z = 0;z< level; z++)
                indent += "-";
            Console.WriteLine(indent + child.name + " " + child.weight + " current total :" + x);
            List<day7TowerNode> children = nodeTree.Where(n => n.nodepointer == child.id).ToList();
            if(children != null)
            {
                foreach(day7TowerNode chi in children)
                {
                   x= AddTheWeights(nodeTree, chi, x, level);
                }
            }
            return x;
        }

        private static List<day7TowerNode> createNodeTree()
        {
            string[] fileinfo = File.ReadAllLines(@"F:\Projects\AOC2017\AOCREpo\AOC2017\AOC\datasets\Day7.txt");

            List<day7TowerNode> nodeTree = new List<day7TowerNode>();
            int counter = 0;

            foreach (string node in fileinfo)
            {
                string[] result = node.Split(' ');
                nodeTree.Add(new day7TowerNode()
                {
                    id = counter++,
                    name = result[0].Trim(),
                    weight = trytoconvertToInt(result[1]),
                    nodepointer = null
                });
            }
            foreach (string node in fileinfo)
            {
                if (node.Contains("-"))
                {
                    string[] result = node.Split(' ');
                    day7TowerNode nodeObj = nodeTree.Single(g => g.name == result[0].Trim());
                    for (int x = 3; x < result.Length; x++)
                    {
                        day7TowerNode nodeObjToSet = nodeTree.FirstOrDefault(n => n.name == result[x].Replace(",", "").Trim());
                        if (nodeObjToSet != null)
                        {
                            int indexToWriteTo = nodeTree.IndexOf(nodeObjToSet);
                            nodeTree[indexToWriteTo].nodepointer = nodeObj.id;
                        }
                    }

                }
            }

            return nodeTree;
        }

        private static int trytoconvertToInt(string v)
        {
            try
            {
                return Convert.ToInt32(v.Replace("(", "").Replace(")", ""));
            }
            catch
            {
                return 0;
            }
        }
    }
    public class day7TowerNode
    {
        public int id { get; set; }
        public string name { get; set; }
        public int weight { get; set; }
        public int? nodepointer { get; set; }
    }
}
