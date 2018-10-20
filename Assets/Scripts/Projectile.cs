using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    
    public float Speed = 10f;
    [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);

    [HideInInspector]public Transform Target;
    [HideInInspector]public float Damage;
    [HideInInspector]public GameObject shooter;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GetComponent<Rigidbody>().velocity = (Target.position + aimOffset - transform.position).normalized * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == shooter)
        {
            return;
        }
        IDamageable damageableComponent = other.gameObject.GetComponent<IDamageable>();
        if (damageableComponent != null)
        {
            damageableComponent.CmdTakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}
