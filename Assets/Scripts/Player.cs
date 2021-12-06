using System;
using UnityEditor.UIElements;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private Animator anim;
    
    public int maxHealth;

    [Header("Idle for given time in second and then run")]
    public float startAfter;
    
    public int health;
    public long points;

    [SerializeField]
    private float speed;
    
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate OnHealthChangedCallback;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("WaitForStart");
        //Run();
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

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds (startAfter);
        Run();
        //anim.SetTrigger("stopIdle");
    }

    public void Run()
    {
        anim.SetBool("idle", false);
        GetComponent<Rigidbody>().velocity = new Vector3( speed, 0, 0);
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
