using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using JsonFx.Json;
using System.IO;

// Sanwich类
[System.Serializable]
public class Sandwich{
	public string name;
	public string bread;
	public float price;
	public List<string> ingredients = new List<string>();
}

public class Test : MonoBehaviour {

	void Start(){
		//准备反序列化json字符串
		string sandwich_json = "{\"name\":\"haqi\",	\"bread\":\"tudousi\",	\"price\":1.45,	\"ingredients\":[\"sala\",\"beef\",\"cheese\",\"whatever\"]}";
		// 反序列化，下个函数
		Sandwich sw = Deserialize (sandwich_json);
		// 修改属性
		sw.bread = "this_bread_is_changed";
		// 序列化并且保存，下下个函数
		SerializeAndSave (sw);
	}

	// 反序列化
	Sandwich Deserialize(string sw_json){

		// 使用JsonFx反序列功能
		Sandwich sw = JsonReader.Deserialize<Sandwich> (sw_json);
		Debug.Log ("name:" + sw.name);
		Debug.Log ("bread:" + sw.bread);
		Debug.Log ("price:" + sw.price.ToString());
		Debug.Log ("first ingredients:" + sw.ingredients[0]);

		return sw;
	}
	
	void SerializeAndSave(Sandwich sw) {

		// 使用JsonFx序列化功能
		string data = JsonWriter.Serialize(sw);
		//持久化
		var streamWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "serialize_sandwich.json"));
		streamWriter.Write(data);
		streamWriter.Close();
	}
	
}
