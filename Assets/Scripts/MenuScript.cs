using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public void Start()
    {
        var startBtn = GameObject.FindWithTag("StartButton").GetComponent<Button>();
        startBtn.onClick.AddListener(() => { SceneManager.LoadScene("Scenes/BaseScene"); });
    }
}
