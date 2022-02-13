using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Player _player;
    //private LevelManager _level;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        //_level = FindObjectOfType<LevelManager>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        CheckPlayerInput();
        CheckDebugInput();
    }

    /// <summary>
    /// Checks the player input.
    /// </summary>
    private void CheckPlayerInput()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (move.y > 0)
        {
            _player.Move(LevelManager.Direction.Up);
        }
        else if (move.y < 0)
        {
            _player.Move(LevelManager.Direction.Down);
        }
        else if (move.x > 0)
        {
            _player.Move(LevelManager.Direction.Right);
        }
        else if (move.x < 0)
        {
            _player.Move(LevelManager.Direction.Left);
        }
    }

    /// <summary>
    /// Checks the debug input.
    /// </summary>
    private void CheckDebugInput()
    {
        // TODO: Reset game
    }
}
