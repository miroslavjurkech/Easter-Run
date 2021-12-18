using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutterDynamic : MonoBehaviour
{
    private void Update() {
        AudioListener.volume = ( PlayerPrefs.GetInt("music", 1) == 1 ) ? 1.0f : 0.0f;
    }
}
