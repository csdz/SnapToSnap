# -*- coding: utf-8 -*-
# rpchandler.py

import pickle

class RPCHandler(object):
    def __init__(self):
        # rpc functions map
        self._functions = {}

    def register_function(self, func):
        self._functions[func.__name__] = func

    def handle_connection(self, connection):
        try:
            while True:
                # 接收到一条消息, 使用pickle协议编码
                func_name, args, kwargs = pickle.loads(connection.recv())
                # rpc调用函数，并返回结果
                try:
                    r = self._functions[func_name](*args, **kwargs)
                    connection.send(pickle.dumps(r))
                except Exception as e:
                    connection.send(pickle.dumps(e))
        except EOFError:
            pass
