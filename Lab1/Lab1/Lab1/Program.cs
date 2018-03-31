using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab1
{
    class Program
    {
        public static int BaseLev(string s1, string s2)
        {
            int LengthS1 = s1.Length;
            int LengthS2 = s2.Length;
            int delete, insert, replace;
            int[,] Matrix = new int[LengthS1+1, LengthS2+1];
            
            // Операция вставки
            for (int q = 0; q <= LengthS2; q++)
            {
                Matrix[0, q] = q;
            }

            // Операция удаления
            for (int k = 0; k <= LengthS1; k++)
            {
                Matrix[k, 0] = k;
            }
                       
            for (int i = 1; i <= LengthS1; i++)
            {           
                for (int j = 1; j <= LengthS2; j++)
                {
                    // Вычисление редакционного расстояния Левенштейна
                    delete = Matrix[i, j - 1] + 1;
                    insert = Matrix[i - 1, j] + 1;
                    replace = Matrix[i - 1, j - 1];
                    if (s1[i-1] != s2[j-1])
                    {
                        replace += 1;
                    }

                    Matrix[i, j] = Math.Min(delete, Math.Min(insert, replace));
                }
            }
                      
            return Matrix[LengthS1, LengthS2];
        }

        // Расстояние Дамерау — Левенштейна
        public static int ModfiedLev(string s1, string s2)
        {
            int LengthS1 = s1.Length;
            int LengthS2 = s2.Length;
            int delete, insert, replace, transp;
            int[,] Matrix = new int[LengthS1 + 1, LengthS2 + 1];

            // Операция удаления
            for (int k = 0; k <= LengthS1; k++)
            {
                Matrix[k, 0] = k;
            }

            // Операция вставки
            for (int q = 0; q <= LengthS2; q++)
            {
                Matrix[0, q] = q;
            }

            for (int i = 1; i <= LengthS1; i++)
            {
                for (int j = 1; j <= LengthS2; j++)
                {
                    // Вычисление редакционного расстояния Дамерау — Левенштейна
                    if ((i > 1) && (j > 1))
                    {
                        delete = Matrix[i, j - 1] + 1;
                        insert = Matrix[i - 1, j] + 1;
                        replace = Matrix[i - 1, j - 1];
                        if (s1[i - 1] != s2[j - 1])
                        {
                            replace += 1;
                        }
                        if (s1[i] == s2[j-1] && s1[i-1] == s2[j])
                        {
                            transp = Matrix[i - 2, j - 2] + 1;
                            Matrix[i, j] = Math.Min(transp,
                                           Math.Min(delete,
                                           Math.Min(insert, replace)));
                        }
                        else
                            Matrix[i, j] = Math.Min(delete, 
                                           Math.Min(insert, replace));
                    }
                    else
                    {
                        delete = Matrix[i, j - 1] + 1;
                        insert = Matrix[i - 1, j] + 1;
                        replace = Matrix[i - 1, j - 1];
                        if (s1[i - 1] != s2[j - 1])
                        {
                            replace += 1;
                        }
                        Matrix[i, j] = Math.Min(delete, Math.Min(insert, replace));                   
                    }                    
                }
            }
            return Matrix[LengthS1, LengthS2];
        }
        
        // Рекурсивный алгоритм Левенштейна
        public static int DistRecLev(string s1, int i, string s2, int j)
        {            
            if (i == 0)
            {
                return j;
            }

            if (j == 0)
            {
                return i;
            }

            int tmp = Math.Min(DistRecLev(s1, i - 1, s2, j), 
                               DistRecLev(s1, i, s2, j - 1)) + 1;
            int t = 0;
           
            if (s1[i-1]!=s2[j-1])
            {
                t = 1;
            }

            tmp = Math.Min(DistRecLev(s1, i-1, s2, j-1) + t, tmp);
            return tmp;
        }

        public static int FindDistRecLev(string s1, string s2)
        {            
            return DistRecLev(s1, s1.Length, s2, s2.Length);
        }

        static private void Test()
        {
            string s1 = Console.ReadLine();
            string s2 = Console.ReadLine();
            GetMeans(4, s1, s2);
            
            //Console.ReadLine();
        }

        static private void GetMeans(int i, string s1, string s2)
        {
            int dist; 

            if (i == 1 || i == 4)
            {
                dist = BaseLev(s1, s2);
                Console.Write("Расстояние Левенштейна по базовуму алгоритму: ");
                Console.WriteLine(dist);
            }

            if (i == 2 || i == 4)
            {
                dist = ModfiedLev(s1, s2);
                Console.Write("Расстояние Левенштейна по модифицированному алгоритму: ");
                Console.WriteLine(dist);
            }

            if (i == 3 || i == 4)
            {
                dist = FindDistRecLev(s1, s2);
                Console.Write("Расстояние Левенштейна по рекурсивному алгоритму: ");
                Console.WriteLine(dist);
            }
        }

        static private void TimeForBaseLev(string[] mas, string word)
        {
            for (int i = 0; i < mas.Length; i++)
            {
                
            }
        }

        static private void Time1()
        {
            string s1, s2;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            s1 = "qwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgqwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgffdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgf";
            s2 = "qwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvqwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfgfvhbjngfwertyuwertyedrftgyhujhgfcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgf";
            GetMeans(1, s1, s2);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);

            stopWatch = new Stopwatch();
            stopWatch.Start();


            s1 = "qwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgf";
            s2 = "qwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvgfvhbjngfwertyuwertyedrftgyhujhgfcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgf";
            GetMeans(1, s1, s2);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);
            /*string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            */
            stopWatch = new Stopwatch();
            stopWatch.Start();

            s1 = "axcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfghjkxcvbnhjhytfvbnjgaxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfghjkxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcvtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcv";
            s2 = "zxcaxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfghjkxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcvvbnhjhytfvbnjgtfdcvsdfghjkjhgfdftresdfghjkxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcv";
            GetMeans(1, s1, s2);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);

            stopWatch = new Stopwatch();
            stopWatch.Start();

            s1 = "axcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfghjkxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcv";
            s2 = "zxcvbnhjhytfvbnjgtfdcvsdfghjkjhgfdftresdfghjkxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcv";
            GetMeans(1, s1, s2);
            
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);
            
            stopWatch = new Stopwatch();
            stopWatch.Start(); 

            s1 = "asdfghccfyghvgfcgtgvfyhbgjnbhvbghbncfygh";
            s2 = "asdfghccgdgbgjnbhvbhvgfcgtgvfyhghtrcfygh";
            GetMeans(1, s1, s2);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);
           
            stopWatch = new Stopwatch();
            stopWatch.Start();
            s1 = "gbgjnbhvbhvgfcgtgvfy";
            s2 = "gbgjndfghgfghhvgfcgt";
            GetMeans(1, s1, s2);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);
              
  
        }

        static private void Time2()
        {
            string s1, s2;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            s1 = "qwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgqwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgffdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgf";
            s2 = "qwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvqwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfgfvhbjngfwertyuwertyedrftgyhujhgfcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgf";
            GetMeans(2, s1, s2);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);
            stopWatch = new Stopwatch();
            stopWatch.Start();

            s1 = "qwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgf";
            s2 = "qwertyuwertyedrftgyhujqwertyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfrtyuwertyedrftgyhujhgfdsdfvgghtfghbvgfdxserghbnvgfvhbjngfwertyuwertyedrftgyhujhgfcfghbfdrtyghujbgfdxesghjbgfdxghjkhgfdrtyuhgfdcghbjnvgfvhbjngfhgfdsdfvgghtfghbvgfdxserghbnvcfghbfdrtyghujbgf";
            GetMeans(2, s1, s2);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);
            /*string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            */
            
            stopWatch = new Stopwatch();
            stopWatch.Start();

            s1 = "axcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfghjkxcvbnhjhytfvbnjgaxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfghjkxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcvtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcv";
            s2 = "zxcaxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfghjkxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcvvbnhjhytfvbnjgtfdcvsdfghjkjhgfdftresdfghjkxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcv";
            GetMeans(2, s1, s2);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);
            stopWatch = new Stopwatch();
            stopWatch.Start();

            s1 = "axcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfghjkxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcv";
            s2 = "zxcvbnhjhytfvbnjgtfdcvsdfghjkjhgfdftresdfghjkxcvbnhjhytfvbnjgtfdcvbgfdxcvgbhjuytresdfjhytfvbnjgtfdcv";
            GetMeans(2, s1, s2);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);
            stopWatch = new Stopwatch();
            stopWatch.Start();

            s1 = "asdfghccfyghvgfcgtgvfyhbgjnbhvbghbncfygh";
            s2 = "asdfghccgdgbgjnbhvbhvgfcgtgvfyhghtrcfygh";
            GetMeans(2, s1, s2);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);

            stopWatch = new Stopwatch();
            stopWatch.Start();
            s1 = "gbgjnbhvbhvgfcgtgvfy";
            s2 = "gbgjndfghgfghhvgfcgt";
            GetMeans(2, s1, s2);

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(ts.Ticks);
         

        }


        static void Main(string[] args)
        {
            //Time2();
            //Time1();

            Test();

            Console.ReadLine();
            
        }


        public static int replace { get; set; }
    }
}
