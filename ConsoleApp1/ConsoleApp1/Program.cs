using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ListRand linkedList = new ListRand();

            linkedList.Add("item0");
            linkedList.Add("item1");
            linkedList.Add("item2");
            linkedList.Add("item3");
            linkedList.Add("item4");

            linkedList.SetRandomNodes();

            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }

            using (FileStream fs = new FileStream("test.json", FileMode.OpenOrCreate))
            {
                linkedList.Serialize(fs);
                Console.WriteLine("Объект сериализован");
            }

            linkedList = new ListRand();

            using (FileStream fs = new FileStream("test.json", FileMode.Open))
            {
                linkedList.Deserialize(fs);
                Console.WriteLine("Объект десериализован");
            }

            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
