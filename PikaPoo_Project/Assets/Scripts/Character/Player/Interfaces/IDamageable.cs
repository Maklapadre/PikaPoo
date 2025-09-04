using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Interface for objects that can take damage.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Method to apply damage to the object.
        /// </summary>
        /// <param name="amount">The amount of damage to apply.</param>
        void TakeDamage(float amount);
        
        /// <summary>
        /// Method to check if the object is alive.
        /// </summary>
        /// <returns>True if the object is alive, false otherwise.</returns>
        bool IsAlive();
    }
}
