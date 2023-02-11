using System;
using Manager;
using UnityEngine;

namespace SpaceShip
{
    public class PlayerSpaceShip : BaseSpaceShip, IDamageable
    {
        public event Action OnExploded;
        
        public static PlayerSpaceShip Instance { get; private set; }

        private void Awake()
        {
            Debug.Assert(defaultBullet != null, "defaultBullet can't be null");
            Debug.Assert(gunPosition != null, "gunPosition can't be null");
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void Init(int hp, float speed)
        {
            base.Init(hp, speed, defaultBullet); //TODO: not understand defaultBullet
        }

        public override void Fire()
        {
            var bullet = Instantiate(defaultBullet, gunPosition.position, Quaternion.identity); //(prefab, position, Quaternion)
            SoundManager.Instance.Play(SoundManager.Sound.PlayerFire);
            bullet.Init(); //to define if bullet spawn it will move immediately
        }

        public void TakeHit(int damage)
        {
            Hp -= damage;
            // if HP more than zero go out of method TakeHit
            if (Hp > 0)
            {
                //For When PlayerDie it'll not play sound
                SoundManager.Instance.Play(SoundManager.Sound.ShieldDown);
                return;
            }
            Explode();
        }

        public void Explode()
        {
            EffectManager.Instance.PlayerExploded(this);
            OnExploded?.Invoke(); //Invoke will call the listener to work 
            Debug.Assert(Hp <= 0, "Hp is more than zero");
            Destroy(gameObject);
        }
    }
}