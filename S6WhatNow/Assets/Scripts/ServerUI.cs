using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerUI : MonoBehaviour
{
	private static ServerUI m_instance;
	public static ServerUI Instance
	{
		get
		{
			if ( m_instance == null )
			{
				m_instance = GameObject.FindObjectOfType<ServerUI>();
			}
			return m_instance;
		}
	}

	[SerializeField] private Text m_levelTimer;
	[SerializeField] private Text m_deactivationTimer;
	[SerializeField] private GameObject m_passcodePanel;

	private bool m_isActivating;
	private float m_deactivationTicker;


	private void Start ()
	{
		HideDeactivationTimer();
		HidePasscodePanel();
	}

	private void Update ()
	{
		if (m_isActivating)
		{
			if (m_deactivationTicker > 0)
			{
				m_deactivationTicker -= Time.deltaTime;
				m_deactivationTimer.text = "" + (int)m_deactivationTicker;
				if (m_deactivationTicker <= 0)
				{
					Map.Instance.ActivateMapObject();
					HideDeactivationTimer();
				}
			}
		}
	}

	public void ShowDeactivationTimer ()
	{
		m_isActivating = true;
		m_deactivationTicker = 5;
		m_deactivationTimer.enabled = true;
	}

	public void HideDeactivationTimer ()
	{
		m_isActivating = false;
		m_deactivationTimer.enabled = false;
	}

	public void ShowPasscodeField ()
	{
		m_passcodePanel.SetActive(true);
		InputField inputField = m_passcodePanel.GetComponentInChildren<InputField>();
		inputField.ActivateInputField();
	}

	private void HidePasscodePanel ()
	{
		InputField inputField = m_passcodePanel.GetComponentInChildren<InputField>();
		inputField.text = "";
		m_passcodePanel.SetActive(false);
	}

	public void SubmitPasscode ()
	{
		InputField inputField = m_passcodePanel.GetComponentInChildren<InputField>();
		Debug.Log(inputField.text);
		if (inputField.text == "123")
		{
			Map.Instance.DeactivateMapObject();
			ShowDeactivationTimer();
		}
		else
		{
			Map.Instance.ActivateMapObject();
		}
		HidePasscodePanel();
	}
}
