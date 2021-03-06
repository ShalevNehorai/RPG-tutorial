﻿using UnityEngine;

// Add a UI Socket transform to your enemy
// Attack this script to the socket
// Link to a canvas prefab that contains NPC UI
public class CharHealthUI : MonoBehaviour {

    // Works around Unity 5.5's lack of nested prefabs
    [Tooltip("The UI canvas prefab")]
    [SerializeField] GameObject CanvasPrefab = null;

    Camera cameraToLookAt;

    private GameObject healthHolder;

    // Use this for initialization 
    void Start()
    {
        cameraToLookAt = Camera.main;
        Instantiate(CanvasPrefab, transform.position, Quaternion.identity, transform);


        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        healthHolder = Instantiate(CanvasPrefab, Vector3.zero, Quaternion.identity);
        healthHolder.transform.SetParent(canvas.transform);
    }

    // Update is called once per frame 
    void LateUpdate()
    {
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }

}