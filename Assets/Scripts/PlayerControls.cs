using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public enum SwipeDirection { Up, Down, Left, Right };

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
public class PlayerControls : MonoBehaviour
{
    private Transform _player;
    private Player _script;
    private Animator _anim;
    private bool _isSwipeEnabled;

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
        var pos = _player.position;
        
        if (_isSwipeEnabled)
        {
            Swipe(pos);
        } else
        {
            Buttons(pos);
        }
    }

    void Swipe(Vector3 pos)
    {
        if (Input.touches.Length > 0)
        {
            var t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                _firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (_firstPressPos != null && t.phase == TouchPhase.Moved)
            {
                _secondPressPos = new Vector2(t.position.x, t.position.y);
                _currentSwipe = new Vector3(_secondPressPos.x - _firstPressPos.Value.x, _secondPressPos.y - _firstPressPos.Value.y);
                
                if (_currentSwipe.magnitude < minSwipeLength) {
                    return;
                }
                
                _currentSwipe.Normalize();
                
                // Swipe up
                if (_currentSwipe.y > 0 &&  _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f) {
                    OnSwipe(pos, SwipeDirection.Up);
                    // Swipe down
                } else if (_currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f) {
                    OnSwipe(pos, SwipeDirection.Down);
                    // Swipe right
                } else if (_currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f) {
                    OnSwipe(pos, SwipeDirection.Right);
                    // Swipe left 
                } else if (_currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f) {
                    OnSwipe(pos, SwipeDirection.Left);
                }

                _firstPressPos = null;
            }
        }
    }

    void OnSwipe(Vector3 pos, SwipeDirection dir)
    {
        switch (dir)
        {
            case SwipeDirection.Left:
                if (_script.InFight)
                {
                    //FIXME je nachuja (asi) kamera a na pohyb je to obratene
                    _script.FightUsed = "RIGHT";
                }
                else
                {
                    if (pos.z > 5)
                    {
                        pos.z -= 1;
                    }
                }
                break;
            case SwipeDirection.Right:
                if (_script.InFight)
                {
                    //FIXME je nachuja (asi) kamera a na pohyb je to obratene
                    _script.FightUsed = "LEFT";
                }
                else
                {
                    if (pos.z < 6)
                    {
                        pos.z += 1;
                    }
                }
                break;
            case SwipeDirection.Up:
                if (_script.InFight)
                {
                    Debug.Log("Fightim UP");
                    _script.FightUsed = "UP";
                }
                else
                {
                    //Jump is trigger on the playerAnimationScript
                    _anim.SetTrigger("jump");
                }
                break;
            case SwipeDirection.Down:
                if (_script.InFight)
                {
                    Debug.Log("Fightim DOWN");
                    _script.FightUsed = "DOWN";
                }
                else
                {
                    _anim.SetTrigger("slide");
                }
                break;
        }
        
        _player.transform.position = pos;
    }

    private void Buttons(Vector3 pos)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_script.InFight)
            {
                _script.FightUsed = "RIGHT";
            }
            else
            {
                if (pos.z > 5)
                {
                    pos.z -= 1;
                }
            }
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_script.InFight)
            {
                _script.FightUsed = "LEFT";
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
            if (_script.InFight)
            {
                _script.FightUsed = "UP";
            }
            else
            {
                //Jump is trigger on the playerAnimationScript
                _anim.SetTrigger("jump");
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_script.InFight)
            {
                _script.FightUsed = "DOWN";
            }
            else
            {
                _anim.SetTrigger("slide");
            }
        }
        
        _player.transform.position = pos;
    }
}
