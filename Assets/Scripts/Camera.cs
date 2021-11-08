using Behaviour;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public bool isSwipeEnabled;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(5, 0, 0);
    }
    
    void Update()
    {
        var pos = player.transform.position;
        Debug.Log(SwipeDetector.swipeDirection.ToString());
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
