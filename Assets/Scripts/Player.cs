using System;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    
    public int health;
    public long points;

    [SerializeField]
    private float speed;
    
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate OnHealthChangedCallback;
    
    void Start()
    {
        Run();
    }

    private void Update()
    {
        //Debug.Log(GetComponent<Rigidbody>().velocity);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     //spravanie zbierania vajec a pod
    //     Destroy(other.gameObject);
    // }

    public void Run()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
    }
    
    public void IncHealth()
    {
        if (health < maxHealth)
        {
            health += 1;

            if (OnHealthChangedCallback != null)
            {
                OnHealthChangedCallback.Invoke();
            }
        }
    }
    
    public void DecHealth()
    {
        if (health > 0)
        {
            health -= 1;

            if (OnHealthChangedCallback != null)
            {
                OnHealthChangedCallback.Invoke();
            }
        }
    }

    public void IncPoints(long amount = 1)
    {
        points += amount;
    }

    public void DecPoints(long amount = 1)
    {
        points += amount;
    }
}
