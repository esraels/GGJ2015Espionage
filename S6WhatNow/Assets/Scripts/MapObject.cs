using UnityEngine;
using System.Collections;

public class MapObject : MonoBehaviour
{
	[SerializeField] private MapObjectType m_mapObjectType;

	private void Start ()
	{
	
	}

	private void Update ()
	{
	
	}

	private void OnMouseDown ()
	{
		Debug.Log("Door Clicked");
	}
}
