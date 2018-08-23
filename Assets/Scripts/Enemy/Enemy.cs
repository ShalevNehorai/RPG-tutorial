using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEditor;
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

	private AICharacterControl aiCharacterController = null;
	private GameObject Player = null;

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
	}
		
	void Start ()
    {
		aiCharacterController = GetComponent<AICharacterControl> ();
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update ()
    {
		float distansToTarget = Vector3.Distance(Player.transform.position, transform.position);
		if (distansToTarget < ChaseRadius) 
		{
			aiCharacterController.SetTarget (Player.transform);
		} 
		else 
		{
			aiCharacterController.SetTarget (transform);
		}

        if (distansToTarget < AttackRange && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + secBetweenShot;
            Attack();
        }
    }

    void Attack()
    {
        Projectile newProjectile = Instantiate(ProjectileToUse, ProjectileSpownPoint.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.Damage = this.Damage;
        newProjectile.Target = Player.transform;
    }

	//void OnDrawGizmos()
	//{
 //       //drow chase circale
	//	Handles.color = Color.blue;
	//	Handles.DrawWireDisc (transform.position, Vector3.up, ChaseRadius);

 //       //drow attack circale
	//	Handles.color = Color.red;
	//	Handles.DrawWireDisc (transform.position, Vector3.up, AttackRange);
	//}
}
