using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Vector3 startPos;
    public Player player;

    public string visionTriggerLayerName;
    private int layerBitMask = 0;
    public static GameController gameController;

    private void Awake()
    {
        gameController = this;
        string[] layers = new string[] { visionTriggerLayerName };
        layerBitMask = LayerMask.GetMask(layers);
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize player position
        player.transform.position = startPos;
    }
    
 
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool isLookingAtVisionTrigger()
    {
        Transform t = player.PlayerCamera.transform;
        return Physics.Raycast(t.position, t.forward, layerBitMask);
    }
}
