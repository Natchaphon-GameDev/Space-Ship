using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace SpaceShip
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Bullet bulletSpeed;
        private Transform player;
        private Vector2 target;
        private new Rigidbody2D rigidbody2D;
        

        private void Start()
        {
            player = GameManager.Instance.PlayerSpawned.transform;
            target = new Vector2(player.position.x, player.position.y);
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, target, bulletSpeed.Speed * Time.deltaTime);

            if (transform.position.x == target.x || transform.position.y == target.y)
            {
                // DestroyProjectile();
                rigidbody2D.gravityScale = 1;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                DestroyProjectile();
            }
        }

        private void DestroyProjectile()
        {
            Destroy(gameObject);
        }
    }
}