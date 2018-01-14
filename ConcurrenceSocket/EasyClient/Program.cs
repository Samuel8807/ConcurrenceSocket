using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyClient
{
    class Program
    {
        //static List<Task> Tasks = new List<Task>();

        static void Main(string[] args)
        {

            Thread.Sleep(5000);
            /*
             * 测试并发socket消息
             * 
             * */
            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(100);
                 Task.Factory.StartNew(() =>
                  {
                      SSClient c = new SSClient(new Random().Next(1, 999).ToString(), "127.0.0.1", 2012);
                      c.Connect();
                      while (true)
                      {
                          Thread.Sleep(100);
                          c.SendCommand(new Random().Next(1, 999).ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                      }
                  });

                //Tasks.Add(t);
            }


            while (Console.ReadKey().Key.ToString().ToUpper() != "Q")
            {

            }

            Console.ReadKey();

        }


    }
}
