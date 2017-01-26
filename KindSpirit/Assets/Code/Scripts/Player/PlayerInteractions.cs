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
        switch (playerManager.controlStyle)
        {
            case PlayerManager.ControlStyle.MatchDirections:
                MatchDirectionInteractions();
                break;
            case PlayerManager.ControlStyle.TwinStick:
                TwinStickInteractions();
                break;
            default:
                Debug.LogError("No valid control style enabled on player");
                break;
        }
    }

    void MatchDirectionInteractions()
    {

    }

    void TwinStickInteractions()
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
