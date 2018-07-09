using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(MyPlayerMovment))]
[RequireComponent(typeof(PlayerAttack))]
public class SetupLocalPlayer : NetworkBehaviour {

    [SyncVar (hook ="OnChangeAnimation")]
    public float animMovment;


    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

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
	
    [Command]
    public void CmdChangeAnim(float move)
    {
        ChangeAnim(move);
    }

    void ChangeAnim(float move)
    {
        animMovment = move;
        animator.SetFloat("Forward", Math.Abs(animMovment), 0.1f, Time.deltaTime);
    }
    void OnChangeAnimation(float move)
    {
        if (!isLocalPlayer)
        {
            ChangeAnim(move);
        }
    }
}
