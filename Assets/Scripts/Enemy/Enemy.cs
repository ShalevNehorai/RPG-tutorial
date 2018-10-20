using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;


public class Enemy : MonoBehaviour, IDamageable {

	[SerializeField] float MaxHealth = 100f;
	[SerializeField] float ChaseRadius = 5f;

	[SerializeField] float AttackRange = 2f;
    [SerializeField] float Damage = 9f;
    [SerializeField] float secBetweenShot = 0.5f;

    [SerializeField] GameObject ProjectileToUse;
    [SerializeField] Transform ProjectileSpownPoint;
    

	private float currentHealth;
    private float nextAttackTime = 0;
    private CapsuleCollider CapsuleCollider;

	private AICharacterControl aiCharacterController = null;
	private GameObject target = null;

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

    void Awake()
	{
		currentHealth = MaxHealth;
        CapsuleCollider = GetComponent<CapsuleCollider>();
        CapsuleCollider.radius = Mathf.Max(ChaseRadius, AttackRange);
    }
		
	void Start ()
    {
		aiCharacterController = GetComponent<AICharacterControl> ();
	}

	void Update ()
    {
        if (target)
        {
            float distansToTarget = Vector3.Distance(target.transform.position, transform.position);
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
                Attack();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!target)
            {
                target = other.gameObject;
            }
            else
            {
                float distansToTarget = Vector3.Distance(target.transform.position, transform.position);
                float distansToNewTarget = Vector3.Distance(other.transform.position, transform.position);
                if (distansToTarget > distansToNewTarget)
                {
                    target = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == target)
        {
            target = null;
        }
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
        //drow chase circale
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.up, ChaseRadius);

        //drow attack circale
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, AttackRange);
    }
}
