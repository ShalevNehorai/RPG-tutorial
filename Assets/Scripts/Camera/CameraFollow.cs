using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private const string PlayerTag = "Player";

	private GameObject Player;

	void Start () {
		GameObject Player = GameObject.FindGameObjectWithTag (PlayerTag);
		if (Player != null) {
			this.Player = Player;
		}
	}

	void LateUpdate () {
		transform.position = this.Player.transform.position;
	}
}
