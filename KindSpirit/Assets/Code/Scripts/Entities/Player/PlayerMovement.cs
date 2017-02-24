/**************************************
 * Created By: Andrew Decker
 * 
 * Basic Movement Script for any 2D player character.
 * Developed for Kind Spirit, but can work for any 2D
 * Character System so long as it also has Player Manager,
 * a Sprite Renderer, and a Rigidbody2D attached.
 * 
 * ***********************************/

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerManager))]
public class PlayerMovement : MonoBehaviour
{

    private PlayerManager playerManager;
    private Rigidbody2D playerRB;                   // Used to move the player
    private Vector2 move;                           // Used to store the input
    private SpriteRenderer playerSpriteRenderer;    // Used in re-orienting sprite based on movement

    // Use this for initialization
    private void Start()
    {
        // Set up any necesary references
        playerManager = GetComponent<PlayerManager>();
        playerRB = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Fixed Update is called once per physics frame
    private void FixedUpdate()
    {
        // Take in movement from the controller and multiply the movement speed by the player speed
        move.x = playerRB.position.x + (playerManager.MovementDirection.x * Time.fixedDeltaTime * playerManager.MovementSpeed);
        move.y = playerRB.position.y + (playerManager.MovementDirection.y * Time.fixedDeltaTime * playerManager.MovementSpeed);
        // Apply motion
        playerRB.MovePosition(move);

    }


}
