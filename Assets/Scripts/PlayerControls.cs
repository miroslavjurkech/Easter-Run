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
        switch (SwipeDetector.swipeDirection)
        {
            case Behaviour.Swipe.Left:
                if (pos.z == 5)
                {
                    pos.z += 1;
                }
                break;
            case Behaviour.Swipe.Right:
                if (pos.z == 5f)
                {
                    pos.z -= 1;
                }
                break;
            default:
                break;
        }
        player.transform.position = pos;
    }
    
    void Buttons(Vector3 pos)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (pos.z > 4)
            {
                pos.z -= 1;
            }
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (pos.z < 6)
            {
                pos.z += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
            //Jump is trigger on the playerAnimationScript
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
        }
        player.transform.position = pos;
    }
}
