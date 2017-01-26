using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    private PlayerManager playerManager;
    private Rigidbody2D playerRB;                   // Used to move the player
    private Vector2 move;                           // Used to store the input
    private SpriteRenderer playerSpriteRenderer;    // Used in re-orienting sprite based on movement
    private float horizontalMovement, verticalMovement;

    // Use this for initialization
    void Start()
    {
        // Set up any necesary references
        playerManager = GetComponent<PlayerManager>();
        playerRB = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        verticalMovement = Input.GetAxis("Horizontal");
        horizontalMovement = Input.GetAxis("Vertical");

        // Take in movement from the controller and multiply the movement speed by the player speed
        move.x = playerRB.position.x + (horizontalMovement * Time.fixedDeltaTime * playerManager.MovementSpeed);
        move.y = playerRB.position.y + (verticalMovement * Time.fixedDeltaTime * playerManager.MovementSpeed);

        // Apply motion
        playerRB.MovePosition(move);

        // Flip Sprite appropriately
        FlipSprite();
    }

    void FlipSprite()
    {
        //TODO: This will most likely be removed when the animations for the player are added

        // Get the input for left/right motion

        // If we aren't moving left or right ..
        if (horizontalMovement == 0)
        {
            // .. don't do anything
            return;
        }
        else // ..otherwise,
        {
            // .. change the sprite direction appropriately
            playerSpriteRenderer.flipX = horizontalMovement > 0;
        }

    }
}
