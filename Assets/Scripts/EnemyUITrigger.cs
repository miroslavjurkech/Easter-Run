using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyUITrigger : MonoBehaviour
{
    private SphereCollider UITrigger;
    // Start is called before the first frame update
    void Start()
    {
        UITrigger = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other) {
        if ( other.gameObject.tag != "Player" )
            return;
        
        Debug.Log("UI should be displayed now");
    }
}
