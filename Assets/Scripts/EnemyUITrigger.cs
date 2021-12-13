
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
            case "UP":
                up.gameObject.SetActive(true);
                break;
            case "DOWN":
                down.gameObject.SetActive(true);
                break;
            case "LEFT":
                left.gameObject.SetActive(true);
                break;
            case "RIGHT":
                right.gameObject.SetActive(true);
                break;
        }
    }
}
