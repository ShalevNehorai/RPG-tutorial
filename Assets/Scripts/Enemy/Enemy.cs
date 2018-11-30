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

	[SerializeField] float MaxHealth = 100f;
	[SerializeField] float ChaseRadius = 5f;

	[SerializeField] float AttackRange = 2f;
    [SerializeField] float Damage = 9f;
    [SerializeField] float secBetweenShot = 0.5f;

    [SerializeField] GameObject ProjectileToUse;
    [SerializeField] Transform ProjectileSpownPoint;
    

	[SyncVar (hook = "OnChangeHealth")]private float currentHealth;
    private float nextAttackTime = 0;

	private AICharacterControl aiCharacterController = null;
	[SyncVar (hook = "OnChangeTarget")][SerializeField] private GameObject target = null;

    private GameObject[] players;

	public float healthAsPercentage 
	{
		get 
		{
			return currentHealth / MaxHealth;
		}
	}

    public void CmdTakeDamage(float damage)
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
                float distansToTarget = Vector3.Distance(target.transform.position, transform.position);
                aiCharacterController.SetTarget(target.transform);
                if (distansToTarget < AttackRange && Time.time >= nextAttackTime)
                {
                    nextAttackTime = Time.time + secBetweenShot;
                    Attack();
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
        float ClosestDistans = ChaseRadius;
        foreach (GameObject player in players)
        {
            float distansToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if(distansToPlayer <= ClosestDistans)
            {
                target = player;
                ClosestDistans = distansToPlayer;
            }
        }
        if( ClosestDistans == ChaseRadius)
        {
            target = null;
        }
    }

    void OnChangeTarget(GameObject g)
    {
        target = g;
    }

    void Attack()
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
