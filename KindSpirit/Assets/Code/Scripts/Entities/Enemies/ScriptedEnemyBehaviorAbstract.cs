/*********************************************************************
 * 
 * Created By Andrew Decker
 * 
 * An abstract to lay the groundwork for all enelmies with patterned
 * behavior. This must be overridden and cannot be directly attached
 * to an enemy. This will probably need to be updated at we go on.
 * 
 * The Behavior loop works as follows
 * write a behavior for what the enemy does when not attacking and
 * state 0, then wait before enacting stage 1 of behavior loop and repeat.
 * Ask me in a meeting if you have questions.
 * 
 * ******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptedEnemyBehaviorAbstract : MonoBehaviour {

    EnemyManager enemyManager;

    protected virtual void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    // Use this for initialization
    protected virtual void Start ()
    {
        // Override this if you want an animation or other action to happen exclusively when an enemy enters a room
        StartCoroutine(BehaviorLoop());
	}

    protected virtual void Update()
    {
        if(enemyManager.Health <= 0)
        {
            // Stop the behavior Loop
            StopAllCoroutines();
        }
    }

    protected virtual IEnumerator BehaviorLoop()
    {
        // Overwrite this in the inherited classes with replacement behaviors

        do
        {
            // Implement Behavior state 1 heres
            yield return null;

            // Behavior state 2 here
            yield return null;

            // Implement more states as needed
        } while (enemyManager.Health > 0);
    }
}
