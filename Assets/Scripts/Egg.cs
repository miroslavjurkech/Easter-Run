using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You collected an egg");

        if (!other.gameObject.tag.Equals("Player")) return;
        
        var player = other.gameObject.GetComponent<Player>();

        player.IncPoints();
        Destroy(gameObject);
        //player.Run();
    }
}
