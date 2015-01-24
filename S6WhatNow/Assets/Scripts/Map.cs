using UnityEngine;
using System.Collections;

public enum MapObjectType
{
	DOOR,
	SENSOR,
}

public class Map : MonoBehaviour
{
	private static Map m_instance;
	public static Map Instance
	{
		get
		{
			if ( m_instance == null )
			{
				m_instance = GameObject.FindObjectOfType<Map>();
			}
			return m_instance;
		}
	}

	[SerializeField] private GameObject[] m_floors;

	private MapObject m_activeMapObject;
	public MapObject ActiveMapObject
	{
		get { return m_activeMapObject; }
		set { m_activeMapObject = value; }
	}

	private void Start ()
	{
		m_floors[1].SetActive(false);
	}

	private void Update ()
	{
	
	}

	public void DeactivateMapObject ()
	{
		m_activeMapObject.Deactivate();
	}

	public void ActivateMapObject ()
	{
		m_activeMapObject.Activate();
	}
}
