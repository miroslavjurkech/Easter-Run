using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrier : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {

        if (!other.gameObject.tag.Equals("Player")) 
            return;
        
        GameObject.FindWithTag("Player").GetComponent<Player>().HitBarrier(gameObject);
    }
}
