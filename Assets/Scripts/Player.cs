using System;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

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

    public int speedUpUntilPoints = 40;

    public float maxSpeed = 8;

    public bool InFight { get; private set; }

    private string FightExpected { get; set; }
    public string FightUsed  { get; set; }
    
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate OnHealthChangedCallback;

    public GameObject CurrentTile { get; private set; }
    
    public int numOfClearTilesAfterFall = 2;
    
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
        var actPointsInc = Math.Min(this.points, speedUpUntilPoints);
        rigidbody.velocity = new Vector3(speed + ((maxSpeed - speed)*actPointsInc/speedUpUntilPoints), 0, 0);
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

    private void AfterHit(Transform tile)
    {
        var mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        
        StartCoroutine(FlashScreen(mainCamera, tile));
    }
    
    private IEnumerator FlashScreen(Camera mainCamera, Transform tile)
    {
        mainCamera.clearFlags = CameraClearFlags.SolidColor;
        mainCamera.backgroundColor = Color.black;
        mainCamera.cullingMask = 0;
        
        anim.SetBool("idle", true);
        anim.SetTrigger("idleAfterFall");
        anim.ResetTrigger("cancel");
        
        ClearNearbyTiles(tile);

        yield return new WaitForSeconds(0.5f);

        mainCamera.clearFlags = CameraClearFlags.Skybox;
        mainCamera.cullingMask = 1;

        StartCoroutine(WaitForStart());
    }

    private void ClearNearbyTiles(Transform currentTile)
    {
        var index = currentTile.GetSiblingIndex();
        
        for (var i = 0; i < numOfClearTilesAfterFall + 1; i++)
        {
            var tile = currentTile.parent.GetChild(index + i).transform;
            Destroy(tile.Find("SpawnObjects").gameObject);
        }
    }

    public void HitBarrier(GameObject o, Transform tile = null)
    {
        DecHealth();

        if (health > 0)
        {
            if (o.CompareTag("Enemy"))
            {
                if (tile != null)
                {
                    AfterHit(tile);
                }
            }
            else
            {
                Destroy(o);
                Run();
            }
        }
        else
        {
            //TODO end game?
            GameObject.FindWithTag("GameController").GetComponent<RoadSimul>().StopAllCoroutines();
            PlayerPrefs.SetString("points", points.ToString());
            SceneManager.LoadScene("Scenes/GameOverScene");
        }
    }
}
