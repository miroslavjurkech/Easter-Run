using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

public class RoadSimul : MonoBehaviour
{
    public Transform tileObj;
    private Vector3 _nextTileSpawn;

    public Transform railingObj;
    public Transform clothesObj;
    public Transform signObj;
    public Transform[] eggObjs;
    public Transform bushObj;
    public Transform enemyObj;
    public Transform girlObj;
    public Transform puddleObj;
    public Transform pillarObj;
    public Transform solidRailObj;
    public Transform bottleObj;
    public Transform sandwichObj;

    private RoadLayoutGenerator _generator;
    [SerializeField] private float changeForLowBarier = 40.0f;
    [SerializeField] private float changeForHighBarier = 30.0f;
    [SerializeField] private uint nOfWhipersPer100Tiles = 10;
    [SerializeField] private int eggsNeededToGenerateGirl = 25;
    

    [Range(0.2f, 5)] public float scale = 1;

    private int eggsGenerated = 0;

    private void Awake() {
        if ( eggObjs.Length == 0 )
            Debug.LogError("No eggs to generate must be added in Game Editor");
    }

    void Start()
    {
        _nextTileSpawn.x = 10;
        tileObj.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        _generator = new RoadLayoutGenerator(changeForHighBarier, changeForLowBarier, nOfWhipersPer100Tiles);

        StartCoroutine(SpawnTile());
    }
    
    private IEnumerator SpawnTile()
    {
        yield return new WaitForSeconds(1);
        foreach (var _ in Enumerable.Range(0, 3))
        {
            var barriers = _generator.GetNextRow();
            var newTile = Instantiate(tileObj, _nextTileSpawn, tileObj.rotation, gameObject.transform);
            var objectsParent = newTile.Find("SpawnObjects");
            foreach (var (barrier, i) in barriers.Select((type, i) => (type, i - 1)))
            {
                var nextObj = GetTransformFromType(barrier);
                if (nextObj != null)
                {
                    var blockSpawn = _nextTileSpawn;
                    var isGirl = nextObj.Equals(girlObj);
                    if (isGirl)
                    {
                        blockSpawn.z = 5;
                        foreach (Transform child in objectsParent.transform) {
                            Destroy(child.gameObject);
                        }
                    }
                    else
                    {
                        blockSpawn.z = 5 + i;
                    }
                    blockSpawn.y += nextObj.localPosition.y;
                    nextObj.localScale *= scale;
                    var o = Instantiate(nextObj, blockSpawn, nextObj.rotation, objectsParent);
                    nextObj.localScale /= scale;
                    if (isGirl)
                    {
                        break;
                    }
                }
            }
            _nextTileSpawn.x += 5;
        }
        StartCoroutine(SpawnTile());
    }

    private Transform GetTransformFromType(RoadType type)
    {
        var random = Random.Range(0, 71);
        int r;
        switch (type)
        {
            case RoadType.Reachable:
                r = random % 9;
                switch (r)
                {
                    case 0:
                    case 1:
                    case 2:
                        return null;
                    case 3:
                        return bottleObj;
                    case 4:
                        return eggsGenerated % 5 == 0 ? sandwichObj : null;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        if ((eggsGenerated % eggsNeededToGenerateGirl) == 0 && (eggsGenerated > 0))
                        {
                            Interlocked.Increment(ref eggsGenerated);
                            return girlObj;
                        }
                        Interlocked.Increment(ref eggsGenerated);
                        int variantType = Random.Range(0,eggObjs.Length);
                        return eggObjs[variantType];
                }
                break;
            case RoadType.BarierHigh:
                r = random % 3;
                switch (r)
                {
                    case 0:
                        return bushObj;
                    case 1:
                        return pillarObj;
                    case 2:
                        return signObj;
                }
                break;
            case RoadType.BarierLow:
                r = random % 4;
                switch (r)
                {
                    case 0:
                        return clothesObj;
                    case 1:
                        return puddleObj;
                    case 2:
                        return railingObj;
                    case 3:
                        return solidRailObj;
                }
                break;
            case RoadType.TheWhiper:
                return enemyObj;
        }
        return null;
    }
}