using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveToGameScene(GameObject player)
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        if (Player != null)
        {
            Destroy(Player);
        }
        Instantiate(player, Vector3.zero, Quaternion.identity);
    }
}
