using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GirlInRun : MonoBehaviour
{
    private bool _exited = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerSpine"))
            return;

        if (_exited) return;
        
        var script = GameObject.FindWithTag("Player").GetComponent<Player>();

        script.SaveState();
        script.Stop();
            
        StartCoroutine(SwitchScene());
    }

    private IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Scenes/WhippingScene");
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("PlayerSpine"))
            return;
        
        _exited = true;
    }
}
