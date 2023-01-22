using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateMessageTrigger : MonoBehaviour
{
    private int messageCount = 0;

    private void Start()
    {
        messageCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && messageCount < 1)
        {
            messageCount += 1;
            GameController.gameController.SendTextMessageToPlayer("Investigate.\n(hint: Press Key1 and Key2 to switch video feeds.)", 6);
        }
    }
}
