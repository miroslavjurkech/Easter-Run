using Behaviour;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
public class PlayerControls : MonoBehaviour
{
    private Transform _player;
    private Player _script;
    private Animator _anim;

    [FormerlySerializedAs("Camera")] 
    public Camera mainCamera;
    
    public float minSwipeLength = 15f;
    private Vector2? _firstPressPos;
    private Vector2 _secondPressPos;
    private Vector2 _currentSwipe;
    
    void Start()
    {
        _script = GetComponent<Player>();
        _player = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
        
        SwipeDetector.OnSwipe += OnSwipe;
        
    }
    void OnDestroy(){
        SwipeDetector.OnSwipe -= OnSwipe;
    }

    void OnSwipe(Swipe dir)
    {
        if (Swipe.None.Equals(dir))
            return;

        if (_script.InFight)
        {
            _script.FightUsed = dir;
        }
        else
        {
            Vector3 pos = _player.position;
            
            switch (dir)
            {
                case Swipe.Left:
                    if (pos.z < 6)
                    {
                        pos.z += 1;
                    }

                    break;
                case Swipe.Right:
                    if (pos.z >= 5)
                    {
                        pos.z -= 1;
                    }

                    break;
                case Swipe.Up:
                    _anim.SetTrigger("jump");
                    break;
                case Swipe.Down:
                    _anim.SetTrigger("slide");
                    break;
            }
            
            _player.position = pos;
        }
    }
}
