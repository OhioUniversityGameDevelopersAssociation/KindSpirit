/**********************************************
 * Created By: Andrew Decker
 * 
 * Script to handle any player attacks appropriately.
 * Requires a PlayerManager be attached to the same
 * GameObject. This version handles, melee, ranged,
 * and an AOE Special
 * 
 * 
 * Edited by: Alex Houser
 * Added animation scripts for attack, switch attack
 * mode, and AOE
 * 
 * *******************************************/
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerInteractions : MonoBehaviour
{

    private PlayerManager playerManager;

    // This should be good enough so long as we only have the melee and ranged attacks to cycle between
    public bool meleeActive = true;

    // Cooldown Variables. All made private so no accidental changes can be made from other scripts
    // but serialized so we can edit them in the inspector
    [SerializeField]
    private float meleeCooldown = 0.5f;
    [SerializeField]
    private float rangedCooldown = 0.3f;
    private float basicAttackCooldownRemaining = 0f;
    [SerializeField]
    private float AOECooldown = 5f;
    private float AOECooldownRemaining = 0f;

    // Used to spawn in instances of the ranged attack
    public GameObject rangedAttackPrefab;
    public float rangedShotForceMultiplier = 1.0f;

	// Creates the animator variable
	private Animator anim;

    // Use this for initialization
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();

		// Grabs the animator variable
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DirectionalAttacks();

        if (Input.GetButtonDown("Interact"))
            AttemptInteraction();

        if (Input.GetButtonDown("AOEAttack") && AOECooldownRemaining == 0)
            AOEAttack();

        if (Input.GetButtonDown("Swap Weapon"))
            CycleAttacks();

        CalculateCooldowns();
    }


    void DirectionalAttacks()
    {
        // TODO: How do we want to handle these cooldowns?
        if (playerManager.AttackDirection != Vector2.zero && basicAttackCooldownRemaining == 0)
        {
            if (meleeActive)
                MeleeAttack();
            else
                RangedAttack();

			anim.SetTrigger ("Attack");
        }
    }

    void MeleeAttack()
    {
        basicAttackCooldownRemaining = meleeCooldown;
        // TODO: This will likely need to be completely re-written as we get melee animations in
        Debug.Log("Melee Attack!");
        // attack anim needs to play Attack Trigger

        // TEMPORARY SOLUTION, REMOVE WHEN ANIMATIONS COME IN //
        StartCoroutine(TempMeleeCoroutine());
        ////////////////////////////////////////////////////////
    }

    void RangedAttack() // Can we eventually do this using an object pool?
    {
        Debug.Log("Ranged Attack!");
        basicAttackCooldownRemaining = rangedCooldown;
        // TODO: Spawn objects to shoot out in direction. Circles with colliders will do fine for now
        // Spawn in light ball attack
        Vector3 placementVector = new Vector3(
            transform.position.x + playerManager.AttackDirection.x,
            transform.position.y + playerManager.AttackDirection.y,
            0f);
        RangedAttackBox rangedAttackInstance = Instantiate(rangedAttackPrefab, placementVector, Quaternion.identity).GetComponent<RangedAttackBox>();
        rangedAttackInstance.SetAttackPower(playerManager.RangedAttackPower);
        rangedAttackInstance.SetLifeSpan(3);
        rangedAttackInstance.GetComponent<Rigidbody2D>().velocity = playerManager.AttackDirection * rangedShotForceMultiplier;

    }

    void AOEAttack()
    {
        AOECooldownRemaining = AOECooldown;
        Debug.Log("Area of Effect Attack!");
        // TODO: Turn on Trigger Collider, come up with better solution later
        //  - Should the player be able to move during the AOE attack?
        //  - Should the AOE attack charge?
        // ANS: NOPE
        //  - Can the player walk around?
        //  - It's basically a bomb
        //  - Should the player have invulnrability during animation?


        // TEMPORARY SOLUTION, REMOVE WHEN ANIMATIONS COME IN //
        //StartCoroutine(TempAOECoroutine());
        ////////////////////////////////////////////////////////
		anim.SetTrigger("AOE");

    }


    void AttemptInteraction()
    {
        // TODO:
        /*  - Turn on Some sort of interaction collider
         *  - Have that collider search for an object
         *  - Interaction animation: Should it only be played if successful?
         *  ANS: NOPE
         * */
    }

    void CycleAttacks()
    {
        meleeActive = !meleeActive;

		anim.SetBool ("isMelee", meleeActive);
    }

    // I hate making cooldowns, putting them in a region so I can minimize and not look at them
    #region Cooldowns

    void CalculateCooldowns()
    {
        if (basicAttackCooldownRemaining > 0f)
        {
            basicAttackCooldownRemaining -= Time.deltaTime;
            if (basicAttackCooldownRemaining <= 0f)
                basicAttackCooldownRemaining = 0f;
        }
        if (AOECooldownRemaining > 0f)
        {
            AOECooldownRemaining -= Time.deltaTime;
            if (AOECooldownRemaining <= 0f)
                AOECooldownRemaining = 0f;
        }
    }

    #endregion

    // We may/may not use these 
    #region Lock/Unlock Abilities

    public void UnlockMeleeAttack()
    {
        //unlock ability
    }

    public void UnlockRangedAttack()
    {
        // Unlock ability
    }

    public void UnlockAOEAttack()
    {
        // Unlock ability
    }

    #endregion

    // Variables that will be useless once art assets and animations are set up,
    // but are crucial to these early testing stages
    #region Temporary Variables and Functions

    public GameObject tempMeleeCollider;
    public GameObject tempAOECollider;

    IEnumerator TempAOECoroutine()
    {
        tempAOECollider.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        tempAOECollider.SetActive(false);
    }

    IEnumerator TempMeleeCoroutine()
    {
        tempMeleeCollider.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        tempMeleeCollider.SetActive(false);
    }

    #endregion
}