using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace menu
{
    public class GameOverScript : MonoBehaviour
    {
        // Start is called before the first frame update
        public void Start()
        {
            var points = GameObject.FindWithTag("PointsInfo").GetComponent<Text>();
            //points.text = points.text + " " + PlayerPrefs.GetString("points");
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
