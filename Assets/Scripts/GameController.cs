using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{

    [FormerlySerializedAs("startPos")] public Transform startTransform;
    public Player player;
    public TextMeshProUGUI playerTextBox;

    public string visionTriggerLayerName;
    private int layerBitMask = 0;
    public static GameController gameController;    
    private Camera[] cameras;

    private void Awake()
    {
        gameController = this;
        cameras = FindObjectsOfType<Camera>();
        string[] layers = new string[] { visionTriggerLayerName };
        layerBitMask = LayerMask.GetMask(layers);
    }

    public void switchToCamWithName(string name)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            var cam = cameras[i];
            if(cam.name == name)
                cam.gameObject.SetActive(true);
            else
                cam.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize player position
        player.transform.position = startTransform.position;
        player.transform.rotation = startTransform.rotation;
        switchToCamWithName("Main Camera");
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

    public void SendTextMessageToPlayer(string msg, float timeout)
    {
        StartCoroutine(DisplayMessageRoutine(msg, timeout));
    }

    IEnumerator DisplayMessageRoutine(string msg, float timeout)
    {
        playerTextBox.text = msg;
        yield return new WaitForSeconds(timeout);
        playerTextBox.text = "";
    }
}
