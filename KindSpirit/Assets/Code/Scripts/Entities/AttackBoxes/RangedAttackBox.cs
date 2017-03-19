using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackBox : AttackBox {

    private int attackPower;
    private int lifeSpan;
	private string ignoreTag = "";

    void Start()
    {
        Destroy(gameObject, lifeSpan /*TODO: Add in time for destroy animation*/);
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
		if (ignoreTag != "" && ignoreTag == col.transform.tag)
			return;
		base.OnCollisionEnter2D (col);
		Destroy (gameObject);
    }

	protected void OnTriggerEnter2D(Collider2D col)//collide with Anton's trigger collider
	{
		if (ignoreTag != "" && ignoreTag == col.transform.tag)
			return;
		IDamagable<int> other = col.gameObject.GetComponent<IDamagable<int>>();
		if (col.gameObject.tag != attacker.ToString() && other != null)
		{
			Attack(other);
		}
		Destroy (gameObject);
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

	public void SetIgnoreCollisions(string tag)
	{
		ignoreTag = tag;
	}
}
