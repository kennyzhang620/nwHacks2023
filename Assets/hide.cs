using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour
{
    public GameObject close;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape")) {
            close.SetActive(false);
            GameController.gameController.SpawnRatAttackEvent();
        }
    }
}
