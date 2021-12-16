using UnityEngine;


public enum BoostType
{
    Bottle,
    Sandwich
}
public class Boost : MonoBehaviour
{
    public BoostType type;
    
    private void OnCollisionEnter(Collision other) {
        Debug.Log("You gained boost: " + type);

        if (!other.gameObject.tag.Equals("Player")) return;
        
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();

        switch (type)
        {
            case BoostType.Sandwich:
                player.IncHealth();
                break;
            case BoostType.Bottle:
                // TODO
                break;
        }
        
        Destroy(gameObject);
        player.Run();
    }
}
