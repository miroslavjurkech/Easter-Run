
using Behaviour;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class EnemyUITrigger : MonoBehaviour
{
    public Image up;
    public Image down;
    public Image left;
    public Image right;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerSpine"))
            return;

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
