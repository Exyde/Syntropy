using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Block Data")]
public class BlockData : ScriptableObject
{
    [SerializeField] public string _name;
    [SerializeField] public Color _color;
    [SerializeField] public Texture2D _texture;

    [SerializeField] public Sprite[] _sprites;

    [SerializeField] public List<Color> _colors;

    public Sprite GetRandomSprite() => _sprites[Random.Range(0, _sprites.Length)];

    public Color GetRandomColor() => _colors[Random.Range(0, _colors.Count)];


}
