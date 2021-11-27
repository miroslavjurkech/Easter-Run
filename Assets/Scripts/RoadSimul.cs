using System.Collections;
using UnityEngine;

public class RoadSimul : MonoBehaviour
{
    public Transform tileObj;
    private Vector3 _nextTileSpawn;
    public Transform railingObj;
    private Vector3 _nextRailingSpawn;
    private int _randZ;
    void Start()
    {
        _nextTileSpawn.x = 20;
        tileObj.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        StartCoroutine(SpawnTile());
    }

    private IEnumerator SpawnTile()
    {
        yield return new WaitForSeconds(1);
        _randZ = Random.Range(-1, 2);
        _nextRailingSpawn = _nextTileSpawn;
        _nextRailingSpawn.z = 5 + _randZ;
        _nextRailingSpawn.y = 0.6f;
        var newTile = Instantiate(tileObj, _nextTileSpawn, tileObj.rotation);
        var newRailing = Instantiate(railingObj, _nextRailingSpawn, railingObj.rotation);
        _nextTileSpawn.x += 5;
        StartCoroutine(SpawnTile());
        StartCoroutine(DisposeTile(newTile));
        StartCoroutine(DisposeTile(newRailing));
    }

    private static IEnumerator DisposeTile(Component obj)
    {
        yield return new WaitForSeconds(10);
        Destroy(obj.gameObject);
    }
}
