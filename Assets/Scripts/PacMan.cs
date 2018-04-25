//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class PacMan : MonoBehaviour {
//
//	public float speed = 10f;
//	private Vector3 direction = Vector3.zero;
//
//	private Node currentNode;	
//
//	// Use this for initialization
//	void Start () {
//		//Node node = GetNodeAtPosition (transform.localPosition);
//
////		if (node != null) {
////		
////			currentNode = node;
////
////		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		CheckInput();
//
//		//Move ();
//
//		updateOrientation ();
//	}
//
//	void CheckInput(){
//		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
//			direction = Vector3.left;
//			MoveToNode (direction);
//		}else if (Input.GetKeyDown (KeyCode.RightArrow)) {
//			direction = Vector3.right;
//			MoveToNode (direction);
//		}else if (Input.GetKeyDown (KeyCode.DownArrow)) {
//			direction = Vector3.back;
//			MoveToNode (direction);
//		}else if (Input.GetKeyDown (KeyCode.UpArrow)) {
//			direction = Vector3.forward;
//			MoveToNode (direction);
//		}
//	}
//
//	void Move(){
//		transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
//	}
//
//	void MoveToNode(Vector3 d){
//		Node moveToNode = CanMove (d);
//		if (moveToNode != null) {
//			transform.localPosition = moveToNode.transform.position;
//			currentNode = moveToNode;
//		}
//	}
//
//	void updateOrientation(){
//		if (direction == Vector3.right) {
//			transform.localEulerAngles = new Vector3 (0, 90, 0);
//		} else if (direction == Vector3.left) {
//			transform.localEulerAngles =  new Vector3(0,270,0);
//		} else if (direction == Vector3.back) {
//			transform.localEulerAngles =  new Vector3(0,180,0);
//		} else if (direction == Vector3.forward) {
//			transform.localEulerAngles =  new Vector3(0,0,0);
//		}
//	}
//
////	Node CanMove(Vector3 d){
////		Node moveToNode = null;
////
////		for (int i = 0; i < currentNode.neighbors.Length; i++) {
////			if (currentNode.validDirections [i] == d) {
////				moveToNode = currentNode.neighbors [i];
////				break;
////			}
////		}
////		return moveToNode;
////	}
////
////	Node GetNodeAtPosition( Vector3 pos){
////		GameObject tile = GameObject.Find ("Game").GetComponent<GameBoard> ().board [(int)pos.x, (int)pos.z];
////
////		if (tile != null) {
////			return tile.GetComponent<Node> ();
////		}
////		return null;
////	}
//}
