using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("Startol som GameOverScript");

        var points = GameObject.FindWithTag("PointsInfo").GetComponent<Text>();
        points.text = points.text + " " + PlayerPrefs.GetString("points");
        
        var startBtn = GameObject.FindWithTag("BackToMenuButton").GetComponent<Button>();
        
        Debug.Log("Mam btn");
        Debug.Log(startBtn.name);
        
        startBtn.onClick.AddListener(() =>
        {
            Debug.Log("Klikol si na back to menu btn");
            SceneManager.LoadScene("Scenes/MenuScene");
        });
    }
}
