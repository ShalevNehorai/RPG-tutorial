using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour, IDamageable
{
    [HideInInspector] public PlayerHealthBar healthUI;

    [SerializeField] float MaxHealth = 100f;

    [SyncVar(hook = "OnChangeHealth")]
    private float currentHealth;

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
        healthUI.ChangeHealthUI(healthAsPercentage);

        if(currentHealth<=0)
        {
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        currentHealth = MaxHealth;
    }
	
	void OnChangeHealth(float n)
    {
        currentHealth = n;
        healthUI.ChangeHealthUI(healthAsPercentage);
    }
}
