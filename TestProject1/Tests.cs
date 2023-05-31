using System;
using EngineLib;
using practice12;
using Xunit;


namespace TestProject1
{
    public class Tests
    { 
        Point<int> p1 = new Point<int>();
        Point<int> p2 = new Point<int>(1, new Point<int>());

        MyCollection<int> c1 = new MyCollection<int>(6);

        MyCollection<int> c2 = new MyCollection<int>(10);
        MyCollection<int> c3 = new MyCollection<int>(new MyCollection<int>());
        
        [Fact]
        public void Test1()
        {
            Assert.True(c1.Count==0);
        }
        
        [Fact]
        public void Test2()
        {
            Assert.True(c1.IsReadOnly==true);
        }
        
        [Fact]
        public void Test3()
        {
            c1.Add(1);
            c1.Add(2);
            Assert.True(c1[0]==2);
        }
        
        [Fact]
        public void Test4()
        {
            c1.Add(1);
            c1.Add(2);
            Assert.True(c1[1]==1);
        }
        
        [Fact]
        public void Test5()
        {
            c1.Add(1);
            c1.Add(2);
            c1.Del();
            Assert.True(c1[0]==1);
        }
        
        [Fact]
        public void Test6()
        {
            c1.Del();
            Assert.True(c1.Count==0);
        }
        
        
    }
}