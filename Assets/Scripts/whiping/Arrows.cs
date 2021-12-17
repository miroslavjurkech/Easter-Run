using Behaviour;
using UnityEngine;
using UnityEngine.UI;

namespace whiping
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

        // Update is called once per frame
        void Update()
        {
        }
    }
}
