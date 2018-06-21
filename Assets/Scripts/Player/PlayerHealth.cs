using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    [SerializeField] float MaxHealth = 100f;

    private float currentHealth;

    public float healthAsPercentage
    {
        get
        {
            return currentHealth / MaxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        this.currentHealth = Mathf.Clamp(currentHealth - damage, 0, MaxHealth);
    }

    // Use this for initialization
    void Awake()
    {
        currentHealth = MaxHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
