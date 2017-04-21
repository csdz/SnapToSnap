using System.Collections;

namespace Example
{

    public class Handler
    {
        string seperator = "#";
        OnSegmentsData recvHandler;
        public OnEntireData sendHandler{get; set;}

        public Handler(RPCManager rpcMgr)
        {
            recvHandler = rpcMgr.handle;
            rpcMgr.sendHandler = handleSend;
        }

        public void handleRecv(string message)
        {
            string[] rpcSeg = message.Split(seperator.ToCharArray());
            if (rpcSeg.Length < 1) // rpcSeg[0] must be func_name in RPCManager
                return;
            recvHandler(rpcSeg);
        }

        public void handleSend(string[] rpcSeg)
        {
            if (null == sendHandler)
                return;

            string entire = string.Join(seperator, rpcSeg);
            sendHandler(entire);
        }

    }
}
