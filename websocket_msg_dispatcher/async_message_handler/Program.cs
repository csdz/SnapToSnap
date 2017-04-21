using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program p = new Program();

            MessageController msgCtrl = new MessageController();
            msgCtrl.RegisterHandlers("RecvCBTest", p.RecvCBTest);

            Console.WriteLine("\nType 'exit' to exit.\n");

            msgCtrl.CallServerMethod(new string[] { "RecvCBTest", "I'm a student", "Why not Curry" });

            Thread.Sleep(100 * 1000);
        }

        public void RecvCBTest(string[] msg)
        {
            for (int i = 0; i < msg.Length; i++)
            {
                Console.WriteLine("{0} param: {1}\n", i, msg[i]);
            }
        }
    }
}
