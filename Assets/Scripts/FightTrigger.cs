using System;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerSpine"))
            return;

        var dir = gameObject.GetComponentInParent<Enemy>().Direction;
        
        GameObject.FindWithTag("Player").GetComponent<Player>().EnterFight(dir);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("PlayerSpine"))
            return;
        
        GameObject.FindWithTag("Player").GetComponent<Player>().CancelCancel();
    }
}
