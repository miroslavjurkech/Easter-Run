using Behaviour;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    public AudioClip wonFight;
    public AudioClip lostFight;
    
    private Animator _anim;
    public Swipe Direction { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        Direction = ChooseDirection();
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggerol ma: " + other.tag + " name: " + other.name);
        
        if (!other.CompareTag("PlayerKickFoot")) return;
        
        if (lostFight != null)
        {
            AudioSource.PlayClipAtPoint(lostFight, transform.position);
        }

        _anim.SetTrigger("die");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerSpine")) return;
        
        var script = GameObject.FindWithTag("Player").GetComponent<Player>();
        script.CancelCancel();
        script.LeaveFight();
    }

    //Event z punch animacie
    public void Hit()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().GetHit();
    }

    public void Punch()
    {
        if (wonFight != null)
        {
            AudioSource.PlayClipAtPoint(wonFight, transform.position);
        }

        _anim.SetTrigger("punch");
    }

    private Swipe ChooseDirection()
    {
        int dir = Random.Range(0, 4);
        
        switch(dir)
        {
            case 0:
                return Swipe.Up;
            case 1:
                return Swipe.Down;
            case 2:
                return Swipe.Left;
            case 3:
                return Swipe.Right;
        }

        return Swipe.None;
    }
}
