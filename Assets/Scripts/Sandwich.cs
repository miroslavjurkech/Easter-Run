using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sandwich : MonoBehaviour
{   
    private AudioSource _sound;

    private void Awake() {
        _sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        if ( !other.gameObject.CompareTag("Player") ) return;
        
        AudioSource.PlayClipAtPoint(_sound.clip, transform.position);
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
