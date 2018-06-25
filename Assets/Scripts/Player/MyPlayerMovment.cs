using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class MyPlayerMovment : NetworkBehaviour {

    private ThirdPersonCharacter thirdPersonCharacter = null;

    // Use this for initialization
    void Start () {
        this.enabled = isLocalPlayer;

        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
    }

    // Update is called once per frame
    void Update () {
        DirectMovement();
	}

    private void DirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }
}
