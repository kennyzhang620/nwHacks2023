using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayvideo : MonoBehaviour
{
    public ClipRenderer[] monitor;
    public int index = 0;
    public int max = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ClipRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown("space")){
        print("test");
        foreach(ClipRenderer x in monitor){
            if(x.index == index){
                x.PlayIndex(x.index);
            }
            else {
                x.PlayStatic();
            }

            if (index < max)
                index++;
            else
                index = 0;
        } 



       } 
        
    }
}
