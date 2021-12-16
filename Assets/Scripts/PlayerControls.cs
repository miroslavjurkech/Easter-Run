using Behaviour;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
public class PlayerControls : MonoBehaviour
{
    private Transform _player;
    private Animator _anim;
    private bool _isSwipeEnabled;
    
    void Start()
    {
        _player = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
        
#if UNITY_ANDROID
        _isSwipeEnabled = true;
#elif UNITY_IOS
        _isSwipeEnabled = true;
#else
        _isSwipeEnabled = false;
#endif
    }
    
    void Update()
    {
        var pos = _player.transform.position;
        //Debug.Log(SwipeDetector.swipeDirection.ToString());
        if (_isSwipeEnabled)
        {
            Swipe(pos);
        }
        else
        {
            Buttons(pos);
        }
    }

    private void Swipe(Vector3 pos)
    {
        var script = _player.GetComponent<Player>();
        
        switch (SwipeDetector.swipeDirection)
        {
            case Behaviour.Swipe.Left:
                if (script.InFight)
                {
                    script.FightUsed = "LEFT";
                }
                else
                {
                    if (pos.z > 4)
                    {
                        pos.z -= 1;
                    }
                }
                break;
            case Behaviour.Swipe.Right:
                if (script.InFight)
                {
                    script.FightUsed = "RIGHT";
                }
                else
                {
                    if (pos.z < 6)
                    {
                        pos.z += 1;
                    }
                }
                break;
            case Behaviour.Swipe.Up:
                if (script.InFight)
                {
                    script.FightUsed = "UP";
                }
                else
                {
                    //Jump is trigger on the playerAnimationScript
                    _anim.SetTrigger("jump");
                }
                break;
            case Behaviour.Swipe.Down:
                if (script.InFight)
                {
                    script.FightUsed = "DOWN";
                }
                else
                {
                    _anim.SetTrigger("slide");
                }
                break;
            default:
                break;
        }
        _player.transform.position = pos;
    }

    private void Buttons(Vector3 pos)
    {
        var script = _player.GetComponent<Player>();
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (script.InFight)
            {
                script.FightUsed = "RIGHT";
            }
            else
            {
                if (pos.z > 4)
                {
                    pos.z -= 1;
                }
            }
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (script.InFight)
            {
                script.FightUsed = "LEFT";
            }
            else
            {
                if (pos.z < 6)
                {
                    pos.z += 1;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (script.InFight)
            {
                script.FightUsed = "UP";
            }
            else
            {
                //Jump is trigger on the playerAnimationScript
                _anim.SetTrigger("jump");
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (script.InFight)
            {
                script.FightUsed = "DOWN";
            }
            else
            {
                _anim.SetTrigger("slide");
            }
        }
        _player.transform.position = pos;
    }
}
