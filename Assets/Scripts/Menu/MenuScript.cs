using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MenuScript : MonoBehaviour
    {
        public void Start()
        {
            var startBtn = GameObject.FindWithTag("StartButton").GetComponent<Button>();
            startBtn.onClick.AddListener(() => { SceneManager.LoadScene("Scenes/RunScene"); });
            var scoreBtn = GameObject.FindWithTag("ScoreboardButton").GetComponent<Button>();
            scoreBtn.onClick.AddListener(() => { SceneManager.LoadScene("Scenes/ScoreboardScene"); });
            
            var musicToggle = GameObject.FindWithTag("MusicToggle").GetComponent<Toggle>();
            musicToggle.isOn = PlayerPrefs.GetInt("music", 1) == 1;
            musicToggle.onValueChanged.AddListener((val) => { PlayerPrefs.SetInt("music", val ? 1 : 0); });
        }
    }
}
