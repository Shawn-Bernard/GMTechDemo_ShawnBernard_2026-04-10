using UnityEngine;

public interface IKnockbackable
{
    void ApplyKnockback(Transform source, float duration, float force);
}
