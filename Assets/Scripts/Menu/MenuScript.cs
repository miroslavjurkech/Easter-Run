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
            startBtn.onClick.AddListener(() => {
                Destroy( GameObject.FindGameObjectWithTag("MenuMusic")); 
                SceneManager.LoadScene("Scenes/RunScene"); 
                });
            var scoreBtn = GameObject.FindWithTag("ScoreboardButton").GetComponent<Button>();
            scoreBtn.onClick.AddListener(() => { SceneManager.LoadScene("Scenes/ScoreboardScene"); });
            var helpBtn = GameObject.FindWithTag("HelpButton").GetComponent<Button>();
            helpBtn.onClick.AddListener(() => { SceneManager.LoadScene("Scenes/HelpScene"); });
            
            var musicToggle = GameObject.FindWithTag("MusicToggle").GetComponent<Toggle>();
            musicToggle.isOn = PlayerPrefs.GetInt("music", 1) == 1;
            musicToggle.onValueChanged.AddListener((val) => { PlayerPrefs.SetInt("music", val ? 1 : 0); });
            
            var hardDrunkToggle = GameObject.FindWithTag("HardDrunkToggle").GetComponent<Toggle>();
            hardDrunkToggle.isOn = PlayerPrefs.GetInt("hardDrunkMode", 0) == 1;
            hardDrunkToggle.onValueChanged.AddListener((val) => { PlayerPrefs.SetInt("hardDrunkMode", val ? 1 : 0); });
        }
    }
}
