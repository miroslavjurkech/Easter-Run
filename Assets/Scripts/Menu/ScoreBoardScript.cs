using Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace menu
{
    public class ScoreBoardScript : MonoBehaviour
    {
        void Start()
        {
            var scores = ScoreManager.GetInstance().GetTopScores();

            var header = GameObject.FindWithTag("ScoreTableHeader").transform;
            var index = header.GetSiblingIndex();
            for (int i = 0; i < scores.Count; i++)
            {
                header.parent.GetChild(index + 1 + i).GetComponent<ScoreTableRecord>().Fill(scores[i]);
            }

            var backBtn = GameObject.FindWithTag("BackButton").GetComponent<Button>();
            backBtn.onClick.AddListener(() => { SceneManager.LoadScene("Scenes/MenuScene"); });
        }
    }
}
