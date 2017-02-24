/*****************************************
 * Created by: Andrew Decker
 * 
 * Playter Manager keeps track of all playerstats 
 * and handles different events regarding the player
 * including restricting controls, respawning, and more.
 * Most variables in this script are private to ensure 
 * that if they are going to be modified, it is with 
 * correct intension. We don't want unexpected things
 * happening to out player. Stores movement and attack
 * in seperate Vector2's but can easily be adapted to
 * Vector3's by the script utilizing those properties.
 * 
 * 
 * Edited by Alex Houser
 * Implemented Animation Control for Movement, Attacks,
 * and Death
 * 
 * **************************************/

using UnityEngine;

public class PlayerManager : MonoBehaviour, IKillable, IDamagable<int>, Healable<int>
{

    // All fields regarding player stats are private 
    // and must only be changed by items / events and
    // special function calls

    // Player Health
    private int health;
    public int Health { get { return health; } }

    // Player Max Health
    [SerializeField]
    private int maxHealth = 10;
    public int MaxHealth { get { return maxHealth; } }

    // Player Movement Speed int units-per-second (UPS)
    [SerializeField]
    private float movementSpeed = 5.0f;
    public float MovementSpeed { get { return movementSpeed; } }

    // Are we currently allowing the player to control the player gameObject?
    [SerializeField]
    private bool playerControlEnabled = true;
    public bool PlayerControlEnabled { get { return playerControlEnabled; } }

    [SerializeField]
    private int meleeAttackPower = 5;
    public int MeleeAttackPower { get { return meleeAttackPower; } }
    [SerializeField]
    private int rangedAttackPower = 2;
    public int RangedAttackPower { get { return meleeAttackPower; } }

    // Variables holding controller input to centralize all controls. All player scripts will draw from this
    private Vector2 attackDirection;
    public Vector2 AttackDirection { get { return attackDirection; } }
    private Vector2 movementDirection;
    public Vector2 MovementDirection { get { return movementDirection; } }

	// Reference to the Animator
	private Animator anim;

    private void Awake()
    {
        // Reset player health. This may change with us having to store player health between scenes
        health = maxHealth;

		// Grab a reference to the animator
		anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (playerControlEnabled)
            UpdateMovementVariables();
    }

    private void Update()
    {
        if (playerControlEnabled)
            UpdateAttackVariables();

#if UNITY_EDITOR
        DebugControls();
#endif
    }

    public void UpgradeHealth(int upgradeAmount)
    {
        // TODO: Do we eventually need a maximum maxHealth cap?
        maxHealth += upgradeAmount;
        health += upgradeAmount;

        Debug.Log("Maximum Health: " + maxHealth + "   Current Health: " + health);

        UpdateHealthUI();
    }

    // Heal the player, fairly self explanatory
    public void Heal(int regen)
    {
        health += regen;

        // Do not let the player exceed maximum health capacity
        if (health > maxHealth)
            health = maxHealth;

        Debug.Log("Healing : " + regen + " HP    Current Health: " + health);

        UpdateHealthUI();
    }

    // Applied when enemies damage the player. Fairly Self Explanatory
    public void Damage(int damageTaken)
    {
        // TODO: Should we build in Coroutine for invulnrability frames immediately after attack?
        // ANS: YES!

        health -= damageTaken;

        Debug.Log("Damage taken: " + damageTaken + "    Current Health: " + health);

        UpdateHealthUI();

        if (health <= 0)
        {
            health = 0;
            Kill();
        }
    }

    void UpdateHealthUI()
    {
        // Something to do with a health bar/hearts. We'll Figure it out
    }

    // Called when the Player is killed
    public void Kill()
    {
        // Die!
        health = 0;
        Debug.Log("Player has died.");

		// Set Animator to die
		anim.SetTrigger("Death");
    }

    // Used to adjust the movement speed of the player
    void SpeedUpgrade(float upgradeAmount)
    {
        // TODO: Do we eventually need a movement speed cap?
        // ANS: Cap this and Damage upgrades
        movementSpeed += upgradeAmount;
        Debug.Log("Movement Speed: " + movementSpeed);
    }

    public void EnablePlayerControl()
    {
        playerControlEnabled = true;
    }

    public void DisablePlayerControl()
    {
        playerControlEnabled = false;
    }

    // Used to set the locally stored directional variables that will be read by all other scripts
    void UpdateMovementVariables()
    {
        // Take in the Movement axis from the controller
        movementDirection.Set(Input.GetAxis("Horizontal Movement"), Input.GetAxis("Vertical Movement"));

        // Set Animator controller movement variables
		anim.SetInteger("Walk Horizontal", (int)Mathf.Round(movementDirection.x));
		anim.SetInteger ("Walk Vertical", (int)Mathf.Round (movementDirection.y));
    }

    void UpdateAttackVariables()
    {
        // Take in the Attack direction from the controller and normalize it into the four directions
        attackDirection = Utility.CutVector2(Input.GetAxis("Horizontal Interaction"), Input.GetAxis("Vertical Interaction"));
        
        // Set Animator controller attack variables
		anim.SetInteger("Attack Horizontal", (int)attackDirection.x);
		anim.SetInteger("Attack Vertical", (int)attackDirection.y);
    }

    void DebugControls() // Used to control input values mid game, should only be usable during development
    {
        if (Input.GetKeyDown(KeyCode.T))        // Heal Player
            Heal(5);
        else if (Input.GetKeyDown(KeyCode.G))   // Hurt Player
            Damage(5);
        else if (Input.GetKeyDown(KeyCode.B))   // Kill Player
            Kill();
        else if (Input.GetKeyDown(KeyCode.U))   // Increase Maximum Health
            UpgradeHealth(5);
        else if (Input.GetKeyDown(KeyCode.J))   // Decrease Maximum Health
            UpgradeHealth(-5);
        else if (Input.GetKeyDown(KeyCode.Y))   // Increase Movement Speed
            SpeedUpgrade(1f);
        else if (Input.GetKeyDown(KeyCode.H))   // Decrease Movement Speed
            SpeedUpgrade(-1f);
    }
}
