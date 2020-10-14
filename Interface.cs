using System;
using System.Collections.Generic;
using System.Text;

//以PrintOut为例，想给PrintOut函数的形参传入一个实现了接口Itest的对象obj，而不在乎obj的类型是testA还是testB。
//接口是一种规则，是一个 "包含了一系列有特定签名的函数" 的黑盒子。
//以下是接口需要满足的要求：
    //接口中的成员不允许使用 public、private、protected、internal 访问修饰符。
    //接口中的成员不允许使用 static、virtual、abstract、sealed 修饰符。
    //在接口中不能定义字段，但可以定义属性。
    //在接口中定义的方法不能包含方法体。
    //一个类要么实现该接口的所有方法，要么不实现该接口。

//关于显式方式实现接口成员
    //显式定义 = 在class内定义接口成员(包括属性、方法)时，在成员头部添加 "接口名." ， 比如 "Itest.func(){//方法体}"就是显式
    //不能在 "被显式定义的接口成员" 前面加修饰符
    //想访问 "被显式定义的接口成员" ，必须先进行类型转换，比如将testB类型转换为Itest类型
namespace CSharpNotes
{
    interface Itest
    {
        void func();
    }

    class test
    {
        static public void PrintOut(Itest obj)
        {
            testA tmpA = obj as testA;
            testB tmpB = obj as testB;
            if (tmpA != null)
            {
                Console.WriteLine("obj.a = {0}", tmpA.a);
            }else if (tmpB != null)
            {
                Console.WriteLine("obj.b = {0}", tmpB.b);
            }
            obj.func();
        }
    }
    class testA : Itest
    {
        public int a = 1;
        public void func() //加public，否则编译错误
        {
            Console.WriteLine("func() called by class testAAAAAAA!");
        }
        static void Main()
        {
            testA a = new testA();
            testB b = new testB();

            test.PrintOut(a);
            test.PrintOut(b);

            //如下面三行代码所示，想访问 "被显式定义的接口成员"，必须先转换为Itest类型，否则会编译错误
            //testB b = new testB();
            //Itest obj = b;
            //obj.func();
        }

    }

    class testB : Itest
    {
        public int b = 2;
        void Itest.func() //显式定义，不能加修饰符
        {
            Console.WriteLine("func() called by class testBBBBBBB!");
        }

    }
}
//参考了http://c.biancheng.net/view/2880.html