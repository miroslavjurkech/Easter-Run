using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerSpine"))
            return;
        
        GameObject.FindWithTag("Player").GetComponent<Player>().Kick();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("PlayerSpine"))
            return;
        
        var script = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (script.IsSuccessfulFight())
        {
            script.LeaveFight();
        }
        else
        {
            script.Stop();
            gameObject.GetComponentInParent<Enemy>().Punch();
        }
    }
}
