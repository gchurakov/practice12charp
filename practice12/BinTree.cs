using System;
using System.Collections.Generic;
using System.Linq;
using EngineLib;

namespace practice12
{
    public class BinTree
    {
        Engine eng;
        public BinTree left,//адрес левого поддерева 
                     right;//адрес правого поддерева

        public int Count = 0;
        public BinTree()
        {
            //случайный узел
            eng = new Engine(1);
            left = null;
            right = null;
            //Count++;
        }
        public BinTree(Engine _eng)
        {
            // узел
            eng = _eng;
            left = null;
            right = null;
            //Count++;
        }

        public override string ToString()
        {
            return eng + " ";
        }

        public static void Run(BinTree p)
        {
            //обход сверху вниз
            if(p!=null)
            {
                //<обработка p.eng>
                Run(p.left);//переход к левому поддереву
                Run(p.right);//переход к правому поддереву
            }
        }
        
        
        public static  void ShowTree(BinTree p, int l)
        { 
            /*рекурсивная функция для печати дерева по уровням с обходом слева направо*/
            if(p!=null)
            {
                ShowTree(p.left,l+3);//переход к левому поддереву
                //формирование отступа
                for (int i = 0; i < l; i++) Console.Write(" ");
                Console.WriteLine(p.eng.ToString());//печать узла
                ShowTree(p.right,l+3);//переход к правому поддереву
            }
        }
        
        static  BinTree first(Engine _eng) => new BinTree(_eng);
        
        public static BinTree Add(BinTree root, Engine _eng)
        {
            //добавление элемента _eng в дерево поиска
            BinTree p = root; //корень дерева
            BinTree r = null;
            bool ok = false;//флаг для проверки существования элемента _eng в дереве
            while (p!=null && !ok)
            {
                r = p;
                //элемент уже существует
                if (_eng == p.eng) 
                    ok = true;               
                else if (_eng < p.eng)
                    p = p.left; //пойти в левое поддерево
                else 
                    p = p.right; //пойти в правое поддерево
            }
            if (ok) 
                return p;//найдено, не добавляем
            //создаем узел
            BinTree NewPoint = new BinTree(_eng);//выделили память
            // если _eng<r.key, то добавляем его в левое поддерево
            // если _eng>r.key, то добавляем его в правое поддерево
            if (_eng < r.eng) 
                r.left = NewPoint;
            else 
                r.right = NewPoint;
            
            //root.Count++;
            return NewPoint;
        }

        /*
        { 
            int min = 0;
            int max = inputArray.Length - 1; 
            while (min <=max)  
            {  
                int mid = (min + max) / 2;  
                if (key == inputArray[mid])  
                {  
                    return ++mid;  
                }  
                else if (key < inputArray[mid])  
                {  
                    max = mid - 1;  
                }  
                else  
                {  
                    min = mid + 1;  
                }  
            }  
            return "Nil";  
        }  */


        public static BinTree IdealTree(int size, BinTree p)
        {
            //построение идеально сбалансированного дерева
            if(size == 0)
                return p;
            int nleft = size / 2;
            int nright = size - nleft - 1 ;
            
            BinTree root = new BinTree();
            root.left = IdealTree(nleft, root.left);
            root.right = IdealTree(nright, root.right);
            
            root.Count += size;
            return root;
        }

        public static BinTree Tree(int size, BinTree p)
        {
            if(size == 0)
                return p;
            BinTree root = new BinTree();
            root.left = Tree(size - 1, root.left);
            root.Count += size;
            return root;
        }

        public static int SearchEngs(BinTree p)
        {
            // поиск двигателей мощностью < 200
            int count = 0;
            if (p != null)
            {
                SearchEngs(p.left);
                if (p.eng.Power < 200)
                    count++;
                SearchEngs(p.right);
            }

            return count;
        }
        
        private static void GetEngs(BinTree p, ref Engine[]engs, ref int i)
        {
            bool isIn = false;
            if (p != null)
            {
                GetEngs(p.left, ref engs, ref i); //переход к левому поддереву
                for (int j = 0; j < i-1; j++)
                    if (engs[j] == p.eng)
                        isIn = true;
                if (!isIn)
                {
                    engs[i++] = p.eng;
                    isIn = false;
                }

                GetEngs(p.right, ref engs, ref i); //переход к правому поддереву
            }
        }
        
        public static Engine[] GetEngsArray(BinTree p)
        {
            Engine[] engs = new Engine[p.Count];
            //сбор двигателей в массив
            int i = 0;
            GetEngs(p, ref engs, ref i);
            
            //  сортировка двигателей по увеличению мощности
            Array.Sort(engs);
            //  перестановка двигателей для сборки дерева
            //      проверка наличия повторяющихся двигателей
            Engine[] engs1;
            if (i != p.Count)
            {
                engs1 = new Engine[i];
                for (int j = 0; j < i; j++)
                    engs1[j] = engs[j];
                engs = engs1;
            }
            //      псевдо бин поиск
            //      составляем элементы в том порядке в котором будем заполнять дерево
            engs1 = new Engine[i];
            int jj = 0;
            a(engs, ref engs1, ref jj, 0, engs.Length-1);

            return engs;
        }

        public static void a(Engine[] engs, ref Engine[] engs1, ref int j, int left, int right)
        {
            int mid = (right + left) / 2;
            if (j<engs.Length-1)
            {
                if (mid > right || mid < left)
                {
                    a(engs, ref engs1, ref j, mid+1, mid+(mid-left)+2);
                }
                
                //right = engs.Length; //правая и левая граница
                //право + лево
                engs1[j++] = engs[mid];
                a(engs, ref engs1, ref j, left, mid-1);
            }
        }

        public static BinTree ToSearchTree(int size, BinTree p, Engine[] engs, ref int i)
        {
            //построение идеально сбалансированного дерева
            if(size == 0)
                return p;
            int nleft = size / 2;
            int nright = size - nleft - 1;
            
            BinTree root = new BinTree(engs[i++]);
            root.left = ToSearchTree(nleft, root.left, engs, ref i);
            root.right = ToSearchTree(nright, root.right, engs, ref i);
            
            root.Count += size;
            return root;
        }
    }
}