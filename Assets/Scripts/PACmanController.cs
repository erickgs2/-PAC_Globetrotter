using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PACmanController : MonoBehaviour {
	public float MoventSpeed = 0f;
	private Vector3 up = new Vector3(0,0,0),
		right = new Vector3(0,90,0),
		down = new Vector3(0,180,0),
		left = new Vector3(0,270,0),
		currentDirection = Vector3.zero;

	private Vector3 initialPosition = new Vector3(5,1.5f,4);
	public Text score;
	public Text time;
	public Text tothemoon;
	private float counterTime = 0f;
	public Text highScore;
	public GameObject[] portales;
	private int counter;
	private Animator animator = null;
	private float waitTime = 0;
	bool isMoving = false;
	public Button[] uiButtons;
	public void Reset(){
		transform.position = initialPosition;
		animator.SetBool ("isDead", false);
		animator.SetBool ("isMoving",false);
		currentDirection = right;
	}


	// Use this for initialization
	void Start () {
		//GameObject.Find ("Controls").transform.localEulerAngles.z = GameObject.Find ("Main Camera").transform.localEulerAngles.z;
		GameObject.Find ("Controls").transform.localEulerAngles = new Vector3 (0, 0, (GameObject.FindWithTag ("MainCamera").transform.localEulerAngles.y));
		//tothemoon.gameObject.active = false;
		QualitySettings.vSyncCount = 0;
		initialPosition = transform.position;
		counter = 0;
		time.text = "Time: 0.00";
		animator = GetComponent<Animator> ();
		Reset();
		Button UPbtn = uiButtons[0].GetComponent<Button>();
		Button DOWNbtn = uiButtons[1].GetComponent<Button>();
		Button LEFTbtn = uiButtons[2].GetComponent<Button>();
		Button RIGHTbtn = uiButtons[3].GetComponent<Button>();
		UPbtn.onClick.AddListener(MoveUp);
		DOWNbtn.onClick.AddListener(MoveDown);
		LEFTbtn.onClick.AddListener(MoveLeft);
		RIGHTbtn.onClick.AddListener(MoveRight);
	}

	public void MoveUp(){
		currentDirection = up;
		if(!isMoving)isMoving = true;
	}
	public void MoveDown(){
		currentDirection = down;
		if(!isMoving)isMoving = true;
	}
	public void MoveLeft(){
		currentDirection = left;
		if(!isMoving)isMoving = true;
	}
	public void MoveRight(){
		currentDirection = right;
		if(!isMoving)isMoving = true;
	}

	// Update is called once per frame
	void Update () {
		GameObject.Find ("Controls").transform.localEulerAngles = new Vector3 (0, 0, (GameObject.FindWithTag ("MainCamera").transform.localEulerAngles.y));
		counterTime += Time.deltaTime;
		waitTime += Time.deltaTime;
		time.text = "Time: " + counterTime.ToString("0.00");


		if (Input.GetKey (KeyCode.UpArrow)) {
			currentDirection = up;
			if(!isMoving)isMoving = true;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			currentDirection = right;
			if(!isMoving)isMoving = true;
		}else if (Input.GetKey (KeyCode.DownArrow)) {
			currentDirection = down;
			if(!isMoving)isMoving = true;
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			currentDirection = left;
			if(!isMoving)isMoving = true;
		}
//		else isMoving = false;
		transform.localEulerAngles = currentDirection;

//		animator.SetBool ("isMoving", isMoving);

		if (isMoving) {
			transform.Translate (Vector3.forward * MoventSpeed * Time.deltaTime);

		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up") || other.gameObject.CompareTag("ToTheMoon") || other.gameObject.CompareTag("Random"))
		{
			counter += 1;
			score.text = "Score: " + counter.ToString ();
			other.gameObject.SetActive (false);

            if(other.gameObject.CompareTag("ToTheMoon")) {
				tothemoon.gameObject.active = true;
                GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");

                foreach (GameObject go in ghosts)
                {
                    go.GetComponent<TheGhost>().StartFrightenedMode();
                }
            }

		}
		if (other.gameObject.CompareTag ("Ghost"))
		{
            GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");

            foreach (GameObject go in ghosts)
            {
                if(go.GetComponent<TheGhost>().currentMode != TheGhost.Mode.Fightened)
                {
					if (other.gameObject.name == go.gameObject.name) {
						gameObject.SetActive (false);
					}
                }
            }
		}
		if (other.gameObject.CompareTag ("Portal"))
		{
			if (transform.position.x > 0 && waitTime >= .5) {
				transform.position = new Vector3 (-25f , 1f, 5f);
				waitTime = 0;
			} else if(transform.position.x < -27.5){
				transform.position = new Vector3 (25f, 1f, 5f);
				waitTime = 0;
			}
		}

	}
		
//	GameObject GetPortal(Vector3 pos) {
//		GameObject tile = GameObject.Find ("Game").GetComponent<GameBoard> ().board [(int)pos.x, (int)pos.z];
//
//		if(tile.GetComponent<Tile>() != null) {
//
//			if (tile != null) {
//				if(tile.GetComponent<Tile>().isPortal) {
//					GameObject otherPortal = tile.GetComponent<Tile>().portalReceiver;
//					return otherPortal;
//				}
//			}
//		}
//
//		return null;
//	}
}
