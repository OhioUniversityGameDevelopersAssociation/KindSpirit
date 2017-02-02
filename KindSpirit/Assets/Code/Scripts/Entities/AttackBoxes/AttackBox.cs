using UnityEngine;

public abstract class AttackBox : MonoBehaviour
{
    public enum Attacker
    {
        Player,
        Enemy
    }

    public Attacker attacker = Attacker.Player;

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        IDamagable<int> other = col.gameObject.GetComponent<IDamagable<int>>();
        if (col.gameObject.tag != attacker.ToString() && other != null)
        {
            Attack(other);
        }
    }

    protected abstract void Attack(IDamagable<int> other);
}
