using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private void Awake() {
        GameObject[] others = GameObject.FindGameObjectsWithTag("MenuMusic");

        if ( others.Length > 1 )
        {
            Destroy( gameObject );
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
