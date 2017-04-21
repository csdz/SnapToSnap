using System.Collections;
using System.Collections.Generic;
using System;

namespace Example{

    public delegate void Function( string[] msgs);
    public class RPCManager {
        Dictionary<string, Function> dic = null;
        public OnSegmentsData sendHandler{get; set;}

	    public RPCManager()
	    {
            dic = new Dictionary<string, Function>();
	    }

	    public void RegisterHandler(string funName, Function fun)
	    {
		    dic.Add (funName, fun);
	    }

	    public void handle(string[] msgs)
	    {
		    string funName = msgs[0];
            if (dic.ContainsKey(funName))
            {
                Function fun = dic[funName];
                fun(msgs);
            }
            else {
                Console.WriteLine("Not found handle function");
            }
	    }

        public void CallServerMethod(string[] rpcSeg)
        {
            if (null == sendHandler)
                return;

            sendHandler(rpcSeg);
        }

    }
}
