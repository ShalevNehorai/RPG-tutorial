using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(MyPlayerMovment))]
[RequireComponent(typeof(PlayerAttack))]
public class SetupLocalPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	    if(isLocalPlayer)
        {
            GetComponent<MyPlayerMovment>().enabled = true;
            GetComponent<PlayerAttack>().enabled = true;
            CameraFollow.SetPlayer = this.gameObject;
        }
        else
        {
            GetComponent<MyPlayerMovment>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
        }
	}
	
}
