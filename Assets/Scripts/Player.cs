using System;
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
    public int points;

    [SerializeField]
    private float speed;

    public bool InFight { get; private set; }

    private string FightExpected { get; set; }
    public string FightUsed  { get; set; }
    
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate OnHealthChangedCallback;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        StartCoroutine("WaitForStart");
    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds (startAfter);
        anim.SetBool("idle", false);
        Run();
    }

    public void Run()
    {
        var points = Math.Min(this.points, 20);
        rigidbody.velocity = new Vector3(speed + (speed*points/20), 0, 0);
    }

    public void Stop()
    {
        rigidbody.velocity = new Vector3( 0, 0, 0 );
        anim.SetTrigger("stop");
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

    public void Kick()
    {
            anim.SetTrigger("kick");
    }

    public void GetHit()
    {
        anim.SetTrigger("hit");
        DecPoints();
        
        LeaveFight();
    }

    public void EnterFight(string expected)
    {
        anim.SetTrigger("cancel");
        FightExpected = expected;
        InFight = true;
    }

    public void CancelCancel()
    {
        anim.ResetTrigger("cancel");
    }

    public void LeaveFight()
    {
        FightExpected = null;
        FightUsed = null;
        InFight = false;
    }

    public bool IsSuccessfulFight()
    {
        Debug.Log("FIGHT");
        Debug.Log("Should be: " + FightExpected);
        Debug.Log("Is: " + FightUsed);
        return InFight && FightExpected.Equals(FightUsed);
    }

    public void IncPoints(int amount = 1)
    {
        points += amount;
        Run();
    }

    public void DecPoints(int amount = 1)
    {
        if (points >= amount)
        {
            points -= amount;
            Run();
        }
    }
}
