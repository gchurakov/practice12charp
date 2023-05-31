using System;
using EngineLib;

namespace practice12
{
    public class DLinkList
    {
        //определение двунаправленного списка
        
        public Engine eng;
        public DLinkList next, //адрес следующего элемента
            prev;//адрес предыдущего элемента
        public DLinkList()
        {
            // случайный движок
            eng = new Engine(1);
            next = null;
            prev = null;
        }
        public DLinkList(Engine _eng)
        {
            // инициализация элемента
            eng = _eng;
            next = null;
            prev = null;
        }
        public override string ToString()
        {
            // демонстрация элемента
            return "-> " + eng.ToString() + " ->\n";
        }

        public static void Show(DLinkList p)
        {
            //печать списка
            if (p == null)
                Console.WriteLine("Пустой список!");
            
            while (p != null)
            {
                Console.Write(p.ToString());
                p = p.next;
            }
        }

        public static DLinkList GenerateDList(int size) 
        {
            //формирование двунаправленного списка
            //добавление в начало
            DLinkList begin = new DLinkList();
            for (int i = 1; i < size; i++)
            {
                DLinkList temp = new DLinkList();
                temp.next = begin;
                begin.prev = temp;
                begin = temp;
            }
            return begin;
        }
        
        
        public static DLinkList DelElems(DLinkList begin)
        {
            if (begin == null)//пустой список
            {
                Console.WriteLine("\nУдаление не возможно! Пустой список!");
                return null;
            }
            else if (begin.next == null)//последний элемент
            {
                Console.WriteLine("\nУдаление не возможно! Всего 1 элемент!");
                return null;
            }
            DLinkList temp = begin;
            int count = 1;//подсчет количества элементов
            //ищем элемент для удаления и встаем на предыдущий
            while (temp.next.next != null)
            {
                if (count % 2 == 1)
                {
                    //исключаем элемент из списка
                    temp.next = temp.next.next;
                    temp.next.prev = temp;
                }
                else
                {
                    temp = temp.next;   
                }

                count++;
            }
            if (count % 2 == 1)//удаление последнего элемента на четной позиции если он есть
                temp.next = null;
            Console.WriteLine("Элементы удалены.");
            return begin;
        }

        public static void DelDLinkList(DLinkList begin)
        {
            DLinkList next = begin.next;
            DLinkList temp;
            while (next != null)
            {
                temp = next;
                next = next.next;//переход к след элементу
                temp = null;//зачистка
            }
            
            begin = null;
            Console.WriteLine("Список очищен.");
        }
    }
}