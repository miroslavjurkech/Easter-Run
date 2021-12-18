using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class EnemyUITrigger : MonoBehaviour
{
    public Image up;
    public Image down;
    public Image left;
    public Image right;
    private AudioSource _sound;
    
    private void Awake() {
        _sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerSpine"))
            return;

        AudioSource.PlayClipAtPoint(_sound.clip, transform.position);

        switch(gameObject.GetComponentInParent<Enemy>().Direction)
        {
            case Swipe.Up:
                up.gameObject.SetActive(true);
                break;
            case Swipe.Down:
                down.gameObject.SetActive(true);
                break;
            case Swipe.Left:
                left.gameObject.SetActive(true);
                break;
            case Swipe.Right:
                right.gameObject.SetActive(true);
                break;
        }
    }
}
