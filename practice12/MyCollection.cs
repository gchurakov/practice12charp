using System;
using System.Collections;
using System.Collections.Generic;
using practice12;

namespace practice12
{
    public class Point<T>
    {
        public T data;
        public Point<T> prev = null;
        
        public Point(){}

        public Point(T _data)
        {
            data = _data;
        }

        public Point(T _data, Point<T> _prev)
        {
            data = _data;
            prev = _prev;
        }

        public override string ToString()
        {
            // демонстрация элемента
            if (data == null)
                return "\n";
            return data.ToString() + " -> ";
        }

        //создание элемента списка
        public static Point<T> MakePoint(T _data = default(T))
        {
            T data = _data;
            return new Point<T>(data);
        }
        
    }
    

    public class MyCollection<T> : ICollection<T>
    {
        private int capacity=-1;
        public Point<T> last; // список
        
        // свойства
        private int Capacity// предельная емкость коллекции, больше наполнить нельзя
        {
            get => capacity;
            set
            {
                if (value < 0)
                {
                    capacity = -1;
                    Console.WriteLine("Value must be > 0 !");
                }
                else
                {
                    capacity = value;
                    //урезание списка до размеров
                    int countElems = this.Count;
                    if (capacity < countElems)
                    {
                        int i = 1;
                        while (capacity < countElems - i)
                            this.Del();
                    }
                }
            }
        }

        public int Count//текущее количество элементов в колекции
        {
            get
            {
                int i = 0;
                Point<T> temp = this.last;
                while (temp != null)
                {
                    temp = temp.prev;
                    i++;
                }
                return i;
            }
        }
        
        public bool IsReadOnly { get=>true; }

        //Constructors
        public MyCollection() { }

        public MyCollection(int _capacity)
        {
            Capacity = _capacity;
        }

        public MyCollection(MyCollection<T> c)
        {
            MyCollection<T> temp=new MyCollection<T>();
            capacity = c.capacity;
            int n = c.Count;
            foreach (T elem in c)
                temp.Add(elem);
            foreach (T elem in temp)
                this.Add(elem);
        }

        virtual public T this[int index]
        {
            //доступ к значениям по []
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException();
                Point<T> current = this.last;
                for (int i = 0; i < index; i++)
                {
                    if (current.prev == null)
                        return default;
                        //throw new ArgumentOutOfRangeException();
                    current = current.prev;
                }
                return current.data;
            }
        }
        
        public static void Show(MyCollection<T> c)
        {
            //печать списка
            if (c.Count == 0)
                Console.WriteLine("Пустой список!");
            else
            {
                Point<T> temp = c.last;
                while (temp != null)
                {
                    Console.Write(temp.ToString());
                    temp = temp.prev;
                }
            }
            
        }
        
        
        public void Del()
        {
            // удаление верхнего элемента
            if (this.last == null)//пустой список
                Console.WriteLine("Пустой стек!");//нужен эксепшн?
                //throw new NullReferenceException();
            else
                this.last = this.last.prev;
        }
        
        //                                                       IEnumerable<T>
        IEnumerator IEnumerable.GetEnumerator()        
        {
            // интерфейс
            //реализовать необобщенный нумератор 
            return GetEnumerator();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            // интерфейс
            //реализовать обобщенный нумератор 
            Point<T> current = last;
            while (current != null)
            {
                yield return current.data;
                current = current.prev;
            }
        }

        
        //                                                       ICollection<T>
        public void Add(T _data)
        {
            // интерфейс
            //добавление нового обтекта
            if (Capacity < 0 || Capacity > Count)
            {
                Point<T> newPoint = Point<T>.MakePoint(_data);
                newPoint.prev = this.last;
                this.last = newPoint;// Point<T>(last.data, last)
            }
            else
                Console.WriteLine("Стек заполнен!");
        }
        
        public void Clear()
        {
            // интерфейс
            //очистка стека
            last = null;
        }

        public bool Contains(T _data)
        {
            // интерфейс
            //перебор стека с поиском элемента
            foreach (T elem in this)
                if (elem.Equals(_data))
                    return true;
            return false;
        }

        public bool Remove(T _data)//переприсвоить измененный список!!! и все!
        {
            // интерфейс
            //попытка удаления первого вхождения значения в стек
            int i = 0;
            //поиск позиции элемента
            foreach (T elem in this)
            {
                if (elem.Equals(_data))
                    break;
                i++;
            }
            
            //случаи нахождения элемента
            
            if (i == 0)//первый элемент
            {
                this.last = this.last.prev;
                return true;
            }
            else if (i >= Count - 1)//последний элемент
            {
                int j = 0;
                Point<T> temp = this.last;
                Point<T> tlast = this.last;
                while (j < i-2)//спускаемся по стеку
                {
                    temp = temp.prev;
                    j++;
                }

                temp.prev = null;//удаляем ссылку на последний элемент
                this.last = tlast;
                return true;
            }
            else if (i < Count - 1)//в середине
            {
                int j = 0;
                Point<T> temp = this.last;
                Point<T> tlast = this.last;
                while (j != i-2)//спускаемся по стеку
                {
                    temp = temp.prev;
                    j++;
                }

                temp.prev = temp.prev.prev;//заменяем ссылку на последний элемент
                this.last = tlast;
                return true;
            }
            return false;//не нашли элемент
        }


        public void CopyTo(T[] array, int arrayIndex=0)
        {
            // интерфейс
            //копирование элементов в массив
            if (arrayIndex >= 0)
            {
                if (array.Length < Count + arrayIndex)
                    Console.WriteLine("Не достаточно места в массиве!");
                //throw new Exception("Не достаточно места в массиве!");
                else
                    foreach (T _data in this)
                        array[arrayIndex++] = _data;
            }
            else
                Console.WriteLine("Индекс < 0!");
        }

        public MyCollection<T> ShallowCopy()
        {
            //поверхностоное копирование
            MyCollection<T> newC = new MyCollection<T>();
            newC.Capacity = this.capacity;
            newC.last = this.last;
            return newC;
        }

        public MyCollection<T> Clone()
        {
            //глубокое копирование
            return new MyCollection<T>(this);
        }
        
    }
    
}



/*
 * 2.4. Задание 4
Реализовать  обобщенную коллекцию, Стек на базе однонаправленного списка.  Для этого:
1.	Реализовать конструкторы:
*    •	public MyCollection() - предназначен для создания пустой коллекции.
*    •	public MyCollection (int capacity) - создает пустую коллекцию с начальной емкостью, заданной параметром capacity.
*    •	public MyCollection (MyCollection c) - служит для создания коллекции, которая инициализируется элементами и емкостью коллекции, заданной параметром с.
2.	Для всех коллекций реализовать:
*    •	свойство Count, позволяющее получить количество элементов в коллекции;
*    •	методы для добавления одного или нескольких элементов в коллекцию;
*    •	методы для удаления одного или нескольких элементов из коллекции (кроме деревьев); 
*    •	метод для поиска элемента по значению;
*    •	метод для клонирования коллекции;
*    •	метод для поверхностного копирования;
*    •	метод для удаления коллекции из памяти.
*3.	Реализовать интерфейсы IEnumerable и IEnumerator (если это необходимо).

4.	Написать демонстрационную программу, в которой создаются коллекции, и демонстрируется работа всех реализованных методов,
    в том числе, перебор коллекции циклом foreach.
 */