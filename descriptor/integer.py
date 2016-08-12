# -*- coding: utf-8 -*- 

# 使用描述符来对以Integer类型为属性的限制
class Integer(object):

	def __init__(self, name):
		self.name = name

	def __get__(self, instance, cls):
		if instance is None:
			return self
		else:
			return instance.__dict__[self.name]

	def __set__(self, instance, value):
		if not isinstance(value, int):
			raise TypeError("Expected an int")
		instance.__dict__[self.name] = value

	def __delete__(self, instance):
		del instance.__dict__[self.name]

# 属性x，y以Integer类为类型的使用
class Point(object):
 	x = Integer('x')
 	y = Integer('y')

 	def __init__(self, x, y):
 		self.x = x
 		self.y = y

if __name__ == '__main__':
 	p = Point(2,3) # 调用Point.__init__，进而调用 Point.x.__set__(),Point.y.__set__()
 	print p.x # 调用Point.x.__get__()

 	p.y = 5 # 调用Point.y.__set__()
 	p.x = 2.3



