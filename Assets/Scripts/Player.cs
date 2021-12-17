using System;
using UnityEngine;
using System.Collections;
using Behaviour;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody _rigidbody;
    
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

    private Swipe FightExpected { get; set; }
    public Swipe FightUsed  { get; set; }
    
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate OnHealthChangedCallback;

    public int numOfClearTilesAfterFall = 2;
    
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        
        GameState state = GameState.GetInstance();

        if (state.GetEggs() != 0)
        {
            points = state.GetEggs();
            health = state.GetLives();
        }
        else
        {
            state.NewGame();
        }

        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds (startAfter);
        _anim.SetBool("idle", false);
        Run();
    }

    public void Run()
    {
        var actPointsInc = Math.Min(this.points, speedUpUntilPoints);
        _rigidbody.velocity = new Vector3(speed + ((maxSpeed - speed) * actPointsInc / speedUpUntilPoints), 0, 0);
    }

    public void Stop()
    {
        _rigidbody.velocity = new Vector3( 0, 0, 0 );
        _anim.SetTrigger("stop");
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
        Debug.Log("Kick");
            _anim.SetTrigger("kick");
    }

    public void GetHit()
    {
        _anim.SetTrigger("hit");
        DecPoints();
        
        LeaveFight();
    }

    public void EnterFight(Swipe expected)
    {
        _anim.SetTrigger("cancel");
        FightExpected = expected;
        FightUsed = Swipe.None;
        InFight = true;
    }

    public void CancelCancel()
    {
        _anim.ResetTrigger("cancel");
    }

    public void LeaveFight()
    {
        FightExpected = Swipe.None;
        FightUsed = Swipe.None;
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
        }
        else
        {
            points = 0;
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
        
        _anim.SetBool("idle", true);
        _anim.SetTrigger("idleAfterFall");
        _anim.ResetTrigger("cancel");
        
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
            SaveState();
            GameObject.FindWithTag("GameController").GetComponent<RoadSimul>().StopAllCoroutines();
            SceneManager.LoadScene("Scenes/GameOverScene");
        }
    }

    public void SaveState()
    {
        GameState.GetInstance().SaveState(points, health);
    }
}
