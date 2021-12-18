using UnityEngine;

namespace Whipping
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class WhippedGirl : MonoBehaviour
    {
        private Animator _animator;
        private AudioSource _audioSource;

        private void Awake() {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision other) {
            _animator.SetTrigger("takeDamage");
            _audioSource.Play();
        }
    }
}
