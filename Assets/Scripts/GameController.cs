using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    [FormerlySerializedAs("startPos")] public Transform startTransform;
    public Player player;
    public TextMeshProUGUI playerTextBox;

    public string visionTriggerLayerName;
    private int layerBitMask = 0;
    public static GameController gameController;    
    private Camera[] cameras;
    private List<Rat> rats;

    //rat stuff
    public GameObject RatPrefab;
    public float ratSpawnRadius;
    public Transform ratSpawn1;

    private void updateCameras()
    {
        cameras = FindObjectsOfType<Camera>();
    }
    
    private void Awake()
    {
        gameController = this;
        updateCameras();
        string[] layers = new string[] { visionTriggerLayerName };
        layerBitMask = LayerMask.GetMask(layers);
        rats = new List<Rat>();
    }

    //switch cameras
    public void SwitchToCamWithName(string name)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            var cam = cameras[i];
            if (cam.name == name)
            {
                cam.gameObject.SetActive(true);
                print($"activated {cam.name}");
            }
            else
            {
                cam.gameObject.SetActive(false);
                print($"DEactivated {cam.name}");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize player position
        player.transform.position = startTransform.position;
        player.transform.rotation = startTransform.rotation;
        SwitchToCamWithName("Player Camera");
    }
    
 
    // Update is called once per frame
    void Update()
    {
        
    }
    
    //raycast on target layer
    public bool isLookingAtVisionTrigger()
    {
        Transform t = player.PlayerCamera.transform;
        return Physics.Raycast(t.position, t.forward, layerBitMask);
    }

    //send timed message to appear on screen-space text book (text object must be assigned)
    public void SendTextMessageToPlayer(string msg, float timeout)
    {
        StartCoroutine(DisplayMessageRoutine(msg, timeout));
    }

    //should be run as a coroutine
    IEnumerator DisplayMessageRoutine(string msg, float timeout)
    {
        playerTextBox.text = msg;
        yield return new WaitForSeconds(timeout);
        playerTextBox.text = "";
    }

    //spawn rat in its default state
    //spawns at random circle near player if no tf given
    private void spawnRat(Transform tf = null)
    {
        if (tf == null)
        {
            var angle = Random.Range(-180f, 180f);
            var pos = player.transform.position;
            pos.x += (float)(ratSpawnRadius * Math.Cos(angle));
            pos.z += (float)(ratSpawnRadius * Math.Sin(angle));
            pos.y = player.playerFeet.position.y;
            var rot = player.transform.rotation;
            rats.Add(Instantiate(RatPrefab, pos, rot).GetComponent<Rat>());
        }
        else
        {
            rats.Add(Instantiate(RatPrefab, tf).GetComponent<Rat>());
        }
        
        updateCameras();    
    }

    public void SpawnRatScareEvent()
    {
        spawnRat(ratSpawn1);
    }


    public void DestroyRat(Rat rat)
    {
        rats.Remove(rat);
        DestroyImmediate(rat.gameObject);
    }
}
