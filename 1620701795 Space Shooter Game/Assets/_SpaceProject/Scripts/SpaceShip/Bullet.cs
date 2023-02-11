using System;
using UnityEngine;

namespace SpaceShip
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int damage = 100;
        public float Speed;
        [SerializeField] private new Rigidbody2D rigidbody2D;
        
        public void Init()
        {
            Move();
        }

        private void Awake()
        {
            Debug.Assert(rigidbody2D != null, "rigidbody2D can't be null");
            
        }

        private void Move()
        {
            rigidbody2D.velocity = Vector2.up * Speed; //to define that bullet move up
        }

        private void OnTriggerEnter2D(Collider2D other) //for detection
        {            
            if (other.CompareTag("ObjectDestroyer"))
            {
                Destroy(gameObject);
                return;
            }
            var target = other.gameObject.GetComponent<IDamageable>(); //check that object has IDamageable
            target?.TakeHit(damage); //target? == if (target != null){target.TakeHit(damage)} 
        }
    }
}