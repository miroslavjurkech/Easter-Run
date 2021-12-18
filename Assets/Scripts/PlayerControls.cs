using Behaviour;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
public class PlayerControls : MonoBehaviour
{
    private Transform _player;
    private Player _script;
    private Animator _anim;

    private bool _sideMove;
    private Swipe _sideMoveDirection;
    private float _sideMoveTarget;
    
    void Start()
    {
        _script = GetComponent<Player>();
        _player = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
        
        SwipeDetector.OnSwipe += OnSwipe;
        
    }
    
    private void FixedUpdate()
    {
        if (_sideMove)
        {
            var pos = _player.position;
            var delta = Time.deltaTime * _script.GetCurrentSpeed();

            if (Swipe.Right.Equals(_sideMoveDirection))
            {
                if (pos.z - delta <= _sideMoveTarget)
                {
                    pos.z = Mathf.Round(_sideMoveTarget);
                    _sideMove = false;
                    _sideMoveDirection = Swipe.None;
                }
                else
                {
                    pos.z -= delta;
                }
            } else if (Swipe.Left.Equals(_sideMoveDirection))
            {
                if (pos.z + delta >= _sideMoveTarget)
                {
                    pos.z = Mathf.Round(_sideMoveTarget);
                    _sideMove = false;
                    _sideMoveDirection = Swipe.None;
                }
                else
                {
                    pos.z += delta;
                }
            }

            _player.position = pos;
        }
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
            switch (dir)
            {
                case Swipe.Left:
                    if (_player.position.z < 5.9 && !_sideMove)
                    {
                        _sideMove = true;
                        _sideMoveDirection = dir;
                        _sideMoveTarget = _player.position.z + 1;
                    }

                    break;
                case Swipe.Right:
                    if (_player.position.z >= 4.9 && !_sideMove)
                    {
                        _sideMove = true;
                        _sideMoveDirection = dir;
                        _sideMoveTarget = _player.position.z - 1;
                    }

                    break;
                case Swipe.Up:
                    _anim.SetTrigger("jump");
                    break;
                case Swipe.Down:
                    _anim.SetTrigger("slide");
                    break;
            }
        }
    }
}
