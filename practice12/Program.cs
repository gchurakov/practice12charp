using System;
using System.Collections.Generic;
using EngineLib;

namespace practice12
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*
            #region DoubleLinkedList
            //создание
            DLinkList dllist = DLinkList.GenerateDList(9);
            dllist.Show();
            //удаление
            Console.WriteLine();
            dllist.DelElems();
            dllist.Show();
            //очистка
            Console.WriteLine();
            dllist = null;
            //DLinkList.Show(dllist);// надо ли это вообще?
            #endregion
            */
            
            /*
            #region BinaryTree
            //создание
            BinTree btree = BinTree.IdealTree(10, new BinTree());
            //BinTree.ShowTree(btree, 4);
            Console.WriteLine();
            //преобразование
            int i = 0;
            BinTree.ToSearchTree(btree.Count, btree, BinTree.GetEngsArray(btree), ref i);
            BinTree.ShowTree(btree, 4);
            BinTree.GetEngsArray(btree);
            //поиск по условию
            Console.WriteLine($"\nКоличество Двигателей Мощностью < 200 : {BinTree.SearchEngs(btree)} ");
            //удаление
            btree = null;

            #endregion
            */
            /*
            #region HashTable

            HTable htable = new HTable(2);
            
            htable.Print();
            Console.WriteLine();
            htable.Add(new Engine(1,1,1));
            htable.Add(new Engine(1,1,1));
            htable.Add(new Engine(1));
            htable.Print();
            
            Console.WriteLine($"поиск существующего элемента :  {htable.FindPoint(new Engine(1, 1, 1))}");
            Console.WriteLine($"поиск НЕсуществующего элемента :  {htable.FindPoint(new Engine(1))}");
            
            
            htable.DelPoint(new Engine(1, 1, 1));
            htable.DelPoint(new Engine(1, 1, 1));
            htable.DelPoint(new Engine(1));
            Console.WriteLine();
            htable.Print();
            Console.WriteLine();

            #endregion*/
            
            
            MyCollection<Engine> c = new MyCollection<Engine>(6);
            
            c.Add(new Engine(1,1,1));
            c.Add(new Engine(1));
            c.Add(new Engine(1));
            c.Add(new Engine(1));
            c.Add(new Engine(1));
            c.Add(new Engine(1));
            c.Add(new Engine(1));

            MyCollection<Engine> c1 = new MyCollection<Engine>(c);
            
            MyCollection<Engine>.Show(c1);
            Console.WriteLine();
            Console.WriteLine(c[1]);
            Console.WriteLine();
            Console.WriteLine(c.Count);
            
            Console.WriteLine(c1.IsReadOnly);

            Console.WriteLine(c1.Contains(new Engine(1,1,1)));//true
            Console.WriteLine(c.Contains(new Engine(1)));//false


            Engine[] arr = new Engine[c.Count];
            c.CopyTo(arr);
            c.CopyTo(arr, 1);
            

            MyCollection<Engine> c2 = c.Clone();
            MyCollection<Engine> c3 = c.ShallowCopy();
            c.Del();
            
            c1.Clear();
            
            MyCollection<Engine>.Show(c);
            Console.WriteLine();
            MyCollection<Engine>.Show(c1);
            Console.WriteLine();
            MyCollection<Engine>.Show(c2);
            Console.WriteLine();
            MyCollection<Engine>.Show(c3);
            
            Console.WriteLine("\n"+c2.Count);
            c2.Remove(new Engine(1,1,1));
            c2.Remove(new Engine(1));
            c2.Del();
            Console.WriteLine(c2.Count);
            Console.ReadKey();
        }
    }
}
/*
 
реализовать интерфейсы


2.1.  Задание 1. 
*    1.	Сформировать двунаправленный список, в информационное поле записать объекты из иерархии классов лабораторной работы №10.
*    2.	Распечатать полученный список.
*    3.	Удалить из списка все элементы с четными номерами (2, 4, 6 и. т. д.)..
*    4.	Распечатать полученный список.
?    5.	Удалить список из памяти.

2.2.  Задание 2. 
*    1.	Сформировать идеально сбалансированное бинарное дерево, в информационное поле записать объекты из иерархии классов лабораторной работы №10.
*    2.	Распечатать полученное дерево.
*    3.	Найти количество элементов дерева, у которых поле (например, имя) начинается с заданного символа.
    4.	Преобразовать идеально сбалансированное дерево в дерево поиска.
*    5.	Распечатать полученное дерево. 
*    6.	Удалить дерево из памяти.
    
2.3. Задание 3
*    1.	Создать хеш-таблицу и заполнить ее элементами.
    2.	Выполнить поиск элемента в хеш-таблице
    3.	Удалить найденный элемент из хеш-таблицы.
    4.	Выполнить поиск элемента в хеш-таблице
    5.	Показать, что будет при добавлении элемента в хеш-таблицу, если в таблице уже находится максимальное число элементов 
        (для метода открытой адресации, для метода цепочек просто показать добавление в таблицу).
 
2.4. Задание 4
Реализовать  обобщенную коллекцию, Стек на базе однонаправленного списка.  Для этого:
1.	Реализовать конструкторы:
    •	public MyCollection() - предназначен для создания пустой коллекции.
    •	public MyCollection (int capacity) - создает пустую коллекцию с начальной емкостью, заданной параметром capacity.
    •	public MyCollection (MyCollection c) - служит для создания коллекции, которая инициализируется элементами и емкостью коллекции, заданной параметром с.
2.	Для всех коллекций реализовать:
    •	свойство Count, позволяющее получить количество элементов в коллекции;
    •	методы для добавления одного или нескольких элементов в коллекцию;
    •	методы для удаления одного или нескольких элементов из коллекции (кроме деревьев); 
    •	метод для поиска элемента по значению;
    •	метод для клонирования коллекции;
    •	метод для поверхностного копирования;
    •	метод для удаления коллекции из памяти.
3.	Реализовать интерфейсы IEnumerable и IEnumerator (если это необходимо).
4.	Написать демонстрационную программу, в которой создаются коллекции, и демонстрируется работа всех реализованных методов, в том числе, перебор коллекции циклом foreach.
/rfr elfkbnm dct 'ktvtyns rjkkbpbb??,ektdjt gjkt?
*/