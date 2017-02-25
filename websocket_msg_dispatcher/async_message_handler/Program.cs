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

            using (var nf = new Notifier())
            using (var ws = new WebSocket("ws://echo.websocket.org"))
            {
                // Set the WebSocket events.

                ws.OnOpen += (sender, e) => ws.Send("Hi, there!");

                ws.OnMessage += (sender, e) =>
                    nf.Notify(
                      new NotificationMessage
                      {
                          Header = "WebSocket Message",
                          Body = !e.IsPing ? e.Data : "Received a ping."
                      }
                    );

                ws.OnError += (sender, e) =>
                    nf.Notify(
                      new NotificationMessage
                      {
                          Header = "WebSocket Error",
                          Body = e.Message
                      }
                    );

                ws.OnClose += (sender, e) =>
                    nf.Notify(
                      new NotificationMessage
                      {
                          Header = String.Format("WebSocket Close ({0})", e.Code),
                          Body = e.Reason
                      }
                    );
                ws.Connect();

                // Connect to the server asynchronously.
                //ws.ConnectAsync ();

                Console.WriteLine("\nType 'exit' to exit.\n");
                while (true)
                {
                    Thread.Sleep(1000);
                    Console.Write("> ");
                    var msg = Console.ReadLine();
                    if (msg == "exit")
                        break;

                    // Send a text message.
                    ws.Send(msg);
                }
            }
        }
    }
}
