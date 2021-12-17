using Behaviour;
using UnityEngine;

namespace Whipping
{
    public class Arrows : MonoBehaviour
    {
        public Swipe direction;
        
        private Rigidbody _rigidbody;
    
        [SerializeField]
        private float speed;
    
    
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = new Vector3( speed, 0, 0);
        }
    }
}
