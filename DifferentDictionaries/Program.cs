using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using RedBlackTree;
using AVLTree;


namespace DifferentDictionaries
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> d1 = new Dictionary<int, int>();
            RedBlackTreeDictionary<int, int> rb1 = new RedBlackTreeDictionary<int, int>();
            AVLDictionary<int, int> avl1 = new AVLTree.AVLDictionary<int, int>();

            var random = new Random();

            var InsertionRBT1 = new Stopwatch(); // calculating the running time
            Console.WriteLine("------------- Case of 320 elements---------------");
            InsertionRBT1.Start();
            for (int i = 0; i < 320; i++)
            {
                rb1.Add(i, random.Next(1000, 2000));
            }
            InsertionRBT1.Stop();
            Console.WriteLine("Taken time for inserting in miliseconds for Red-Black tree is {0} ", InsertionRBT1.ElapsedMilliseconds);
            var InsertionAVL1 = new Stopwatch(); // calculating the running time
            InsertionAVL1.Start();
            for (int j = 0; j < 320; j++)
            {
                avl1.Add(j, random.Next(2000, 3000));
            }
            InsertionAVL1.Stop();
            Console.WriteLine("Taken time for inserting in milliseconds for AVL tree is {0} ", InsertionAVL1.ElapsedMilliseconds);
            var InsertionDictionary1 = new Stopwatch();
            InsertionDictionary1.Start();
            for (int k = 0; k < 320; k++)
            {
                d1.Add(k, random.Next(6000, 7000));
            }
            InsertionDictionary1.Stop();
            Console.WriteLine("Taken time for inserting in milliseconds for hash-table Dictionary is {0}", InsertionDictionary1.ElapsedMilliseconds);
            var deletionRBT1 = new Stopwatch();
            deletionRBT1.Start();
            for (int i = 0; i < 320; i++)
                rb1.Remove(i);
            deletionRBT1.Stop();
            Console.WriteLine("Deletion in red black tree in milliseconds is {0} ", deletionRBT1.ElapsedMilliseconds);
            var deletionAVL1 = new Stopwatch();
            deletionAVL1.Start();

            for (int j = 0; j < 320; j++)
                avl1.Remove(j);
            deletionAVL1.Stop();
            Console.WriteLine("Deletion in AVL tree in milliseconds is {0} ", deletionAVL1.ElapsedMilliseconds);
            var deletionDictionary1 = new Stopwatch();
            deletionDictionary1.Start();
            for (int k = 0; k < 320; k++)
            {
                d1.Remove(k);
            }
            deletionDictionary1.Stop();
            Console.WriteLine("Deletion in hash-table based dictionary in milliseconds is {0} ", deletionDictionary1.ElapsedMilliseconds);
            Console.WriteLine("------------ Case of 640 elements------------------");
            RedBlackTreeDictionary<int, int> rb2 = new RedBlackTreeDictionary<int, int>();
            AVLDictionary<int, int> avl2 = new AVLDictionary<int, int>();
            Dictionary<int, int> d2 = new Dictionary<int, int>();
            var InsertionRBT2 = new Stopwatch();
            InsertionRBT2.Start();
            for (int i = 0; i < 640; i++)
            {
                rb2.Add(i, random.Next(3000, 5000));
            }
            InsertionRBT2.Stop();
            Console.WriteLine("Taken time in Red-Black tree for insertion in milliseconds is {0} ", InsertionRBT2.ElapsedMilliseconds);
            var InsertionAVL2 = new Stopwatch();
            InsertionAVL2.Start();
            for (int j = 0; j < 640; j++)
            {
                avl2.Add(j, random.Next(5000, 7000));
            }
            InsertionAVL2.Stop();
            Console.WriteLine("Taken time in AVL tree for insertion in milliseconds is {0} ", InsertionAVL2.ElapsedMilliseconds);
            var InsertionDictionary2 = new Stopwatch();
            InsertionDictionary2.Start();
            for (int k = 0; k < 640; k++)
            {
                d2.Add(k, random.Next(4000, 5000));
            }
            InsertionDictionary2.Stop();
            Console.WriteLine("Taken time in hash-based dictionary for insertion in millisecons is {0} ", InsertionDictionary2.ElapsedMilliseconds);
            var DeletionRBT2 = new Stopwatch();
            DeletionRBT2.Start();
            for (int i = 0; i < 640; i++)
            {
                rb2.Remove(i);
            }
            DeletionRBT2.Stop();
            Console.WriteLine("Taken time in Red-Black tree for deletion in milliseconds is {0} ", DeletionRBT2.ElapsedMilliseconds);
            var DeletionAVL2 = new Stopwatch();
            DeletionAVL2.Start();
            for (int j = 0; j < 640; j++)
            {
                avl2.Remove(j);
            }
            DeletionAVL2.Stop();
            Console.WriteLine("Taken time in AVL tree for deletion in milliseconds is {0} ", DeletionAVL2.ElapsedMilliseconds);
            var DeletionDictionary2 = new Stopwatch();
            DeletionDictionary2.Start();
            for (int k = 0; k < 640; k++)
            {
                d2.Remove(k);
            }
            DeletionDictionary2.Stop();
            Console.WriteLine("Taken time in hash-table based dictionary for deletion in milliseconds is {0} ", DeletionDictionary2.ElapsedMilliseconds);



            Console.WriteLine("------------ Case of 1280 elements------------------");
            // I know that I could clear my tree instead of creating a new instance, but it is already done :)
            RedBlackTreeDictionary<int, int> rb3 = new RedBlackTreeDictionary<int, int>();
            AVLDictionary<int, int> avl3 = new AVLDictionary<int, int>();
            Dictionary<int, int> d3 = new Dictionary<int, int>();
            var InsertionRBT3 = new Stopwatch();
            InsertionRBT3.Start();
            for (int i = 0; i < 1280; i++)
            {
                rb3.Add(i, random.Next(3000, 5000));
            }
            InsertionRBT3.Stop();
            Console.WriteLine("Taken time in Red-Black tree for insertion in milliseconds is {0} ", InsertionRBT3.ElapsedMilliseconds);
            var InsertionAVL3 = new Stopwatch();
            InsertionAVL3.Start();
            for (int j = 0; j < 1280; j++)
            {
                avl3.Add(j, random.Next(5000, 7000));
            }
            InsertionAVL3.Stop();
            Console.WriteLine("Taken time in AVL tree for insertion in milliseconds is {0} ", InsertionAVL3.ElapsedMilliseconds);
            var insertionDictionary3 = new Stopwatch();
            insertionDictionary3.Start();
            for (int k = 0; k < 1280; k++)
            {
                d3.Add(k, random.Next(7000, 9000));
            }
            insertionDictionary3.Stop();
            Console.WriteLine("Taken time in hash-table based dictionary in milliseconds is {0} ", insertionDictionary3.ElapsedMilliseconds);
            var DeletionRBT3 = new Stopwatch();
            DeletionRBT3.Start();
            for (int i = 0; i < 1280; i++)
            {
                rb3.Remove(i);
            }
            DeletionRBT3.Stop();
            Console.WriteLine("Taken time in Red-Black tree for deletion in milliseconds is {0} ", DeletionRBT3.ElapsedMilliseconds);
            var DeletionAVL3 = new Stopwatch();
            DeletionAVL3.Start();
            for (int j = 0; j < 1280; j++)
            {
                avl3.Remove(j);
            }
            DeletionAVL3.Stop();
            Console.WriteLine("Taken time in AVL tree for deletion in milliseconds is {0} ", DeletionAVL3.ElapsedMilliseconds);
            var deletionDictionary3 = new Stopwatch();
            deletionDictionary3.Start();
            for (int k = 0; k < 1280; k++)
            {
                d3.Remove(k);
            }
            deletionDictionary3.Stop();
            Console.WriteLine("Taken time in hash-table based dictionary for deletion in milliseconds is {0} ", deletionDictionary3.ElapsedMilliseconds);
            //same for retireve
        }
    }
}
