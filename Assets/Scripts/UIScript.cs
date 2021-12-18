using UnityEngine;
using UnityEngine.UI;

public class UIScript: MonoBehaviour {
    void Update()
    {
        var script = GameObject.FindWithTag("Player").GetComponent<Player>();

        GameObject.FindWithTag("Points").GetComponent<Text>().text = "Points: " + script.points + "p";
    }
    
    
}
