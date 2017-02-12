public interface IKillable
{
    void Kill();
}

public interface IDamagable<T>
{
    void Damage(T damageTaken);
}

public interface Healable<T>
{
    void Heal(T regen);
}

public interface IInteractable
{
    void Interact();
}

public interface IInteractable<T>
{
    void Interact(T interactionParam);
}