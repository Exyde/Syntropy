using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] BlockData _BData;
    [SerializeField] SpriteRenderer _spriteRenderer;

    [SerializeField] Vector3 _blockSize;
    [SerializeField] float _growthStep = .1f;


    public BlockData BlockData { get { return _BData; } }

    private void Awake() {
        float _sizeMalus = Random.Range(.1F, .6f);
        _blockSize = new Vector3(WorldBuilder.BLOCK_SIZE, WorldBuilder.BLOCK_SIZE, WorldBuilder.BLOCK_SIZE);
        //Initialize(_BData);
    }

    public Block Initialize(BlockData data = null)
    {
        if (data) _BData = data;
        _spriteRenderer.sprite = _BData.GetRandomSprite();
        _spriteRenderer.material.color = _BData.GetRandomColor();

        return this;
    }

    private void Update(){
        if (transform.localScale.x < _blockSize.x)
        {
            Grow();
        }
    }

    private void Grow(){
        transform.localScale = Vector3.Lerp(transform.localScale, _blockSize, _growthStep);
    }
}
