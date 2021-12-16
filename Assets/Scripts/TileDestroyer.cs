using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDestroyer : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("PlayerSpine")) 
            return;

        StartCoroutine(DisposeTile(gameObject));
    }
    
    private static IEnumerator DisposeTile(GameObject tile)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Destroying tile: " + tile.name);
        Destroy(tile);
    }
}
