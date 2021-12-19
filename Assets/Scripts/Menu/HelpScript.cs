using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class HelpScript : MonoBehaviour
    {
        private void Start()
        {
            var btn = GameObject.FindWithTag("BackButton").GetComponent<Button>();
            btn.onClick.AddListener(() => {SceneManager.LoadScene("Scenes/MenuScene");});
        }
    }
}