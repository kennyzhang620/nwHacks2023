using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : Item
{
    private bool triggeredRatScareEvent = false;
    private void LateUpdate()
    {
        if (PickedUp && !triggeredRatScareEvent)
        {
            GameController.gameController.SpawnRatScareEvent();
            triggeredRatScareEvent = true;
        }
    }
}
