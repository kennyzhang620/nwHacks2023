using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoLoader : MonoBehaviour
{
    public ClipRenderer C;
    public GameObject Canvas;
    public int x = 0;
    public int MaxVal = 2;
    public Light[] Lights;
    // Start is called before the first frame update
    void OnTriggerStay(Collider x0) {
        print("test");

        if (!Canvas.activeSelf) {
            Canvas.SetActive(true);
        }
        
        if (Input.GetKeyDown("k")) {

        if (x < MaxVal) {
            C.PlayIndex(x++);
        }
        else {
            foreach (Light l in Lights) {
                l.enabled = false;
            }
        }
        }
    }
}
