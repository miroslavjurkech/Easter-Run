using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickTrigger : MonoBehaviour
{
    private bool _exited = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerSpine"))
            return;
     
        Debug.Log("PlayerEntered");
        if (!_exited)
        {
            var script = GameObject.FindWithTag("Player").GetComponent<Player>();

            if (script.IsSuccessfulFight())
            {
                script.Kick();
                script.LeaveFight();
            }
            else
            {
                script.Stop();
                gameObject.GetComponentInParent<Enemy>().Punch();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("PlayerExited");
        _exited = true;
    }
}
