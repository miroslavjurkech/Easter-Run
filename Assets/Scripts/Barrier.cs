using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrier : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Debug.Log("You collided with barrier");

        if (!other.gameObject.tag.Equals("Player")) return;
        
        var player = other.gameObject.GetComponent<Player>();

        player.DecHealth();

        if (player.health > 0)
        {
            Destroy(gameObject);
            player.Run();
        }
        else
        {
            //TODO end game?
            GameObject.FindWithTag("GameController").GetComponent<RoadSimul>().StopAllCoroutines();
            PlayerPrefs.SetString("points",player.points.ToString());
            SceneManager.LoadScene("Scenes/GameOverScene");
        }
    }
}
