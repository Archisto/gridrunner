using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _startingCoordinates;

    [SerializeField]
    private float _moveTime = 0.5f;

    private LevelManager _level;

    private Vector2Int _gridCoordinates;
    private float _moveCooldown;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        _level = FindObjectOfType<LevelManager>();

        if (!_level.CoordinatesAreValid(_startingCoordinates))
        {
            Debug.LogWarning("Invalid starting coordinates: " + _startingCoordinates);
            _startingCoordinates = Vector2Int.zero;
        }
        
        MoveToGridCoordinates(_startingCoordinates);
    }

    /// <summary>
    /// Moves the player character to the coordinates in the grid.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    /// <returns>Was the move successful.</returns>
    private void MoveToGridCoordinates(Vector2Int coordinates)
    {
        _gridCoordinates = coordinates;
        SetPositionInGrid(coordinates);
    }

    /// <summary>
    /// Sets the player character's position in the grid.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    private void SetPositionInGrid(Vector2Int coordinates)
    {
        Vector3 newPosition = _level.GetGridPosition(coordinates);
        newPosition.y = _level.GroundY;
        transform.position = newPosition;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (_moveCooldown > 0)
        {
            _moveCooldown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Moves the player character.
    /// </summary>
    /// <param name="direction">The direction.</param>
    /// <returns>Was the move successful.</returns>
    public bool Move(LevelManager.Direction direction)
    {
        if (_moveCooldown <= 0)
        {
            Vector2Int newCoordinates = _gridCoordinates;

            switch (direction)
            {
                case LevelManager.Direction.None:
                    return false;
                case LevelManager.Direction.Up:
                    newCoordinates += new Vector2Int(0, -1);
                    break;
                case LevelManager.Direction.Down:
                    newCoordinates += new Vector2Int(0, 1);
                    break;
                case LevelManager.Direction.Left:
                    newCoordinates += new Vector2Int(-1, 0);
                    break;
                case LevelManager.Direction.Right:
                    newCoordinates += new Vector2Int(1, 0);
                    break;
            }

            if (_level.CoordinatesAreValid(newCoordinates))
            {
                MoveToGridCoordinates(newCoordinates);
                _moveCooldown = _moveTime;
                return true;
            }
        }

        return false;
    }
}
