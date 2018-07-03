using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private const string PlayerTag = "Player";

	private static GameObject Player;


    public static GameObject SetPlayer
    {
        set { Player = value; }
    }

	void Start () {
        //SearchForPlayer();
    }

    private void Update()
    {
        if (Player == null)
        {
            //SearchForPlayer();
        }
    }

    void LateUpdate () {
		transform.position = Player.transform.position;
	}

    //public void SearchForPlayer()
    //{
    //    GameObject Player = GameObject.FindGameObjectWithTag(PlayerTag);
    //    if (Player != null)
    //    {
    //        this.Player = Player;
    //    }
    //}
}
