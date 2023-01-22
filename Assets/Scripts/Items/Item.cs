using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool pickedUp;

    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.E) && pickedUp)
        //     Drop();
    }

    private void OnTriggerStay(Collider other)
    {
        print("trigger");
        if (!pickedUp && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            var p = other.GetComponent<Player>();
            p.PickupItem(this);
            pickedUp = true;
        }
    }

    void Drop()
    {
        transform.parent = null;
    }
}
