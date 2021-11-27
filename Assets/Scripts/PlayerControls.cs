using Behaviour;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Transform player;
    private Vector3 _initialOffset;
    private Vector3 _cameraPosition;
    public bool isSwipeEnabled;
    
    void Start()
    {
        _initialOffset = transform.position - player.position;
    }

    void FixedUpdate()
    {
        _cameraPosition = player.position + _initialOffset;
        transform.position = _cameraPosition;
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
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (pos.y < 1)
            {
                pos.y += 1;
                // wait
                pos.y -= 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
        }
        player.transform.position = pos;
    }
}
