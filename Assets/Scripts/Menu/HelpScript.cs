using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class HelpScript : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Scenes/MenuScene");
            }
        }
    }
}