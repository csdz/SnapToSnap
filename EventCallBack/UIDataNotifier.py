# -*- coding: utf-8 -*-
import sys

def data_listener(*events):
	def wrapped_f(f):
		f.events = events
		return f
	return wrapped_f

class UIDataEvent(object):
	_events = []
	def __init__(self, name):
		self._name = name
		self._callbacks = []
		UIDataEvent._events.append(self)

	def __iadd__(self, cb):
		self._callbacks.append(cb)
		return self

	def __call__(self, *args, **kwargs):
		for cb in self._callbacks:
			try:
				cb(*args, **kwargs)
			except:
				ex = sys.exc_info()
				print "UIDataNotifier cb error, function:", cb.__name__, ex

	def __repr__(self):
		return 'UIDataEvent %s' % self._name

	@classmethod
	def clear(cls):
		"""清空所有事件上的所有监听器，在销毁一个界面的时候调用"""
		for event in cls._events:
			event._cb = []

OnItemAdded = UIDataEvent('OnItemAdded')