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

    void LateUpdate ()
    {
        if (Player != null)
        {
            transform.position = Player.transform.position;
        }
	}

    public void SearchForPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PlayerTag);
        if (player != null)
        {
            SetPlayer = player;
        }
    }
}
