using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.tag.Equals("PlayerSpine")) 
            return;
        
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();

        player.IncPoints();
        Destroy(gameObject);
    }
}
