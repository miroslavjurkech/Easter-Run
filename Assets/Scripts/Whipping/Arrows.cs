using UnityEngine;

namespace Whipping
{
    public class Arrows : MonoBehaviour
    {
        public Swipe direction;
        
        private Rigidbody _rigidbody;
    
        [SerializeField]
        private float speed;
    
    
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = new Vector3( speed, 0, 0);
        }
    }
}
