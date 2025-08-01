using Interfaces;
using UnityEngine;
using UnityEngine.Splines;

public class SimpleEnemy : MonoBehaviour, IDamageable
{
    public bool isAlive = true;

    public void TakeDamage(float damageAmount)
    {
        Debug.Log("Ouch, I took -" + damageAmount + "HP!");
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
