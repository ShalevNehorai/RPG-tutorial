using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour
{
    RawImage healthBarRawImage;
    PlayerHealth player;

    // Use this for initialization
    void Start()
    {
        GetComponentInParent<PlayerHealth>().healthUI = this;
        healthBarRawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealthUI(float healthPersent)
    {
        float xValue = -(healthPersent / 2f) - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
