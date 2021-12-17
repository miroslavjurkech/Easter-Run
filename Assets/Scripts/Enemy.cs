using System.Collections;
using System.Collections.Generic;
using Behaviour;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private Animator anim;
    public Swipe Direction { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        Direction = ChooseDirection();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerKickFoot"))
        {
            anim.SetTrigger("die");
        }
    }

    //Event z punch animacie
    public void Hit()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().GetHit();
    }

    public void Punch()
    {
        anim.SetTrigger("punch");
    }

    public Swipe ChooseDirection()
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
