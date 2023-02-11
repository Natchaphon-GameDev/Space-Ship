using System;

namespace SpaceShip
{
    public interface IDamageable //Interface like API : Define what tasks are required 
    {
        // The Things that can be damaged
        event Action OnExploded;
        void TakeHit(int damage);
        void Explode();
    }
}