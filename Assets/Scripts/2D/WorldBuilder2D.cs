using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder2D : MonoBehaviour
{
    [SerializeField] RoomBuilder2D[,] _rooms;
    [SerializeField] Vector2Int _dimensions= new Vector2Int(4, 4);

    [SerializeField] RoomBuilder2D _roomPrefab;

    [SerializeField] Vector2Int _roomSize = new Vector2Int(16, 16);

    private void Start() {
        BuildWorld();
    }

    private void Update() {
        //Regenerate Rooms
        if (Input.GetKeyDown(KeyCode.R)){
            BuildWorld();
        }
    }

    void BuildWorld(){
        StopAllCoroutines();
        StartCoroutine(GenerateWorld());
    }

    IEnumerator GenerateWorld(){

        ClearTransform();

        _rooms = new RoomBuilder2D[_dimensions.x, _dimensions.y];
        for (int x = 0; x < _dimensions.x; x++){
            for (int y = 0; y < _dimensions.y; y++){
                RoomBuilder2D newRoom = Instantiate(_roomPrefab, new Vector3(x * _roomSize.x * 2, y * _roomSize.y * 2, 0), Quaternion.identity);
                newRoom.name = "Room : " + (x * _dimensions.x +y);
                newRoom.transform.parent = this.transform;
                newRoom.BuildWorld();
                _rooms[x, y] = newRoom;

                yield return new WaitForSeconds(.1f);
            }
        }

        yield return new WaitForSeconds(1f);
    
        PlayerController2D _player = FindObjectOfType<PlayerController2D>();
        if (_player != null){
            _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _player.transform.position = _rooms[0, 0].transform.position;
        }
    }

        public void ClearTransform(){
        //Destroy all the children of this transform from the last one to the first 
        //so that the indices don't get messed up

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
