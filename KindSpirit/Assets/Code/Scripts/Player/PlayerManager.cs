/*****************************************
 * Created by: Andrew Decker
 * 
 * Playter Manager keeps track of all playerstats 
 * and handles different events regarding the player
 * including restricting controls, respawning, and more.
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

    [SerializeField]
    private float movementSpeed = 5.0f;
    public float MovementSpeed { get { return movementSpeed; } }

    // Used by all other player scripts to position and animate attacks correctly
    [HideInInspector]
    public Vector2 attackDirection;
    public Vector2 movementDirection;

    private void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
#if UNITY_EDITOR
        DebugControls();
#endif
        //UpdateDirection();

    }

    public void UpgradeHealth(int upgradeAmount)
    {
        maxHealth += upgradeAmount;
        health += upgradeAmount;

        Debug.Log("Maximum Health: " + maxHealth + "   Current Health: " + health);

        UpdateHealthUI();
    }

    public void Heal(int regen)
    {
        health += regen;

        if (health > maxHealth)
            health = maxHealth;

        Debug.Log("Healing : " + regen + " HP    Current Health: " + health);

        UpdateHealthUI();
    }

    public void Damage(int damageTaken)
    {
        health -= damageTaken;

        Debug.Log("Damage taken: " + damageTaken + "    Current Health: " + health);

        UpdateHealthUI();

        if (health <= 0)
        {
            Kill();
        }
    }

    void UpdateHealthUI()
    {
        // Do things
    }

    public void Kill()
    {
        // Die!
        health = 0;
        Debug.Log("Anton has died.");
    }

    void SpeedUpgrade(float upgradeAmount)
    {
        movementSpeed += upgradeAmount;
        Debug.Log("Movement Speed: " + movementSpeed);
    }

    void DebugControls() // Used to control input values mid game, should only be usable during development
    {
        if (Input.GetKeyDown(KeyCode.T))        // Heal Player
            Heal(5);
        else if (Input.GetKeyDown(KeyCode.G))   // Hurt Player
            Damage(5);
        else if (Input.GetKeyDown(KeyCode.B))   // Killed Player
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
