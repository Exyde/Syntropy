using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] Vector3Int _worldSize;
    [SerializeField] public static Vector3Int _dimensions;

    public static float BLOCK_SIZE = 2;

    [SerializeField] GameObject _blockPrefab;
    [SerializeField] bool _animateSpawn = false;
    [SerializeField]float _timeBtwSpawn = 0.001f;

    [SerializeField] Vector3 _spawnSize = new Vector3(.1f , .1f , .1f);

    [SerializeField] Block [,,] _blocks; // 3D array of blocks
    

    [SerializeField][Range(0, 100)] int _skipPercent = 50;

    private FPSController _player;

    void Awake(){
    }
    private void Start() {
        BuildWorld();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            BuildWorld();
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

        Transform XTransform, YTransform;

        _blocks = new Block[_dimensions.x, _dimensions.y, _dimensions.z];

        for (int x = 0; x < _dimensions.x; x++)
        {

            XTransform = new GameObject("Holder : " + x).transform;
            XTransform.parent = transform;

            for (int y = 0; y < _dimensions.y; y++)
            {

                YTransform = new GameObject("Holder : " + y).transform;
                YTransform.parent = XTransform;

                for (int z = 0; z < _dimensions.z; z++)
                {

                    //float spawnPercent = _dimensions.x /2  - (x + y + z) / 3f;
                    float spawnPercent = Vector3.Distance(new Vector3(x, y, z), new Vector3(_dimensions.x / 2, _dimensions.y / 2, _dimensions.z / 2));

                    spawnPercent = Mathf.Abs(spawnPercent) / (_dimensions.x / 2f) * 100f;
                    //Debug.Log($"Indices : {x}, {y}, {z} --> Spawn Percent : {spawnPercent}");

                    if (spawnPercent < _skipPercent)
                    {
                        continue;
                    }

                    CreateBlockAtCell(x, y, z , YTransform);

                    if (_animateSpawn)
                    {
                        yield return new WaitForSeconds(_timeBtwSpawn);
                    } else {
                        //yield return null;
                    }
                }
            }
        }

        yield return new WaitForSeconds(1f);
        
        _player = FindObjectOfType<FPSController>();
        if (_player != null){   
            _player.transform.position =  Vector3.zero;
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
        int z = Random.Range(0, _dimensions.z);

        if (_blocks[x, y, z] == null)
        {
            Debug.Log("Block is null");
            CreateBlockAtCell(x, y, z);
            return;
        }

        Debug.Log("Block Name : " + _blocks[x, y, z].name);
    }

    public void CreateBlockAtCell(int x, int y, int z, Transform parent = null){

        if (parent == null)
        {
            parent = transform;
        }

        GameObject block = Instantiate(_blockPrefab, parent);
        block.name = "Block : " + x + " , " + y + " , " + z;
        block.transform.localPosition = new Vector3(x * BLOCK_SIZE - _dimensions.x / 2 * BLOCK_SIZE,
            y * BLOCK_SIZE - _dimensions.y / 2 * BLOCK_SIZE,
            z * BLOCK_SIZE - _dimensions.z / 2 * BLOCK_SIZE
        );
        block.transform.localScale = _spawnSize;

        _blocks[x, y, z] = block.GetComponent<Block>().Initialize();
    }
}
