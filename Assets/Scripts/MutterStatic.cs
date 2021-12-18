using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MutterStatic : MonoBehaviour
{
    private void Awake() {
        AudioListener.volume = ( PlayerPrefs.GetInt("music", 1) == 1 ) ? 1.0f : 0.0f;
    }
}