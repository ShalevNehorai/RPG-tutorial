using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour, IDamageable {


    [SerializeField] int enemyLayer = 9;
    [SerializeField] float MaxHealth = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] float secBetweenHit = 0.5f;
    [SerializeField] float maxAttackRange = 2f;

    private float currentHealth;
    private float lastHitTime = 0f;

    private CameraRaycaster cameraRaycaster = null;

    private GameObject currentTarget;

	public float healthAsPercentage 
	{
		get 
		{
			return currentHealth / MaxHealth;
		}
	}

    void IDamageable.CmdTakeDamage(float damage)
    {
        this.currentHealth = Mathf.Clamp(currentHealth - damage, 0, MaxHealth);
    }

    void Awake()
	{
		currentHealth = MaxHealth;
	}

	// Use this for initialization
	void Start () {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();

        cameraRaycaster.notifyMouseClickObservers += onMouseClick;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void onMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if (layerHit == enemyLayer)
        {
            currentTarget = raycastHit.collider.gameObject;

            //check distanse to enemy
            if ((transform.position - currentTarget.transform.position).magnitude > maxAttackRange)
            {
                return;
            }

            Component damageableComponent = currentTarget.GetComponent(typeof(IDamageable));
            if (damageableComponent && (Time.time - lastHitTime > secBetweenHit))
            {
                (damageableComponent as IDamageable).CmdTakeDamage(damage);
                lastHitTime = Time.time;
            }
        }
    }
    
   
}
