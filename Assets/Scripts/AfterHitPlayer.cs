using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterHitPlayer : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var enemy = animator.GetComponent<Transform>();
        
        GameObject.FindWithTag("Player").GetComponent<Player>().HitBarrier(enemy.gameObject, enemy.parent.parent);
    }
}
