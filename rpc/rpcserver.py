# -*- coding: utf-8 -*-
# rpcserver.py

from multiprocessing.connection import Listener
from threading import Thread

def rpc_server(handler, address, authkey):

    sock = Listener(address, authkey=authkey)
    while True:
        client = sock.accept()
        t = Thread(target=handler.handle_connection, args = (client,))
        t.daemon = True
        t.start()


if __name__ == '__main__':

    # 写几个测试方法
    def add(x, y):
        return x+y

    def printdict(**kwargs):
        cnt = 0
        for k, v in kwargs.iteritems():
            print(''.join(['"', str(k), '":"', str(v), '"']))
            cnt += 1
        return cnt


    # 新建一个handler类实例, 并将add, printdict方法注册到handler里面
    from rpchandler import RPCHandler
    rpc_handler = RPCHandler()
    rpc_handler.register_function(add)
    rpc_handler.register_function(printdict)

    # 运行server
    rpc_server(rpc_handler, ('localhost', 17000), authkey='tab_space')
