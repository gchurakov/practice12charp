using System;
using EngineLib;

namespace practice12
{
    class LPoint
    {
        public int key;//ключ
        public Engine eng;//значение  
        public bool isColliseum = false;
        
        public LPoint(Engine _eng)
        {
            eng = _eng;
            key = eng.GetHashCode();
        }
        
        public LPoint()
        {
            eng = new Engine(1);
            key = eng.GetHashCode();
        }
        public override string ToString()
        {
            return key + " : " + eng.ToString();
        }
    }


    class HTable
    {
        public LPoint[] point=null;//таблица
        public int Size;//размер
        private int Count=0;//заполненые ячейки

        public HTable(int amount = 4)
        {
            Size = amount * 3;
            point = new LPoint[Size];
            for (int i = 0; i < amount; i++)
                this.Add(new Engine(1));
        }

        public void Add(Engine eng)
        {
            LPoint newPoint = new LPoint(eng);

            if (Count >= Size / 2)
                Resize();
            
            int index = newPoint.key % Size;
            if (this.point[index] == null)
                this.point[index] = newPoint;
            else//колизия
            {
                while (this.point[index]!=null)
                {
                    this.point[index].isColliseum = true;
                    index = (index + 1) % Size;
                }

                newPoint.isColliseum = false;
                this.point[index] = newPoint;
                
            }
            Count++;
        }

        private void Resize()
        {
            LPoint[] newpoint = new LPoint[this.Size * 2];
            int index = 0;
            for (int i = 0; i < Size; i++)
            {
                
                if (point[i] == null)//пустое значение
                    continue;
                else
                {
                    index = point[i].key % (Size * 2);
                    if (newpoint[i] == null)
                        newpoint[index] = point[i];
                    else //колизия
                    {
                        while (newpoint[index] != null)
                        {
                            newpoint[index].isColliseum = true;
                            index = (index + 1) % (Size * 2);
                        }

                        newpoint[index] = point[i];
                    }
                }
            }
            this.point = newpoint;
            Size = Size * 2;
            Console.WriteLine("Размер таблицы увеличен в 2 раза.");
        }

        public void Print()
        {
            if (point == null)//пусто
            {
                Console.WriteLine("Таблица пустая!");
                return;
            }

            for (int i = 0; i < Size; i++)
            {
                if (point[i] == null)
                    Console.WriteLine(i + " : ");
                else
                    Console.WriteLine(i + " : " + point[i].ToString());
            }

        }

        public bool FindPoint(Engine eng)
        {
            LPoint lp = new LPoint(eng);
            int code = lp.key%Size;
            int start = 0;
            
            if (point[code] == null)//отсутствует
                Console.WriteLine("Элемент отсутствует.");

            else if (point[code].eng == eng)//на позиции
                return true;
            
            while (point[code] != null)//коллизия
            {
                if (point[code].eng == eng)
                    return true;
                if (code == start)
                    return false;
                code = (code+1) % Size;
            }
            return false;
        }
        
        
        public void DelPoint(Engine eng)
        {
            LPoint lp = new LPoint(eng);
            int index = lp.key%Size;
            
            if (point[index] == null)//отсутствует
                Console.WriteLine("Элемент отсутствует.");

            else if (point[index].eng == eng && point[index].isColliseum==false)//на позиции без коллизий
            {
                point[index] = null;
                Console.WriteLine("Элемент удален.");
            }
            else//коллизия
            {
                while (point[index].isColliseum)//поиcк последнего вхождения
                    index = (index+1) % Size;
                if (point[index].eng == eng)
                {
                    point[index] = null;
                    point[index - 1].isColliseum = false;
                    Console.WriteLine("Элемент удален.");
                }
                else
                    Console.WriteLine("Элемент отсутствует в таблице!");
            }
        }

    }

}
/*
 2.3. Задание 3
*    1.	Создать хеш-таблицу и заполнить ее элементами.
    2.	Выполнить поиск элемента в хеш-таблице
    3.	Удалить найденный элемент из хеш-таблицы.
    4.	Выполнить поиск элемента в хеш-таблице
    5.	Показать, что будет при добавлении элемента в хеш-таблицу, если в таблице уже находится максимальное число элементов 
        (для метода открытой адресации, для метода цепочек просто показать добавление в таблицу).
 */
