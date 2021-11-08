using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public long points;
    
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(5, 0, 0);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     //spravanie zbierania vajec a pod
    //     Destroy(other.gameObject);
    // }

    public void IncHealth()
    {
        health += 1;
    }
    
    public void DecHealth()
    {
        health -= 1;
    }
}
