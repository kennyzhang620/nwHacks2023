using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed;
    public Camera faceCamera;
    public Camera backCamera;

    private void Awake()    
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dashForward());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * moveSpeed;
    }

    //coroutine for rat scare event
    IEnumerator dashForward(float cameraSwitchTime=5f)
    {
        GameController.gameController.SwitchToCamWithName(backCamera.name);
        yield return new WaitForSeconds(cameraSwitchTime/2);
        GameController.gameController.SwitchToCamWithName(faceCamera.name); 
        yield return new WaitForSeconds(cameraSwitchTime/2);
        GameController.gameController.SwitchToCamWithName(GameController.gameController.player.PlayerCamera.name);
        GameController.gameController.DestroyRat(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            GameController.gameController.SwitchToCamWithName(GameController.gameController.player.PlayerCamera.name);
            GameController.gameController.DestroyRat(this);
        }
    }
}
