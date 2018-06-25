using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum AttackType
{
    melee,
    range
}

public class PlayerAttack : NetworkBehaviour {
    [Header("General")]
    public AttackType attackType;
    public Transform spownPoint;
    public float attackSpeed = 1f;//number of attacks per sec
    [Header("Ranged")]
    public GameObject Projectile;


    private CameraRaycaster cameraRaycaster = null;

    private float nextAttackTime = 0;

	// Use this for initialization
	void Start () {
        this.enabled = isLocalPlayer;

        nextAttackTime = 0;

        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();

        //cameraRaycaster.notifyMouseClickObservers += Attack;
    }
    private void Update()
    {
        nextAttackTime = Mathf.Clamp(nextAttackTime - Time.deltaTime, 0, 1 / attackSpeed);

        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
 
    private void Attack(/*RaycastHit raycastHit, int layerHit*/)
    {
        //transform.LookAt(new Vector3(raycastHit.point.x, 0, raycastHit.point.z));

        if (nextAttackTime > 0) { return; }

        switch (attackType)
        {
            case AttackType.melee:
                Instantiate(Projectile, spownPoint.transform.position, spownPoint.transform.rotation);
                break;
            case AttackType.range:
                Instantiate(Projectile, spownPoint.transform.position, spownPoint.transform.rotation);
                break;
        }
        nextAttackTime = 1 / attackSpeed;
    }
}
