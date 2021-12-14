using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace menu
{
    public class MenuScript : MonoBehaviour
    {
        public void Start()
        {
            var startBtn = GameObject.FindWithTag("StartButton").GetComponent<Button>();
            startBtn.onClick.AddListener(() => { SceneManager.LoadScene("Scenes/BaseScene"); });
        }
    }
}
