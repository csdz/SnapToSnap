using System; 
using System.IO;
using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 

//新建一个HttpRequest单例
class HttpRequest : Singleton<HttpRequest>
{

	// Get请求
    public WWW GET(string url)
    {
        WWW www = new WWW (url);
        StartCoroutine (WaitForRequest (www)); // 由于是继承自MonoBehaviour，所以可以调用协程函数StartCoroutine
		while (!www.isDone) {}
		return www;
    }

	// Post请求
    public WWW POST(string url, Dictionary<string,string> post)
    {
        WWWForm form = new WWWForm();
        foreach(KeyValuePair<String,String> post_arg in post)
        {
           form.AddField(post_arg.Key, post_arg.Value);//添加表单字段数据
        }
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
		while (!www.isDone) {}
        return www; 
    }

	//枚举器，yield关键字作用
    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        } else {
            Debug.Log("WWW Error: "+ www.error);
        }    
    }

	public Sprite LoadSpriteAssets(string fullPath)
	{
		string      finalPath;
		WWW         localFile;
		Texture2D     texture;
		Sprite sprite;
		
		finalPath = "file://" + fullPath;
		localFile = new WWW(finalPath);

		StartCoroutine (WaitForRequest (localFile));

		while (localFile.isDone) {}
		
		texture = localFile.texture;
		return LoadSprite (texture);
	}

	public Sprite LoadSprite(string resPathWithoutExtension)
	{
		Texture2D tex = Resources.Load<Texture2D>(resPathWithoutExtension);
		return LoadSprite (tex);
	}

	public Sprite LoadSprite(Texture2D tex)
	{
		Rect rec = new Rect(0, 0, tex.width, tex.height);
		return Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f));
	}
}