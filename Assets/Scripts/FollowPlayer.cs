using System.Collections;
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

    public void ShakeCamera()
    {
        StartCoroutine(RandomizeCameraPosition(1, 1));
    }
    
    private IEnumerator RandomizeCameraPosition(float duration, float magnitude)
    {
        var originalPosition = transform.position;
        var elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            float x = Random.Range(-2f, 2f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
 
            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
