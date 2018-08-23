using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeProjectileThatHeals : MonoBehaviour {

    public float Speed = 1;
    public float Range = 10;
    public float Damage = 50;
    public float HealPoints = 10;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, Range / Speed);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageableComponent = other.gameObject.GetComponent<IDamageable>();
        if (damageableComponent != null)
        {
            if (other.tag != "Player")
            {
                damageableComponent.CmdTakeDamage(Damage);
            }
            else
            {
                damageableComponent.CmdTakeDamage(-HealPoints);
            }
        }

        Destroy(gameObject);
    }
}
