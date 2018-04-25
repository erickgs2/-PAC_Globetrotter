using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

	private static int BoardHeight = 10000;
	private static int BoardWidth = 10000;

	public GameObject[,] board = new GameObject[BoardWidth,BoardHeight];

	// Use this for initialization
	void Start () {

		Object[] obj = GameObject.FindObjectsOfType (typeof(GameObject));

		foreach (GameObject o in obj) {
			Vector3 pos = o.transform.position;

			if (o.name != "Pacman") {
				board [(int)pos.x, (int)pos.z] = o;
			} else {
				Debug.Log ("Fund Pacman at:" + pos);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
