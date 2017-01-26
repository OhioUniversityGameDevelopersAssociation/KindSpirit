using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    private PlayerManager playerManager;

    // Use this for initialization
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DirectionalAttacks();
    }

    void DirectionalAttacks()
    {
        
    }

    void MeleeAttack()
    {
        Debug.Log("Melee Attack!");
    }

    void RangedAttack()
    {
        Debug.Log("Ranged Attack!");
    }

    void AOEAttack()
    {
        Debug.Log("Area of Effect Attack!");
    }
}
