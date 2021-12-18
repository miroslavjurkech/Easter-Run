using System;
using UnityEngine;
using System.Collections;
using Behaviour;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody _rigidbody;
    private Camera _mainCamera;
    private AudioSource _audio;
    
    public int maxHealth = 3;

    [Header("Idle for given time in second and then run")]
    public float startAfter = 2f;
    
    public int health = 3;
    public int points = 0;

    [SerializeField]
    private float speed = 4f;

    [SerializeField]
    private float animSpeedMultiplierAddition = 0.1f;
    
    [SerializeField]
    private float animSpeedMultiplierMax = 1.7f;

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
        _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _audio = GetComponent<AudioSource>();
        
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

    public float GetCurrentSpeed()
    {
        return _rigidbody.velocity.x;
    }

    public void Run()
    {
        var actPointsInc = Math.Min(this.points, speedUpUntilPoints);

        var newSpeed = speed + ((maxSpeed - speed) * actPointsInc / speedUpUntilPoints);

        if (_anim.GetFloat("speedMultiplier") < animSpeedMultiplierMax && Mathf.Floor(newSpeed) - Mathf.Floor(_rigidbody.velocity.x) >= 1)
        {
            _anim.SetFloat("speedMultiplier", _anim.GetFloat("speedMultiplier") + animSpeedMultiplierAddition);
        }

        _rigidbody.velocity = new Vector3(newSpeed, 0, 0);
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
        _audio.Play();
        _anim.SetTrigger("hit");
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

    private IEnumerator AfterHit(Transform tile)
    {
        _mainCamera.clearFlags = CameraClearFlags.SolidColor;
        _mainCamera.backgroundColor = Color.black;
        _mainCamera.cullingMask = 0;
        
        _anim.SetBool("idle", true);
        _anim.SetTrigger("idleAfterFall");
        _anim.ResetTrigger("cancel");
        
        ClearNearbyTiles(tile);

        yield return new WaitForSeconds(0.5f);

        _mainCamera.clearFlags = CameraClearFlags.Skybox;
        _mainCamera.cullingMask = 1;

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
                    StartCoroutine(AfterHit(tile));
                }
            }
            else
            {
                _audio.Play();
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
