/*******************************************************************
 * 
 * Created by Andrew Decker
 * 
 * Attached to all enemies (might make a different one for the bosses)
 * Stores all important values for the enemies to be used by their 
 * scripted behavior. Override this as necessary.
 * 
 * ****************************************************************/
using UnityEngine;

public class EnemyManager : MonoBehaviour, IKillable, IDamagable<int>
{
    // All fields regarding zombie stats are private
    // and must only be changed by items / events and
    // special function calls

    // Enemy Health
    private int health;
    public int Health { get { return health; } }

    // Enemy Max Health
    [SerializeField]
    private int maxHealth = 10;
    public int MaxHealth { get { return maxHealth; } }

    // Enemy Movement Speed in units-per-second (UPS)
    [SerializeField]
    private float movementSpeed = 5.0f;
    public float MovementSpeed { get { return movementSpeed; } }

    // Attack power of whatever the enemy's attack may be
    [SerializeField]
    private float attackPower = 3;
    public float AttackPower { get { return attackPower; } }

    protected virtual void Awake()
    {
        // Reset enemy health. this may have to change if enemies are stored from one room to another
        health = maxHealth;
    }

    public virtual void Kill()
    {
        // When overriden, base.Kill() should be the last line in the override
        // Don't forget to play the death animation first!

        health = 0;

        // Add a timer to this in children scripts to play their death animations
        Destroy(gameObject);
    }

    public virtual void Damage(int damageTaken)
    {
        health -= damageTaken;

        Debug.Log(name + " damage taken: " + damageTaken + "    " + name + " current Health: " + health);

        if (health <= 0)
        {
            Kill();
        }
    }
}
