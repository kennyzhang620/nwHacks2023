using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Camera playerCamera;
    
    public Rigidbody RigidBody => rb;
    public Camera PlayerCamera => playerCamera;
    public float cameraY = 30f;
    
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    
    //for picking up objects
    public Transform pickupLocationLocal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0f, moveZ);

        if (rb.velocity.magnitude < 3)
        {
            rb.AddRelativeForce(transform.position + moveSpeed * Time.deltaTime * movement * 100);
        }
        
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX);
        float mouseY = Input.GetAxis("Mouse Y");
        print(cameraY);
      
        if (mouseY > 0 && cameraY < 900){
        transform.Rotate(Vector3.left * mouseY);
        cameraY++;
        }

        if (mouseY < 0 && cameraY > -900){
        transform.Rotate(Vector3.left * mouseY);
        cameraY--;
        }


//        if(cameraY<500 &&cameraY >=0){
  //      cameraY+-=1;
    //       transform.Rotate(Vector3.left * mouseY);
      //  }
    

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    public void PickupItem(Item item)
    {
        item.transform.position = pickupLocationLocal.position;
        item.transform.rotation = pickupLocationLocal.rotation;
        item.transform.parent = transform;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
