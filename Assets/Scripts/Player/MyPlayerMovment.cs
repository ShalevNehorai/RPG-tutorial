using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;

//TODO: change the movment to run with WASD and look at mouse position
public class MyPlayerMovment : NetworkBehaviour
{
    public float speed = 1000f;

    private ThirdPersonCharacter thirdPersonCharacter = null;
    private Animator animator = null;
    private Rigidbody rigidbody = null;

    // Use this for initialization
    void Start()
    {
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DirectMovement();
        LookAtMouse();
    }

    private void DirectMovement()
    {
        float v = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        rigidbody.MovePosition(rigidbody.position + movement);

        UpdateAnimator(movement * speed);
    }

    private void LookAtMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rigidbody.MoveRotation(newRotation);
        }
    }

    //TODO: put animations
    private void UpdateAnimator(Vector3 movement)
    {
        animator.SetFloat("Forward", Math.Abs(movement.z), 0.1f, Time.deltaTime);
    }
}

