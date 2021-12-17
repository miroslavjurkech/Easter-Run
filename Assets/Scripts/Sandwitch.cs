using UnityEngine;

public class Sandwitch : MonoBehaviour
{   
    private void OnCollisionEnter(Collision other) {
        if ( other.gameObject.tag != "Player" ) return;
        
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            player.IncHealth();
            player.Run();
        }
        else
            Debug.LogError("Player does not have Player script on him!");
        
        Destroy(gameObject);
    }
}
