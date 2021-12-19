using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private Vector3 _initialOffset;
    private Vector3 _cameraPosition;
    private bool _isShake;
    
    void Start()
    {
        if (player == null)
        {
            Debug.Log("Player to follow must be set!");
        }

        _initialOffset = transform.position - player.position;
    }

    void Update()
    {
        if (!_isShake)
        {
            _cameraPosition = player.position + _initialOffset;
            transform.position = _cameraPosition;
        }
    }

    public void ShakeCamera()
    {
        StartCoroutine(RandomizeCameraPosition(1, 1));
    }
    
    private IEnumerator RandomizeCameraPosition(float duration, float magnitude)
    {
        _isShake = true;
        var elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            var factor = Random.Range(-1f, 1f) * magnitude;
            
            var originalPosition = player.position + _initialOffset;
            transform.position = new Vector3(originalPosition.x + factor, originalPosition.y, originalPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _isShake = false;
    }
}
