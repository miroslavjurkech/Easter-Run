using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private BoxCollider animationTrigger;
    private Animator anim;
    
    [Header("Adjust time from collision with player to dead animation")]
    [SerializeField] float waitForDeadTime;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        animationTrigger = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other) {
        if ( other.gameObject.tag != "Player" )
            return;

        StartCoroutine(CorutineForDead());
    }

    private IEnumerator CorutineForDead() 
    {
        yield return new WaitForSeconds(waitForDeadTime);
        anim.SetBool("isDead", true);
        Debug.Log("Enemy is dead");
    }
}
