using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

[RequireComponent(typeof(MyPlayerMovment))]
[RequireComponent(typeof(PlayerAttack))]
public class SetupLocalPlayer : NetworkBehaviour {

    public int charID;

    [SyncVar (hook ="OnChangeAnimation")]
    public float animMovment;


    Animator animator;
	// Use this for initialization
	void Start () {
        //CmdUpdatePlayerCharacter(charID);

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

    private void OnGUI()
    {
        if (isLocalPlayer)
        {
            if (Event.current.Equals(Event.KeyboardEvent("0")) ||
               Event.current.Equals(Event.KeyboardEvent("1")) ||
               Event.current.Equals(Event.KeyboardEvent("2")) ||
               Event.current.Equals(Event.KeyboardEvent("3")))
            {
                int charID = int.Parse(Event.current.keyCode.ToString().Substring(5)) + 1;
                CmdUpdatePlayerCharacter(charID);
            }
            else if(Event.current.Equals(Event.KeyboardEvent("Space")))
            {
                CmdUpdatePlayerCharacter(charID + 1);
            }
        }
    }

    [Command]
    public void CmdUpdatePlayerCharacter(int cid)
    {
        //LobbyManager.s_Singleton.SwitchPlayer(this, cid);
    }
}
