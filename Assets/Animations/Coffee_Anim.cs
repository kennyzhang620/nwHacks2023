using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee_Anim : MonoBehaviour
{
    public Player p;
    // Start is called before the first frame update
   public void EnablePlayer()
    {
        p.enabled = true;
    }
    public void DisablePlayer()
    {
        p.enabled = false;
    }
}
