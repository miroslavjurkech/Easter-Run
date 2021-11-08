using System.Collections;
using UnityEngine;

public class RoadSimul : MonoBehaviour
{
    public Transform tileObj;
    private Vector3 _nextTileSpawn;
    
    void Start()
    {
        _nextTileSpawn.x = 20;
        tileObj.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(SpawnTile());
    }

    private IEnumerator SpawnTile()
    {
        yield return new WaitForSeconds(1);
        var obj = Instantiate(tileObj, _nextTileSpawn, tileObj.rotation);
        _nextTileSpawn.x += 5;
        StartCoroutine(SpawnTile());
        StartCoroutine(DisposeTile(obj));
    }

    private IEnumerator DisposeTile(Transform obj)
    {
        yield return new WaitForSeconds(10);
        Destroy(obj.gameObject);
    }
}
