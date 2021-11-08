using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript: MonoBehaviour {
    void Update()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        
        //TODO: remove just for test
        player.IncPoints();

        GameObject.FindWithTag("Points").GetComponent<Text>().text = "Points: " + player.points + "p";
    }
}
