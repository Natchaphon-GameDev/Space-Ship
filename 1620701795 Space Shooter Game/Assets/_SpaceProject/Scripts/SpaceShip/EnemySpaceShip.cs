using System;
using Manager;
using UnityEngine;

namespace SpaceShip
{
    public class EnemySpaceShip : BaseSpaceShip, IDamageable
    {
        public event Action OnExploded;

        private void Awake()
        {
            Debug.Assert(defaultBullet != null, "defaultBullet can't be null");
            Debug.Assert(gunPosition != null, "gunPosition can't be null");
        }

        public void Init(int hp, float speed)
        {
            base.Init(hp, speed, defaultBullet);
        }

        public override void Fire()
        {
            SoundManager.Instance.Play(SoundManager.Sound.EnemyFire);
        }

        public void TakeHit(int damage)
        {
            Hp -= damage;
            if (Hp > 0)
            {
                return;
            }
            

            Explode();
        }

        public void Explode()
        {
            EffectManager.Instance.EnemyExploded(this);
            OnExploded?.Invoke();
            Debug.Assert(Hp <= 0, "Hp is more than zero");
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        
        
    }
}