using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrier : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Debug.Log("You collided with barrier: " + other.gameObject.tag);

        if (!other.gameObject.tag.Equals("Player")) return;
        
        GameObject.FindWithTag("Player").GetComponent<Player>().HitBarrier(gameObject);
    }
}
