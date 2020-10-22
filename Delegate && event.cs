using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

class Base
{
    static public string func(Base b)
    {
        Console.WriteLine("Base.func");
        return "Hello World!";
    }
}

class Derived : Base
{
    static public int func2(Base b)
    {
        return -1;
    }
    static public string func3(Base b)
    {
        Console.WriteLine("Derived.func3!");
        return "!!";
    }
}

class Run
{
    delegate object myDelegate(Derived i);
    static void Main()
    {
        myDelegate del;

        //委托的逆变性——方法的参数可以是委托类型参数的基类
        //委托的协变性——方法的返回值可以是委托类型返回值的派生类
        del = Base.func; 

        Base b = new Base();
        Derived d = new Derived();

        //del(b); //编译错误:无法从Base转换为Derived
        string tmp = del(d).ToString();
        Console.WriteLine("tmp = {0}", tmp);
        
        Console.WriteLine(":  {0}", del.Method);
        //协变性和逆变性只支持引用类型，不支持值类型
        //虽然int和string都派生自object类，但返回int的方法不能添加到委托myDelegate上
        //del += Derived.func2;//编译错误:返回类型错误

        //先讲讲Delegate类里有的三个重要非公有字段
        //_target,引用的是回调方法要操作的对象，静态方法是null
        //_methodPtr,引用的是一个整数值，表示要回调的方法
        //_invocationList的值通常为null，当存在委托链的时它引用一个委托示例数组


        //Delegate类中有一些用于委托的增减的方法
        myDelegate delOther = new myDelegate(Derived.func3);

        //下面一行代码将创建一个委托示例数组，数组内存放着del和delOther内部的方法，返回该数组的引用
        //上一行注释提到的委托示例数组的引用，存放在MulticastDelegate类的重要非公有字段_invocationList里
        del = (myDelegate)Delegate.Combine(del, delOther);
        //如果这里再次使用 Delegate.Combine 来链接新的委托示例，会创建一个新的委托实例数组，原先的数组将被垃圾回收

        del = (myDelegate)Delegate.Remove(del, delOther);//委托实例数组从后往前，移除第一个匹配的委托示例

        del = (myDelegate)Delegate.Remove(delOther, del); //delOther的委托链里不存在del，试图把委托实例del Remove了，也并不会报错

        //以上的Combine和Remove可以用+=和-=代替，同时不再需要显式将这两个方法的返回值转换为myDelegate

        //Action<T>
        //匿名方法:delegate(参数列表){....}
        //Lambda表达式:(参数列表)=>{....}
    }
}
