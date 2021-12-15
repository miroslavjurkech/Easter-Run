using Behaviour;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
public class PlayerControls : MonoBehaviour
{
    private Transform player;
    private Animator anim;
    public bool isSwipeEnabled;
    
    void Start()
    {
        player = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        
#if UNITY_ANDROID
        isSwipeEnabled = true;
#elif UNITY_IOS
        isSwipeEnabled = true;
#else
        isSwipeEnabled = false;
#endif
    }
    
    void Update()
    {
        var pos = player.transform.position;
        //Debug.Log(SwipeDetector.swipeDirection.ToString());
        if (isSwipeEnabled)
        {
            Swipe(pos);
        }
        else
        {
            Buttons(pos);
        }
    }

    void Swipe(Vector3 pos)
    {
        var script = player.GetComponent<Player>();
        
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
                    anim.SetTrigger("jump");
                }
                break;
            case Behaviour.Swipe.Down:
                if (script.InFight)
                {
                    script.FightUsed = "DOWN";
                }
                else
                {
                    anim.SetTrigger("slide");
                }
                break;
            default:
                break;
        }
        player.transform.position = pos;
    }
    
    void Buttons(Vector3 pos)
    {
        var script = player.GetComponent<Player>();
        
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
                anim.SetTrigger("jump");
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
                anim.SetTrigger("slide");
            }
        }
        player.transform.position = pos;
    }
}
