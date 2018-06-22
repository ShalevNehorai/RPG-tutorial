using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

    public float Damage = 50;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageableComponent = other.gameObject.GetComponent<IDamageable>();
        if (damageableComponent != null && other.tag != "Player")
        {
            damageableComponent.TakeDamage(Damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireMesh(new Mesh(), transform.position, transform.rotation, transform.localScale);
    }
}
