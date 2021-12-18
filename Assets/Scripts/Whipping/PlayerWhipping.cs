using UnityEngine;

namespace Whipping
{
    [RequireComponent(typeof(Animator))]
    public class PlayerWhipping : MonoBehaviour
    {
        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        public void Whip()
        {
            _animator.SetTrigger("whip");
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
