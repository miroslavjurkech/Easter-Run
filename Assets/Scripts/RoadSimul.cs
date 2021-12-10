using System.Collections;
using UnityEngine;

public class RoadSimul : MonoBehaviour
{
    public Transform tileObj;
    private Vector3 _nextTileSpawn;
    
    public Transform railingObj;
    private Vector3 _nextRailingSpawn;

    public Transform clothesObj;
    private Vector3 _nextClothesSpawn;

    public Transform signObj;
    private Vector3 _nextSignSpawn;
    
    public Transform eggObj;
    private Vector3 _nextEggSpawn;
    
    private int _randZ;
    private int _randChoice;
    
    [Range(0.1f, 2)]
    public float scale = 1;
    
    void Start()
    {
        _nextTileSpawn.x = 20;
        tileObj.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        railingObj.localScale = railingObj.localScale * scale;
        clothesObj.localScale = clothesObj.localScale * scale;
        signObj.localScale = signObj.localScale * scale;
        eggObj.localScale = eggObj.localScale * scale;
        StartCoroutine(SpawnTile());
    }

    //TODO not completed yet
    private IEnumerator SpawnTile()
    {
        yield return new WaitForSeconds(1);
        _randZ = Random.Range(-1, 2);
        _nextRailingSpawn = _nextTileSpawn;
        _nextRailingSpawn.z = 5 + _randZ;
        _nextRailingSpawn.y = 0.7f;
        var newTile = Instantiate(tileObj, _nextTileSpawn, tileObj.rotation);
        var newBlock = Instantiate(railingObj, _nextRailingSpawn, railingObj.rotation);
        StartCoroutine(DisposeTile(newTile));
        StartCoroutine(DisposeTile(newBlock));
        
        
        _nextTileSpawn.x += 5;
        _randZ = Random.Range(-1, 2);
        _nextClothesSpawn.x = _nextTileSpawn.x;
        _nextClothesSpawn.y = 1.2f;
        _nextClothesSpawn.z = 5 + _randZ;
        newTile = Instantiate(tileObj, _nextTileSpawn, tileObj.rotation);
        newBlock = Instantiate(clothesObj, _nextClothesSpawn, clothesObj.rotation);
        StartCoroutine(DisposeTile(newTile));
        StartCoroutine(DisposeTile(newBlock));

        _randZ = _randZ switch
        {
            0 => 1,
            1 => -1,
            _ => 0
        };

        _nextSignSpawn.x = _nextTileSpawn.x;
        _nextSignSpawn.y = 0.6f;
        _nextSignSpawn.z = 5 + _randZ;
        newBlock = Instantiate(signObj, _nextSignSpawn, signObj.rotation);
        StartCoroutine(DisposeTile(newBlock));
        
        _randZ = _randZ switch
        {
            1 => -1,
            -1 => 0,
            _ => 1
        };
        
        _nextEggSpawn.x = _nextTileSpawn.x;
        _nextEggSpawn.y = 0.6f;
        _nextEggSpawn.z = 5 + _randZ;
        newBlock = Instantiate(eggObj, _nextEggSpawn, eggObj.rotation);
        StartCoroutine(DisposeTile(newBlock));
        
        
        _nextTileSpawn.x += 5;
        StartCoroutine(SpawnTile());
        
    }

    private static IEnumerator DisposeTile(Component obj)
    {
        yield return new WaitForSeconds(20);
        //Destroy(obj.gameObject);
    }

    // private Transform RandomGenerate()
    // {
    //     var r = Random()
    // }
}
