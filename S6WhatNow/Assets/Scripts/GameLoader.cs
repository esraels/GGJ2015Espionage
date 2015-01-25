using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour
{
	GameObject m_curStage = null;

	private void Start ()
	{
		LoadStage(0);
	}

	public void LoadStage (int p_stageNum)
	{
		if (m_curStage)
		{
			Debug.Log("Destroying previous stage");
			Destroy(m_curStage);
			m_curStage = null;
		}

		Debug.Log("Loading a stage...");
		m_curStage = Instantiate(Resources.Load("Stage"+p_stageNum)) as GameObject;
		if (m_curStage)
		{
			Debug.Log("Finished loading...");
		}
		else
		{
			Debug.Log("Failed to load Stage"+p_stageNum+"...");
		}

	}

	public void DestroyTitleScreen(){

		if(m_curStage){
			Debug.Log("Destroying previous stage");
			Destroy(m_curStage);
			m_curStage = null;
		}
	}


}
