using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (AICharacterControl))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	private ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
	private CameraRaycaster cameraRaycaster = null;
	private AICharacterControl aiCharacterControl = null;

	GameObject walkTarget = null;
	Vector3 currentDestination, clickPoint;

	private bool isinDirectMode = false;
        
    private void Start()
    {
		walkTarget = new GameObject ("WalkTarget");
		walkTarget.transform.position = transform.position;

		thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
		aiCharacterControl = GetComponent<AICharacterControl> ();

        currentDestination = transform.position;

		cameraRaycaster.notifyMouseClickObservers += MoveToTarget;
    }
	// TODO call that some time
	private void DirectMovement()
	{
		print ("in direct movment");
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

		thirdPersonCharacter.Move (movement, false, false);
	}
	void MoveToTarget(RaycastHit raycastHit, int layerHit)
	{
		switch (layerHit) {
		case 8://walkable
			walkTarget.transform.position = raycastHit.point;
			aiCharacterControl.SetTarget (walkTarget.transform);
			break;
		case 9://enemy
			GameObject enemy = raycastHit.collider.gameObject;
			aiCharacterControl.SetTarget (enemy.transform);
			break;
		default:
			Debug.LogWarning ("unexpected layer found");
			return;
		}
	}
}

