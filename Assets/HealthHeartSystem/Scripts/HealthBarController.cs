/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;

//asset from assetStore but script changed to map better to our use case
//credit to OArielG https://assetstore.unity.com/publishers/34878
public class HealthBarController : MonoBehaviour
{
    [SerializeField] 
    private float heartScale;
    
    private GameObject[] _heartContainers;
    private Image[] _heartFills;
    private Player _player;

    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        
        Debug.Log("initialize: " + _player.maxHealth);
        // Should I use lists? Maybe :)
        _heartContainers = new GameObject[_player.maxHealth];
        _heartFills = new Image[_player.maxHealth];

        _player.OnHealthChangedCallback += UpdateHeartsHUD;
        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetFilledHearts();
    }

    void SetFilledHearts()
    {
        for (var i = 0; i < _heartFills.Length; i++)
        {
            //Debug.Log("Iterujem: " + i);
            _heartFills[i].fillAmount =  i >= _player.maxHealth - _player.health ? 1 : 0;
        }
    }

    void InstantiateHeartContainers()
    {
        for (var i = 0; i < _player.maxHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab, heartsParent, false);
            temp.transform.localScale = new Vector3(heartScale, heartScale, heartScale);
            _heartContainers[i] = temp;
            _heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
