using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class AttackArea : MonoBehaviour
{
    public List<IDamageable> DamageablesInRange { get; } = new List<IDamageable>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damageable")
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null && !DamageablesInRange.Contains(damageable))
            {
                DamageablesInRange.Add(damageable);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Damageable")
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null && DamageablesInRange.Contains(damageable))
            {
                DamageablesInRange.Remove(damageable);
            }
        }
    }
}

