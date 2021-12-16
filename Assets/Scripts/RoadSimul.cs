using System.Collections;
using System.Linq;
using UnityEngine;

public class RoadSimul : MonoBehaviour
{
    public Transform tileObj;
    private Vector3 _nextTileSpawn;

    public Transform railingObj;
    public Transform clothesObj;
    public Transform signObj;
    public Transform eggObj;
    public Transform bushObj;
    public Transform enemyObj;
    public Transform girlObj;
    public Transform lobollyObj;
    public Transform pillarObj;
    public Transform solidRailObj;

    private RoadLayoutGenerator _generator;

    [Range(0.2f, 5)] public float scale = 1;

    void Start()
    {
        _nextTileSpawn.x = 20;
        tileObj.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        _generator = new RoadLayoutGenerator(30, 40);

        StartCoroutine(SpawnTile());
    }

    //TODO not completed yet
    private IEnumerator SpawnTile()
    {
        yield return new WaitForSeconds(1);
        foreach (var _ in Enumerable.Range(0, 10))
        {
            var barriers = _generator.GetNextRow();
            var newTile = Instantiate(tileObj, _nextTileSpawn, tileObj.rotation);
            foreach (var (barrier, i) in barriers.Select((type, i) => (type, i)))
            {
                var nextObj = GetTransformFromType(barrier);
                if (nextObj != null)
                {
                    var blockSpawn = _nextTileSpawn;
                    blockSpawn.z = 5 + i - 1;
                    blockSpawn.y += nextObj.localPosition.y;
                    nextObj.localScale *= scale;
                    var o = Instantiate(nextObj, blockSpawn, nextObj.rotation, newTile);
                    nextObj.localScale /= scale;
                }
            }
            StartCoroutine(DisposeTile(newTile));
            _nextTileSpawn.x += 5;
        }
        StartCoroutine(SpawnTile());
    }

    private static IEnumerator DisposeTile(Component obj)
    {
        yield return new WaitForSeconds(10);
        // Destroy(obj.gameObject);
    }

    private Transform GetTransformFromType(RoadType type)
    {
        var random = Random.Range(0, 71);
        int r;
        switch (type)
        {
            case RoadType.Reachable:
                r = random % 5;
                switch (r)
                {
                    case 0:
                    case 1:
                        return null;
                    case 2:
                    case 3:
                    case 4:
                        return eggObj;
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
                        return lobollyObj;
                    case 2:
                        return railingObj;
                    case 3:
                        return solidRailObj;
                }
                break;
            case RoadType.theWhiper:
                r = random % 2;
                switch (r)
                {
                    case 0:
                        return enemyObj;
                    case 1:
                        return girlObj;
                }
                break;
        }
        return null;
    }
}