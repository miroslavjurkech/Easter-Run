using System;
using UnityEditor.UIElements;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rigidbody;
    
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
        rigidbody = GetComponent<Rigidbody>();

        StartCoroutine("WaitForStart");
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
        anim.SetBool("idle", false);
        Run();
    }

    public void Run()
    {
        rigidbody.velocity = new Vector3( speed, 0, 0);
    }

    public void Stop()
    {
        rigidbody.velocity = new Vector3( 0, 0, 0 );
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

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag != "Enemy")
            return;

        Stop();
        anim.SetTrigger("kick");
        //Run is again trigger from animation state machine after kick finishes
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
