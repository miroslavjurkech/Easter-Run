using UnityEngine;

namespace whiping
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
    }
}
