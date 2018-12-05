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
        nextAttackTime = 0;

        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();

        //cameraRaycaster.notifyMouseClickObservers += Attack;
    }
    private void Update()
    {
        nextAttackTime = Mathf.Clamp(nextAttackTime - Time.deltaTime, 0, 1 / attackSpeed);

        if(Input.GetMouseButton(0))
        {
            Attack();
        }
    }
 
    private void SpownProjectile()
    {
        Instantiate(Projectile, spownPoint.transform.position, spownPoint.transform.rotation);
    }

    private void Attack()
    {
        if (nextAttackTime > 0) { return; }

        switch (attackType)
        {
            case AttackType.melee:
                Instantiate(Projectile, spownPoint.transform.position, spownPoint.transform.rotation);
                break;
            case AttackType.range:
                CmdAttack();
                break;
        }

        nextAttackTime = 1 / attackSpeed;
    }

    [Command]
    void CmdAttack()
    {
        SpownProjectile();
        RpcAttack();
    }

    [ClientRpc]
    void RpcAttack()
    {
        if(!isServer)
        {
            SpownProjectile();
        }
    }
}
