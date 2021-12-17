using UnityEngine;

namespace Whipping
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
            Debug.Log("Should take damage");
            animator.SetTrigger("takeDamage");
            audioSource.Play();
        }
    }
}
