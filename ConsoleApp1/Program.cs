using System;
using System.Diagnostics;
using System.Threading;
namespace ConsoleApp1
{
    class ThreadCreationProgra
    {
        public string ExecuteADBCMD(string command, int seconds)
        {
            string output = ""; //输出
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();//创建进程对象
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动
                startInfo.RedirectStandardInput = false;//不重定向输入
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.CreateNoWindow = true;//不创建窗口
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程
                    {
                        Console.WriteLine("长度： {0}", seconds);
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒
                        }
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出
                    }
                }
                catch
                {
                }
                finally
                {
                    //if (process != null)
                    process.Close();
                }
            }
            return output;
        }


        public void display(string deviceNo)
        {
           
            string url = "kwai://profile/1162429530";
            string command = "adb -s " + deviceNo + " shell am start -a android.intent.action.VIEW -d " + url;

            string b = "adb -s " + deviceNo + " shell am start -a android.intent.action.VIEW -d " + "kwai://profile/1162429531";
            string backcmd = "adb -s " + deviceNo + " shell input tap 76 166";//76 166   933 575
            string swipecmd = "adb -s " + deviceNo + " shell input swipe 300 1200 300 0";

            
            ExecuteADBCMD(command, 10000);
            Thread.Sleep(5000);
            ExecuteADBCMD(backcmd, 10000);
            Thread.Sleep(5000);

            //Console.WriteLine("In Main: Creating the Child {0}");


            for (int counter = 0; counter <= 5; counter++)
            {
                ExecuteADBCMD(swipecmd, 10000);

                Random rNumber = new Random();
                int RandKey = rNumber.Next(5, 20);
                int time_int = RandKey * 1000;
                Console.WriteLine("随机时间{0}", time_int);
                Thread.Sleep(time_int);
                Console.WriteLine(counter);
            }

        }



        public class MyThread
        {
          
            public string name { set; get; }

            public void ThreadMain()
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                ThreadCreationProgra th = new ThreadCreationProgra();
                Console.WriteLine("线程ID {0}", threadId, name);

                while(true)
                {
                    th.display(name);
                    int time1 = 8000;
                    Console.WriteLine("休息{0}毫秒",time1 );
                    Thread.Sleep(50000);

                }
              
               
            }
        }




        static void Main(string[] args)
        {
          
       
                string[] marks = new string[] { "c32ad225","6d265857" };


                for (int i = 0; i < 2; i++)
                {
                    MyThread myThread = new MyThread();

                    myThread.name = marks[i];

                    Thread thread = new Thread(myThread.ThreadMain);
                    thread.Start();
                }

                Console.ReadLine();
                //Console.ReadKey();
        
           






        }
    }
}
