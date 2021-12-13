using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private BoxCollider animationTrigger;
    private Animator anim;
    public string Direction { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        Direction = ChooseDirection();
        anim = GetComponent<Animator>();
        animationTrigger = GetComponent<BoxCollider>();
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

    public string ChooseDirection()
    {
        int dir = Random.Range(0, 4);
        
        switch(dir)
        {
            case 0:
                return "UP";
            case 1:
                return "DOWN";
            case 2:
                return "LEFT";
            case 3:
                return "RIGHT";
        }

        return "";
    }
}
