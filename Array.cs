using System;

namespace Array
{
    class Array
    {
        static void Main(string[] args)
        {
            //以下三种方式等价
            int[,] b = new int[,]   { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            int[,] c = new int[3,2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            int[,] d =              { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            var    e = new [,]       { { 1, 2 }, { 3, 4 }, { 5, 6 } };  //隐式类型

            //******************以上是矩形数组的语法************************//


            //******************以下是交错数组的语法************************//

            //如何声明交错数组
            //int[][] f = new int[3][4];//编译错误，只能声明顶层数组的维数
            int[][] f = new int[3][];//正确
            int[][,] arr;
            arr = new int[3][,];//维度为3，数组元素是一个二维的矩形数组

            //以下对交错数组arr的三个元素进行赋值，这三个元素的类型是int[,]，即二维矩形数组
            //arr的三个元素都是指向另外三个数组的引用，在赋值完成后，一共存在四个数组
            arr[0] = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            arr[1] = new int[,] { { 7, 8 }, { 9, 10 }, { 11, 12 } };
            arr[2] = new int[,] { { 13, 14 }, { 15, 16 }, { 17, 18 } };

            //数组都从System.Array类继承而来，以下使用一些常用方法
            Console.WriteLine("Rank of arr = {0}", arr.Rank);
            Console.WriteLine("Rank of arr[0] = {0}", arr[0].Rank);
            Console.WriteLine("arr.GetLength(0) = {0}", arr.GetLength(0)); //获取arr第一维的长度
            //Console.WriteLine("arr.GetLength(1) = {0}", arr.GetLength(1));//运行错误，试图获取arr第二维的长度，但是arr只有一个维度

            int[,] tmpArr = (int[,])arr.GetValue(2);//取index为2的这一维
            int[,] tmpArr2 = arr[2];                //与上一行等价         

            
            System.Array.Reverse(arr);
            arr[2] = (int[,]) arr[1].Clone(); //浅复制

            //遍历数组arr
            for (int i = 0; i < arr.GetLength(0); ++i)
            {
                for (int j = 0; j < arr[i].GetLength(0); ++j)
                {
                    for (int k = 0; k < arr[i].GetLength(1); ++k)
                    {
                        Console.WriteLine("arr[{0}][{1},{2}] = {3}", i, j, k, arr[i][j, k]);
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("");
            }

            foreach(int[,] i in arr)
            {
                foreach(int tmp in i)
                {
                    //tmp = tmp;//tmp是数组i的只读别名，无法赋值
                    //如果数组里存的是class对象，那么可以通过这个引用改变对象里的值
                    Console.WriteLine(tmp);
                }
            }
            //******************以上是交错数组的语法************************//
            //在CIL中，一维数组有特定的指令用于性能优化，因此交错数组比起矩形数组效率更高
        }
    }
}