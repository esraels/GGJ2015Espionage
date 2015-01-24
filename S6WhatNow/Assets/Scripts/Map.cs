using UnityEngine;
using System.Collections;

public enum MapObjectType
{
	DOOR,
	SENSOR,
}

public class Map : MonoBehaviour
{
	[SerializeField] private MapObject[] m_mapObjects;

	private void Start ()
	{
	
	}

	private void Update ()
	{
	
	}
}
