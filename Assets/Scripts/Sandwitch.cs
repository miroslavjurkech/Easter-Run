using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sandwitch : MonoBehaviour
{   
    private AudioSource sound;

    private void Awake() {
        sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        if ( other.gameObject.tag != "Player" ) return;
        
        AudioSource.PlayClipAtPoint(sound.clip, transform.position);
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
