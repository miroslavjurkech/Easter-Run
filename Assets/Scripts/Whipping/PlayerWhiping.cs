using UnityEngine;

namespace Whipping
{
    [RequireComponent(typeof(Animator))]
    public class PlayerWhiping : MonoBehaviour
    {
        private Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
        }

        public void Whip()
        {
            animator.SetTrigger("whip");
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Sibol som " + other.gameObject.tag);
        }

        private void OnCollisionExit(Collision other)
        {
            Debug.Log("Vychadzam z " + other.gameObject.tag);
        }
    }
}
