using UnityEngine; // Unity相关API
using System.Collections;
using System;

// 泛型T，继承自MonoBehaviour, 保证了此单例是组件的基类
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance;
	
	static bool instantiated = false; 
 
	/**
	  Returns the instance of this singleton.
	*/
	public static T Instance //使用c#的get，set Property的写法
	{
		get
      	{

         	if(instance == null){ //第一次调用Singleton<T>.Instance时instance==null
				if(instantiated)
					return null;
            	instance = (T) FindObjectOfType(typeof(T)); // 寻找object，找到后转为Component
 
            	if (instance == null){ //如果没有找到
					GameObject go = new GameObject();//新建GameObject
					go.name = typeof(T).ToString();
					instance = go.AddComponent<T>();//把T挂载到go上，
					instantiated = true; //初始化成功
				}
			}

			//Destroy instance if the singleton persists into the editor
			if (Application.isEditor && !Application.isPlaying){ //持久化之前消除该GameObject
				Destroy(instance);
				instance = null;
			}
 
			return instance;
		}
	}	
	
	protected void RemoveInstance(){
		Destroy(instance);
		instance = null;
	}
	
	public static bool exists{
		set{}
		get{return instance != null;}
	}
}