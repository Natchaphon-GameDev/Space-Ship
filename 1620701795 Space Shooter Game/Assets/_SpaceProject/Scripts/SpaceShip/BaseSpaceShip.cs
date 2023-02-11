using UnityEngine;

namespace SpaceShip
{
    public abstract class BaseSpaceShip : MonoBehaviour //abstract used because doesn't need to create new one need only base class 
    {
        [SerializeField] protected Bullet defaultBullet;
        [SerializeField] protected Transform gunPosition;

        public int Hp { get; protected set; }
        public float Speed { get; private set; }
        public Bullet Bullet { get; private set; }

        protected void Init(int hp, float speed, Bullet bullet)
        {
            Hp = hp;
            Speed = speed;
            Bullet = bullet;
        }

        public abstract void Fire(); //used for let's child class implement itself 
    }
}