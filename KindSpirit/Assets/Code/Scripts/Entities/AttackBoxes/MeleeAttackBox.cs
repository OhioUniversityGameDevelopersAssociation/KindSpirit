/******************************************
 * 
 * Created By: Andrew Decker
 * 
 * Script inheriting from abstract class AttackBox
 * Used for all Melee Attacks, both player and enemy
 * 
 * ***************************************/
using UnityEngine;


public class MeleeAttackBox : AttackBox
{
    [SerializeField]
    protected int attackPower;

    protected override void Attack(IDamagable<int> other)
    {
        other.Damage(attackPower);
    }

    public void SetAttackPower(int newAttackPower)
    {
        attackPower = newAttackPower;
    }
}
