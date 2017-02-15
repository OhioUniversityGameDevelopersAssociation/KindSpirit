using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackBox : AttackBox {

    private int attackPower;
    private int lifeSpan;

    void Start()
    {
        Destroy(gameObject, lifeSpan /*TODO: Add in time for destroy animation*/);
    }

    protected override void Attack(IDamagable<int> other)
    {
        other.Damage(attackPower);
    }

    public void SetAttackPower(int newAttackPower)
    {
        attackPower = newAttackPower;
    }

    public void SetLifeSpan(int newLifeSpan)
    {
        lifeSpan = newLifeSpan;
    }
}
