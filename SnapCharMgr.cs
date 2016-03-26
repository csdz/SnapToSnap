using UnityEngine;
using System.Collections;

public class SnapCharMgr : MonoBehaviour {

	// 公有变量, 接收Unity内GameObject拖入
	public RectTransform choosePanel; //将ChoosePanel拖入
	public RectTransform[] elements; //将Content中的若干个元素拖入 
	public RectTransform center; //在ChoosePanel内新建一个EmptyObject，以centerToCompare命名，并将此拖入

	// 私有变量
	private int distanceBetweenEles; //相邻两个元素的距离，在Start方法计算
	private float[] distanceToCenter; //每个元素距离center的距离，在Update方法计算
	private int minEleNum; //在所有元素中，距离center最近的元素索引
	private bool dragging = false; //Element是否在被拖拽；


	// Use this for initialization
	void Start () {

		int eleLength = elements.Length;
		distanceToCenter = new float[eleLength];
		
		//Get distance between elements
		distanceBetweenEles = (int)Mathf.Abs (elements [1].anchoredPosition.x - elements [0].anchoredPosition.x);
	
	}
	
	// Update is called once per frame
	void Update () {

		for(int i = 0; i < elements.Length; i++)
		{
			distanceToCenter[i] = Mathf.Abs(center.transform.position.x - elements[i].transform.position.x);//计算每个元素距离center的距离
		}	
		float minDist = Mathf.Min (distanceToCenter);
		
		for (int i = 0; i < elements.Length; i++) {
			if(minDist == distanceToCenter[i])
			{
				minEleNum = i; //找到最小距离的元素索引
			}
		}
		
		if (!dragging) { //如果目前没有滑动
			LerpEleToCenter(minEleNum * -distanceBetweenEles); //LerpEleToCenter作用是自然地滑到目标距离
		}
	
	}

	void LerpEleToCenter(int position)
	{
		float newX = Mathf.Lerp (choosePanel.anchoredPosition.x, position, Time.deltaTime *20f); //使用Mathf.Lerp函数让数据的顺滑地变化
		Vector2 newPosition = new Vector2 (newX, choosePanel.anchoredPosition.y);//目标距离
		
		choosePanel.anchoredPosition = newPosition;
	}

	public void StartDrag()
	{
		dragging = true;
	}
	
	public void EndDrap()
	{
		dragging = false;
	}
}
