using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bottle : MonoBehaviour
{
    private GameObject _mainCamera;
    [SerializeField] private int effectTime;
    private AudioSource _sound;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
        _mainCamera = GameObject.FindWithTag("MainCamera");
        if (_mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Probably is not correctly tagged");
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && _mainCamera != null)
        {
            AudioSource.PlayClipAtPoint(_sound.clip, transform.position);
            DrunkCamera drunkCamera = _mainCamera.GetComponent<DrunkCamera>();
            drunkCamera.AddEfectTime(effectTime);
            drunkCamera.enabled = true;
            Destroy(gameObject);
            other.gameObject.GetComponent<Player>().Run();
        }
    }
}