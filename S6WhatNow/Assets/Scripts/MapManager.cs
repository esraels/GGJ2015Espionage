using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{
	private static MapManager m_instance;
	public static MapManager Instance
	{
		get
		{
			if ( m_instance == null )
			{
				m_instance = GameObject.FindObjectOfType<MapManager>();
			}
			return m_instance;
		}
	}

	[SerializeField] private Map[] m_maps;

	private void Start ()
	{
	
	}

	private void DeactivateMapObject (int p_mapIndex, int p_mapObjectIndex)
	{

	}
}
