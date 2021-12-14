using UnityEngine;

namespace whiping
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class WhipedGirl : MonoBehaviour
    {
        private Animator animator;
        private AudioSource audioSource;

        private void Awake() {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision other) {
            animator.SetTrigger("takeDamage");
            audioSource.Play();
        }
    }
}
