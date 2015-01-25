using UnityEngine;
using System.Collections;

public class PlayerInField : MonoBehaviour {

	enum PlayerAniMove{
		IDLE,
		WALK_UP,
		WALK_DOWN,
		WALK_RIGHT,
		WALK_LEFT
	};

	public enum PlayerID{
		ONE=1, TWO
	}

	public PlayerID m_playerNum = PlayerID.ONE; //

	public float m_speed = 250;
	//public float m_speedLimit = 5;
	public Vector2 m_PasscodePos = new Vector2(0,0);

	string m_curPasscode = "";

	Animator m_animator;
	Transform m_aninode;
	Transform m_callout;
	Transform m_camera = null;
	Transform m_audiosource = null;

	SwitchObject m_curActiveSwitch = null;
	SwitchObject m_nearSwitch = null;

	// Use this for initialization
	void Start () {
	
		if(m_camera == null){
			m_camera = GameObject.Find("Main Camera").transform;
			m_camera.GetComponent<Camera>().orthographicSize = 5;
		}
		if(m_animator == null){
			m_aninode = transform.FindChild("animations");
			m_animator = m_aninode.gameObject.GetComponent<Animator>();
		}

		//m_audiosource = transform.FindChild ("audiosource");


		m_callout = transform.FindChild("callout");
		m_callout.gameObject.SetActive(false); //hide initially

		//m_texCallout = m_callout.GetComponent<SpriteRenderer>().guiTexture.texture as Texture2D;

	}
	
	// Update is called once per frame
	void Update () {
	
		rigidbody2D.velocity = Vector2.zero;

		if (GameFieldManager.activePlayer != m_playerNum) {
			return;
		}

		//---quick fix:
		if(Input.GetKeyUp("1")){
			Debug.Log("  -- 1 is entered");
			GameFieldManager.FocusToPlayer(PlayerInField.PlayerID.ONE);
		}
		else if(Input.GetKeyUp("2")){
			Debug.Log("  -- 2 is entered");
			GameFieldManager.FocusToPlayer(PlayerInField.PlayerID.TWO);
		}


		if(Input.GetKeyUp("k")){
			if(m_nearSwitch){
				//activate previous obstacles.
				if(m_curActiveSwitch) m_curActiveSwitch.ActivateObstacles();

				//deactivate obstacles for current switch
				m_nearSwitch.DeactivateObstacles();
				m_curActiveSwitch = m_nearSwitch;

			}
		}

		//=================================
		// move the player by user input
		// note: can collide in walls
		//---------------------------------
		Vector3 move;
		move.x = Input.GetAxis("Horizontal");
		move.y = Input.GetAxis("Vertical");
		move.z = 0;

//		if (move.magnitude > m_speedLimit) {
//			move = move.normalized * (m_speed * Time.deltaTime);
//		}

		rigidbody2D.velocity = move * (m_speed * Time.deltaTime);

		//=================================
		// make the camera follow the player
		//---------------------------------
		//FocusToCamera();
		Vector3 newPosCam = transform.position;
		newPosCam.z = m_camera.position.z;
		
		m_camera.position = newPosCam;

		//=================================
		// set animation based on movement
		//---------------------------------
		PlayerAniMove aniValue = PlayerAniMove.IDLE; //default to idle animation
		float facingLeft = 1;
		if(Mathf.Abs(move.x) > Mathf.Abs(move.y)){
			if(move.x > 0){
				aniValue = PlayerAniMove.WALK_RIGHT;
				facingLeft = -1;
			}
			else if(move.x < 0)
				aniValue = PlayerAniMove.WALK_LEFT;
		}
		else {
			if(move.y > 0)
				aniValue = PlayerAniMove.WALK_UP;
			else if(move.y < 0)
				aniValue = PlayerAniMove.WALK_DOWN;
		}

//		if(aniValue == PlayerAniMove.WALK_UP) audio.Pause();
//		else if(aniValue == PlayerAniMove.WALK_RIGHT) audio.Play();

//		if(aniValue != PlayerAniMove.IDLE){
//			audio.Play();		
//		} 
//		else {
//			audio.Pause();	
//		}

		m_animator.SetInteger("move_dir", (int)aniValue);
		Vector3 scaleVal = m_aninode.localScale;
		scaleVal.x =  facingLeft * Mathf.Abs(m_aninode.localScale.x);
		m_aninode.localScale = scaleVal;


	}

	public void FocusToCamera(){
		Vector3 newPosCam = transform.position;
		newPosCam.z = m_camera.position.z;
		
		m_camera.position = newPosCam;
		//rigidbody2D.isKinematic = false;
	}

	public void ShowIdle(){
		m_animator.SetInteger("move_dir", (int)PlayerAniMove.IDLE);
		rigidbody2D.velocity = Vector2.zero;
		//rigidbody2D.isKinematic = true;

	}

//	void OnTriggerCollider2D(Collider2D p_collidedObj){
//		SensorObject sensor = p_collidedObj.GetComponent<SensorObject>();
//		if(sensor){
//			Debug.Log("The Player hit the sensor");
//			//TODO: show some effects before reloading
//			GameFieldManager.ReloadStage();
//		}
//	
//	}

//	public void OnGUI(){
//		//=================================
//		// Show passcode via unity gui
//		//---------------------------------
//		if(m_curPasscode != ""){
//			Camera cam = m_camera.GetComponent<Camera>();
//			Vector3 screenPos = cam.WorldToScreenPoint(m_callout.position);
//			screenPos.y = Screen.height - screenPos.y;
//
//			Vector2 pos = new Vector2(screenPos.x + m_PasscodePos.x, screenPos.y - m_PasscodePos.y);
//			GUI.Label(new Rect(pos.x, pos.y, 50, 50), m_curPasscode);
//		}
//
//	}

	public PlayerID GetPlayerNum(){
		return m_playerNum;
	}

	public void ShowPasscode(string p_passcode){

		m_curPasscode = p_passcode;
		m_callout.gameObject.SetActive(true);

	}

	public void HidePasscode(){
		m_curPasscode = "";
		m_callout.gameObject.SetActive(false);
	}

	public void BecomeNearTo(GameObject p_go){

		SwitchObject sw = p_go.GetComponent<SwitchObject>();
		if(sw){
			if(m_nearSwitch){
				//compare which is nearer
				float dist0 = (m_nearSwitch.transform.position - transform.position).sqrMagnitude;
				float dist1 = (p_go.transform.position - transform.position).sqrMagnitude;
				if(dist1 < dist0){
					m_nearSwitch = sw; // set the new near switch
				}
			}
			else {
				m_nearSwitch = sw;
			}
		}

		m_callout.gameObject.SetActive(true);
	}

	public void BecomeFarTo(GameObject p_go){

		SwitchObject sw = p_go.GetComponent<SwitchObject>();
		if(sw == m_nearSwitch){
			m_callout.gameObject.SetActive(false);
			m_nearSwitch = null;
		}
	}



}
