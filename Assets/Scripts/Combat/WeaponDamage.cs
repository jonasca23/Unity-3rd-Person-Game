using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private List<Collider> alreadyCollidedWith = new List<Collider>();
    int damageAmount;
    float knockbackAmount;

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (alreadyCollidedWith.Contains(other)) return;
        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damageAmount);
        }

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockbackAmount);
        }
    }

    public void SetAttack(int _amount, float _knockback)
    {
        damageAmount = _amount;
        knockbackAmount = _knockback;
    }
}