using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class ListNode
    {
        public ListNode Prev { get; set; }
        public ListNode Next { get; set; }
        public ListNode Rand { get; set; }
        public string Data { get; set; }

        public ListNode()
        {
            Data = "";
        }
        public ListNode(string data)
        {
            Data = data;
        }
    }

    class ListRand : IEnumerable
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Add(string data)
        {
            ListNode node = new ListNode(data);

            if (Head == null)
                Head = node;
            else
            {
                Tail.Next = node;
                node.Prev = Tail;
            }
            Tail = node;
            Count++;
        }

        public void SetRandomNodes()
        {
            Random r = new Random();
            ListNode current = Head;

            while (current != null)
            {
                int k = r.Next(0, Count);
                int j = 0;

                ListNode tmp = Head;

                while (j < k)
                {
                    tmp = tmp.Next;
                    j++;
                }

                current.Rand = tmp;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            ListNode current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public void Serialize(FileStream s)
        {
            List<ListNode> tempList = new List<ListNode>();
            ListNode tempNode = Head;

            while (tempNode != null)
            {
                tempList.Add(tempNode);
                tempNode = tempNode.Next;
            }

            if (tempList.Count > 0)
                using (StreamWriter sw = new StreamWriter(s))
                {
                    foreach (ListNode l in tempList)
                        sw.WriteLine(l.Data.ToString() + ";" + tempList.IndexOf(l.Rand).ToString());

                    sw.Close();
                }
            else
                Console.WriteLine("Нет узлов для записи");
        }

        public void Deserialize(FileStream s)
        {
            List<ListNode> tempList = new List<ListNode>();
            List<int> rndList = new List<int>();
            ListNode tempNode = new ListNode();
            string line;

            Count = 0;
            Head = tempNode;

            using (StreamReader sr = new StreamReader(s))
            {
                while ((line = sr.ReadLine()) != null && !line.Equals(""))
                {
                    ListNode next = new ListNode();
                    string[] spltLine = line.Split(';');

                    tempNode.Data = spltLine[0];
                    rndList.Add(Convert.ToInt32(spltLine[1]));
                    tempNode.Next = next;
                    Count++;
                    tempList.Add(tempNode);
                    next.Prev = tempNode;
                    tempNode = next;
                }

                sr.Close();
            }

            for (int i = 0; i < Count; i++)
                tempList[i].Rand = tempList[rndList[i]];

            Tail = tempNode.Prev;
            Tail.Next = null;
        }
    }
}
