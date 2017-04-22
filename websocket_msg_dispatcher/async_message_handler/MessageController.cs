using System.Collections;

namespace Example{

    public delegate void OnSegmentsData(string[] msgs);
    public delegate void OnEntireData(string msg);
    public class MessageController {


        RPCManager rpcMapper;
	    Handler handler;
        Dispatcher dispatcher;
        ClientSocket cs;

        public MessageController()
	    {     
            rpcMapper = new RPCManager();
            handler = new Handler(rpcMapper);
            dispatcher = new Dispatcher(handler);
	    }

        public void RegisterHandler(string funName, Function fun)
	    {
            rpcMapper.RegisterHandler(funName, fun);	
	    }

        public void CallServerMethod(string[] rpcSeg)
        {
            rpcMapper.CallServerMethod(rpcSeg);
        }

        public void Connect()
        {
            cs = new ClientSocket(dispatcher);
        }

    }
}
