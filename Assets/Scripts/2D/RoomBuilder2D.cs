using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder2D : MonoBehaviour
{
    [SerializeField] Vector2Int _worldSize;
    [SerializeField] public static Vector2Int _dimensions;

    public static float BLOCK_SIZE = 2f;

    [SerializeField] GameObject _blockPrefab;
    [SerializeField] bool _animateSpawn = false;
    [SerializeField]float _timeBtwSpawn = 0.001f;

    [SerializeField] Vector3 _spawnSize = new Vector3(.1f , .1f , .1f);

    [SerializeField] Block [,] _blocks;
    

    [SerializeField][Range(0, 100)] int _skipPercent = 50;

    private FPSController _player;

    void Awake(){
    }
    private void Start() {
        BuildWorld();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            //BuildWorld();
        }

        if (Input.GetKeyDown(KeyCode.P)){
            GetRandomBlockName();
        }
    }

    public void BuildWorld(){
        StopAllCoroutines();
        StartCoroutine(BuildWorldRoutine());
    }

    IEnumerator BuildWorldRoutine(){

        ClearTransform();
        _dimensions = _worldSize;

        Transform RowHolder = null;

        _blocks = new Block[_dimensions.x, _dimensions.y];

        for (int x = 0; x < _dimensions.x; x++)
        {

            RowHolder = new GameObject("Row : " + x).transform;
            RowHolder.parent = transform;
            RowHolder.localPosition = Vector3.zero;

            for (int y = 0; y < _dimensions.y; y++)
            {

                //float spawnPercent = _dimensions.x /2  - (x + y + z) / 3f;
                float spawnPercent = Vector3.Distance(new Vector2(x, y), new Vector2(_dimensions.x / 2, _dimensions.y / 2));

                spawnPercent = Mathf.Abs(spawnPercent) / (_dimensions.x / 2f) * 100f;
                Debug.Log($"Indices : {x}, {y} --> Spawn Percent : {spawnPercent}");

                if (spawnPercent < _skipPercent + Random.Range(-20, 20))
                {
                    continue;
                }

                CreateBlockAtCell(x, y, RowHolder);

                if (_animateSpawn)
                {
                    yield return new WaitForSeconds(_timeBtwSpawn);
                } else {
                    //yield return null;
                }
            }
        }

        yield return null;
    }

    public void ClearTransform(){
        //Destroy all the children of this transform from the last one to the first 
        //so that the indices don't get messed up

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    
    private void GetRandomBlockName(){
        int x = Random.Range(0, _dimensions.x);
        int y = Random.Range(0, _dimensions.y);

        if (_blocks[x, y] == null)
        {
            Debug.Log("Block is null");
            CreateBlockAtCell(x, y);
            return;
        }

        Debug.Log("Block Name : " + _blocks[x, y].name);
    }

    public void CreateBlockAtCell(int x, int y, Transform parent = null){

        if (parent == null)
        {
            parent = transform;
        }

        GameObject block = Instantiate(_blockPrefab, parent);
        block.name = "Block : " + x + " , " + y;
        block.transform.localPosition = new Vector3(x * BLOCK_SIZE - _dimensions.x / 2 * BLOCK_SIZE,
            y * BLOCK_SIZE - _dimensions.y / 2 * BLOCK_SIZE
        );
        block.transform.localScale = _spawnSize;

        _blocks[x, y] = block.GetComponent<Block>().Initialize();
    }
}
