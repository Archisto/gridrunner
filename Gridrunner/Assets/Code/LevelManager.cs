using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField]
    private float _squareWidth = 1f;

    [SerializeField]
    private Transform _topLeftCorner;

    [SerializeField]
    private Transform _bottomRightCorner;

    [SerializeField]
    private float _groundY;

    [SerializeField]
    private GameObject _groundBlockPrefab;

    [SerializeField]
    private Transform _groundParent;

    private GameObject[,] _grid;

    public int GridWidth { get; private set; }
    public int GridHeight { get; private set; }

    public int MinXBound { get => 0; }
    public int MinYBound { get => 0; }
    public int MaxXBound { get => GridWidth - 1; }
    public int MaxYBound { get => GridHeight - 1; }

    public float GroundY { get => _groundY; }

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

        GridWidth = (int)((_bottomRightCorner.position.x - _topLeftCorner.position.x) / _squareWidth);
        GridHeight = (int)((_topLeftCorner.position.z - _bottomRightCorner.position.z) / _squareWidth);

        _grid = new GameObject[GridHeight, GridWidth];

        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                GameObject block = Instantiate(_groundBlockPrefab, _groundParent);
                block.name = "Block_" + x + "-" + y;
                block.transform.position =
                    _topLeftCorner.position + new Vector3(_squareWidth * (x + 0.5f), 0, _squareWidth * -1 * (y + 0.5f));
                _grid[y, x] = block;
            }
        }

        //foreach (GameObject block in _blocks)
        //{
        //    Debug.Log(block.name);
        //}
    }

    /// <summary>
    /// Checks whether the grid coordinates are valid.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>Are the coordinates valid.</returns>
    public bool CoordinatesAreValid(int x, int y)
    {
        if (x < MinXBound || x > MaxXBound || y < MinYBound || y > MaxYBound)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks whether the grid coordinates are valid.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    /// <returns>Are the coordinates valid.</returns>
    public bool CoordinatesAreValid(Vector2Int coordinates)
    {
        return CoordinatesAreValid(coordinates.x, coordinates.y);
    }

    /// <summary>
    /// Gets the world position of the grid square.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>The position.</returns>
    public Vector3 GetGridPosition(int x, int y)
    {
        if (!CoordinatesAreValid(x, y))
        {
            return -1 * Vector3.one;
        }

        return _grid[y, x].transform.position;
    }

    /// <summary>
    /// Gets the world position of the grid square.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    /// <returns>The position.</returns>
    public Vector3 GetGridPosition(Vector2Int coordinates)
    {
        return GetGridPosition(coordinates.x, coordinates.y);
    }

    /// <summary>
    /// Draws gizmos.
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
