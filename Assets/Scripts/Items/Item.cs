using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool pickedUp;
    public bool PickedUp => pickedUp;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && pickedUp)
            Drop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pickedUp && other.CompareTag("Player"))
            GameController.gameController.SendTextMessageToPlayer($"Press E to pickup {name}.", 3f);
    }

    private void OnTriggerStay(Collider other)
    {
        // print("trigger");
        if (!pickedUp && other.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.E))
            {
                var p = other.GetComponent<Player>();
                p.PickupItem(this);
                pickedUp = true;
                rb.isKinematic = true;
                GameController.gameController.SendTextMessageToPlayer($"Press F to drop {name}.", 3f);
            }
        }
    }

    void Drop()
    {
        transform.parent = null;
        rb.isKinematic = false;
        pickedUp = false;
    }
}
