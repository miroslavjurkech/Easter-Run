using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class GameOverScript : MonoBehaviour
    {
        public void Start()
        {
            var points = GameObject.FindWithTag("PointsInfo").GetComponent<Text>();
            points.text = points.text + " " + GameState.GetInstance().GetEggs();
        
            var startBtn = GameObject.FindWithTag("BackToMenuButton").GetComponent<Button>();

            startBtn.onClick.AddListener(() =>
            {
                GameState.GetInstance().ClearState();
                SceneManager.LoadScene("Scenes/MenuScene");
            });
        }
    }
}
