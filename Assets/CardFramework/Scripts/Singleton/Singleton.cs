using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	static public T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (T)GameObject.FindObjectOfType(typeof(T));
				if (_instance == null)
				{
					GameObject gameObject = new GameObject(typeof(T).ToString());
					_instance = (T)gameObject.AddComponent(typeof(T));
				}
			}
			return _instance;
		}
	}
	static private T _instance;
}