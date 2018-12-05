#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;


public class Enemy : NetworkBehaviour, IDamageable {

	[SerializeField] protected float MaxHealth = 100f;
	[SerializeField] protected float ChaseRadius = 5f;

	[SerializeField] protected float AttackRange = 2f;
    [SerializeField] protected float Damage = 9f;
    [SerializeField] protected float secBetweenShot = 0.5f;

    [SerializeField] GameObject ProjectileToUse;
    [SerializeField] Transform ProjectileSpownPoint;
    

	[SyncVar (hook = "OnChangeHealth")]protected float currentHealth;
    protected float nextAttackTime = 0;

	protected AICharacterControl aiCharacterController = null;
	[SyncVar (hook = "OnChangeTarget")][SerializeField] protected GameObject target = null;

    protected GameObject[] players;

    public float distansToTarget;


    public float healthAsPercentage 
	{
		get 
		{
			return currentHealth / MaxHealth;
		}
	}

    public virtual void CmdTakeDamage(float damage)
    {
        this.currentHealth = Mathf.Clamp(currentHealth - damage, 0, MaxHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnChangeHealth(float n)
    {
        currentHealth = n;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Awake()
	{
		currentHealth = MaxHealth;
    }
		
	void Start ()
    {
		aiCharacterController = GetComponent<AICharacterControl> ();
        SetPlayers();

        InvokeRepeating("CmdSetTarget", 1, 0.2f);
	}

	void Update ()
    {
        SetPlayers();
        if (isServer)
        {
            if (target)
            {
                /*float*/ distansToTarget = Vector3.Distance(target.transform.position, transform.position);
                if (distansToTarget < ChaseRadius)
                {
                    aiCharacterController.SetTarget(target.transform);
                }
                else
                {
                    aiCharacterController.SetTarget(transform);
                }
                if (distansToTarget < AttackRange && Time.time >= nextAttackTime)
                {
                    nextAttackTime = Time.time + secBetweenShot;
                    CmdAttack();
                }
            }
            else
            {
                aiCharacterController.SetTarget(transform);
            }
        }
    }

    public void SetPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    [Command]
    private void CmdSetTarget()
    {
        float ClosestDistans = Mathf.Max(ChaseRadius, AttackRange);
        foreach (GameObject player in players)
        {
            float distansToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if(distansToPlayer <= ClosestDistans)
            {
                target = player;
                ClosestDistans = distansToPlayer;
            }
        }
        if( ClosestDistans == Mathf.Max(ChaseRadius, AttackRange))
        {
            target = null;
        }
    }

    void OnChangeTarget(GameObject g)
    {
        target = g;
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
        if (!isServer)
        {
            SpownProjectile();
        }
    }

    void SpownProjectile()
    {
        Projectile newProjectile = Instantiate(ProjectileToUse, ProjectileSpownPoint.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.Damage = this.Damage;
        newProjectile.Target = target.transform;
        newProjectile.shooter = this.gameObject;
    }

    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        //drow chase circale
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.up, ChaseRadius);

        //drow attack circale
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, AttackRange);
#endif
    }
}
