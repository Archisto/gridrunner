using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private float _squareWidth = 1f;

    [SerializeField]
    private Transform _topLeftCorner;

    [SerializeField]
    private Transform _bottomRightCorner;

    [SerializeField]
    private GameObject _groundBlockPrefab;

    [SerializeField]
    private Transform _groundParent;

    private GameObject[,] _blocks;
    private int _gridWidth;
    private int _gridHeight;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        CreateGround();
    }

    /// <summary>
    /// Creates the ground.
    /// </summary>
    private void CreateGround()
    {
        if (_groundBlockPrefab == null)
        {
            return;
        }

        _gridWidth = (int)((_bottomRightCorner.position.x - _topLeftCorner.position.x) / _squareWidth);
        _gridHeight = (int)((_topLeftCorner.position.z - _bottomRightCorner.position.z) / _squareWidth);
        _blocks = new GameObject[_gridHeight, _gridWidth];

        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                GameObject block = Instantiate(_groundBlockPrefab, _groundParent);
                block.name = "Block_" + x + "-" + y;
                block.transform.position =
                    _topLeftCorner.position + new Vector3(_squareWidth * (x + 0.5f), 0, _squareWidth * -1 * (y + 0.5f));
                _blocks[y,x] = block;
            }
        }

        //foreach (GameObject block in _blocks)
        //{
        //    Debug.Log(block.name);
        //}
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        
    }

    /// <summary>
    /// Draws gizmos
    /// </summary>
    private void OnDrawGizmos()
    {
        if (_topLeftCorner == null || _bottomRightCorner == null)
        {
            return;
        }

        Vector3 topRightCorner = new Vector3(_bottomRightCorner.position.x, _topLeftCorner.position.y, _topLeftCorner.position.z);
        Vector3 bottomLeftCorner = new Vector3(_topLeftCorner.position.x, _topLeftCorner.position.y, _bottomRightCorner.position.z);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(_topLeftCorner.position, topRightCorner);
        Gizmos.DrawLine(topRightCorner, _bottomRightCorner.position);
        Gizmos.DrawLine(_bottomRightCorner.position, bottomLeftCorner);
        Gizmos.DrawLine(bottomLeftCorner, _topLeftCorner.position);
    }
}
