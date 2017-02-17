using UnityEngine;

public class Zombie : MonoBehaviour, IKillable, IDamagable<int>
{
    private int health;
    public int Health { get { return health; } }

    // Player Max Health
    [SerializeField]
    private int maxHealth = 10;
    public int MaxHealth { get { return maxHealth; } }

    [SerializeField]
    private float movementSpeed = 5.0f;
    public float MovementSpeed { get { return movementSpeed; } }

    private void Awake()
    {
        health = maxHealth;
    }

    public void Kill()
    {
        // Die!
        health = 0;
        Destroy(gameObject);
        Debug.Log("Zombie has died.");
    }
    public void Damage(int damageTaken)
    {
        health -= damageTaken;

        Debug.Log("Zombie damage taken: " + damageTaken + "    Zombie current Health: " + health);

        if (health <= 0)
        {
            Kill();
        }
    }


}