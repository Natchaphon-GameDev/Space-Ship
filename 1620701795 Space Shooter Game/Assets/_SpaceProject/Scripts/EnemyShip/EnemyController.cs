using System;
using Manager;
using SpaceShip;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyShip
{
    public class EnemyController : MonoBehaviour //TODO!!: Problem enemy follow the spawn of playerShip position
    {
        private float timeBtwShots;
        [SerializeField] private float maxTimeBtwShots;

        [SerializeField] private Projectile bulletProjectile;
        [SerializeField] private EnemySpaceShip enemySpaceShip;
        [SerializeField] private float chasingThresholdDistance;
        [SerializeField] private float retreatDistance;


        private void Awake()
        {
            Debug.Assert(enemySpaceShip != null, "enemySpaceShip can't be null");
            Debug.Assert(bulletProjectile != null, "projectile can't be null");
            Debug.Assert(retreatDistance > 0, "retreatDistance Can't be under the zero");
            Debug.Assert(chasingThresholdDistance > 0, "chasingThresholdDistance Can't be under the zero");
            Debug.Assert(maxTimeBtwShots > 0, "startTimeBtwShots Can't be under the zero");
        }

        private void Start()
        {
            timeBtwShots = maxTimeBtwShots;
        }

        private void Update()
        {
            MoveToPlayer();
            ShootPlayer();
        }

        private void MoveToPlayer()
        {
            //move to player system
            if (Vector2.Distance(transform.position, GameManager.Instance.PlayerSpawned.transform.position) >
                chasingThresholdDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    GameManager.Instance.PlayerSpawned.transform.position, enemySpaceShip.Speed * Time.deltaTime);
            }
            //stop system
            else if (Vector2.Distance(transform.position, GameManager.Instance.PlayerSpawned.transform.position) <
                     chasingThresholdDistance &&
                     Vector2.Distance(transform.position, GameManager.Instance.PlayerSpawned.transform.position) >
                     retreatDistance)
            {
                transform.position = transform.position;
            }
            //retreat system
            else if (Vector2.Distance(transform.position, GameManager.Instance.PlayerSpawned.transform.position) <
                     retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    GameManager.Instance.PlayerSpawned.transform.position, -enemySpaceShip.Speed * Time.deltaTime);
            }
        }

        private void ShootPlayer()
        {
            if (timeBtwShots <= 0)
            {
                enemySpaceShip.Fire();
                Instantiate(bulletProjectile, transform.position, Quaternion.identity);
                timeBtwShots = maxTimeBtwShots;
                // Destroy(bulletProjectile,8);
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
//Please Give A to 1620701795 senPai :)