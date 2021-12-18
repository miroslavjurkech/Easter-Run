using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField]
    private AudioClip _takeSound;

    private void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.tag.Equals("PlayerSpine")) 
            return;
        
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        AudioSource.PlayClipAtPoint(_takeSound, transform.position);

        player.IncPoints();
        Destroy(gameObject);
    }
}
