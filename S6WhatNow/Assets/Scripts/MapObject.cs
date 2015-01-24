using UnityEngine;
using System.Collections;

public class MapObject : MonoBehaviour
{
	[SerializeField] private MapObjectType m_mapObjectType;
	[SerializeField] private int m_deactivationDuration = 5;

	private bool m_isAccessed;
	private bool m_isActivating;
	private float m_deactivationTicker;

	private SpriteRenderer m_sprite;


	private void Awake ()
	{
		m_sprite = GetComponentInChildren<SpriteRenderer>();
	}

	private void Start ()
	{
		m_isActivating = false;
		m_deactivationTicker = m_deactivationDuration;	
	}

	private void Update ()
	{
		if (m_isActivating)
		{
			if (m_deactivationTicker > 0)
			{
				m_deactivationTicker -= Time.deltaTime;
				if (m_deactivationTicker <= 0)
				{
					Activate();
				}
			}
		}
	}

	private void OnMouseDown ()
	{
		Access();
	}

	private void Access ()
	{
		m_isAccessed = true;
		m_sprite.color = new Color(1, 0.72f, 0);
		Map.Instance.ActiveMapObject = this;
		ServerUI.Instance.ShowPasscodeField();
	}

	public void Deactivate ()
	{
		m_sprite.color = new Color(0, 1, 0);
		m_deactivationTicker = m_deactivationDuration;
		m_isActivating = true;
	}

	public void Activate ()
	{
		m_sprite.color = new Color(1, 0, 0);
		m_isActivating = false;
	}
}
