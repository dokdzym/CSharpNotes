using System;
using System.Collections.Generic;
using System.Text;

namespace Delegate
{
    delegate void MyDel();
    delegate int MyDelWithReturn(int x);
    class Delegate
    {
        static void func1()
        {
            Console.WriteLine("func1 called");
        }
        void func2()
        {
            Console.WriteLine("func2 called");
        }
        static void Main(string[] args)
        {
            MyDel delInstance;
            //delInstance += Delegate.func1;               //编译错误，必须先给委托对象赋值
            delInstance = Delegate.func1;                  //赋值一个static成员函数
            delInstance = new MyDel(Delegate.func1);       //与上一句等价

            Delegate obj = new Delegate();
            delInstance -= Delegate.func1;                 //删除委托对象里的func1函数
            Console.WriteLine("******************************");
            //delInstance();                               //抛出异常，因为试图调用空委托对象
            if(null == delInstance)
            {
                Console.WriteLine("委托对象的调用列表为空");//一般都会先做null检查，再去调用委托对象
            }


            delInstance -= obj.func2;                      //试图删除委托对象里不存在的函数，并不会报错

            delInstance += obj.func2;                      //添加一个对象成员函数    

            Console.WriteLine("******************************");
            delInstance();

            //委托对象可以返回值
            //委托对象的ref参数将传递给调用列表的下一个函数


            /********************************************/
            //使用匿名方法初始化一个带返回值的委托对象
            //格式为delegate(参数列表){函数体};
            MyDelWithReturn delInstance2;
            {
                int y = 10;
                delInstance2 = delegate (int x)
                {
                    //return x + 10;         
                    return x + y;          //与上一行等价，因为匿名方法可以捕获外部变量
                };                         //记得加分号，这是一个语句！

                //delInstance2 += (int x) => { return x + y + 10; };  //正常运行，lambda表达式，=>读作"goes to"
            }
            //Console.WriteLine("y = {0}", y);     //编译错误，局部变量y已过期
            Console.WriteLine("x + y = {0}", delInstance2(3)); //y已被委托对象捕获，程序正常运行

            //delegate后的圆括号可以省略，但必须满足：不使用任何参数
            //可以省略参数类型
            //如果只有一个参数，可以省略圆括号()
            //如果函数体只有一行返回语句，可以省略花括号{}
        }
    }
}
