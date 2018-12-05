using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour, IDamageable
{
    [HideInInspector] public PlayerHealthBar healthUI;
    public int connectionID;

    [SerializeField] float MaxHealth = 100f;

    [SyncVar(hook = "OnChangeHealth")]
    private float currentHealth;

    private Vector3 StartingPos;

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

        if (currentHealth <= 0)
        {
            if (LobbyManager.s_Singleton != null)
            {
                transform.position = StartingPos;
                this.currentHealth = MaxHealth;
                healthUI.ChangeHealthUI(healthAsPercentage);
            }
        }
    }

    void Awake()
    {
        currentHealth = MaxHealth;    
    }

    void Start()
    {
        StartingPos = this.transform.position;
    }
	
	void OnChangeHealth(float n)
    {
        currentHealth = n;
        healthUI.ChangeHealthUI(healthAsPercentage);
    }

    IEnumerator Respown()
    {
        GetComponent<PlayerMovement>().enabled = false;
        this.gameObject.SetActive(false);
        this.transform.position = StartingPos;
        yield return new WaitForSeconds(3);
        this.currentHealth = MaxHealth;
        healthUI.ChangeHealthUI(healthAsPercentage);
        GetComponent<PlayerMovement>().enabled = true;
        this.gameObject.SetActive(true);
    }
}
