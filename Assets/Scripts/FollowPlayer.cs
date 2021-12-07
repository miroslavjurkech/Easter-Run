using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private Vector3 _initialOffset;
    private Vector3 _cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.Log("Player to follow must be set!");
        }

        _initialOffset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        _cameraPosition = player.position + _initialOffset;
        transform.position = _cameraPosition;
    }
}
