using System.Collections;

namespace Example
{

    public class Dispatcher
    {
        OnEntireData recv;
        public OnEntireData send{get; set;}
        public Dispatcher(Handler handler)
        {
            this.recv = handler.handleRecv;
            handler.sendHandler = this.sendMsg;
        }

        public void dispatchMsg(string data)
        {
            recv(data);
        }

        public void sendMsg(string data)
        {
            send(data);
        }

    }
}
