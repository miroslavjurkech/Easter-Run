using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bottle : MonoBehaviour
{
    private GameObject mainCamera;
    [SerializeField] private int effectTime;
    private AudioSource sound;

    private void Awake() {
        sound = GetComponent<AudioSource>();
        mainCamera = GameObject.FindWithTag("MainCamera");
        if ( mainCamera == null )
        {
            Debug.LogError("Main Camera not found. Probably is not correctly tagged");
        }
    }


    private void OnCollisionEnter(Collision other) {
        if ( other.gameObject.tag == "Player" && mainCamera != null )
        {
            AudioSource.PlayClipAtPoint(sound.clip, transform.position);
            DrunkCamera drunkCamera = mainCamera.GetComponent<DrunkCamera>();
            drunkCamera.AddEfectTime(effectTime);
            drunkCamera.enabled = true;
            Destroy(gameObject);
            other.gameObject.GetComponent<Player>().Run();
        }
    }
}
