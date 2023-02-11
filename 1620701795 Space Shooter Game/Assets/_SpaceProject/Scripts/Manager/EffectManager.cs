using System;
using System.Collections;
using System.Collections.Generic;
using EnemyShip;
using Manager;
using SpaceShip;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject explodedEffect;
    [SerializeField] private GameObject playerExplodedEffect;

    public static EffectManager Instance { get; private set; }

    private void Awake()
    {
        Debug.Assert(explodedEffect != null, "explodedEffect can't be null");
        Debug.Assert(playerExplodedEffect != null, "playerExplodedEffect can't be null");

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnemyExploded(EnemySpaceShip enemy)
    {
        //exploded particle for each EnemySpaceShip
        Instantiate(explodedEffect, enemy.transform.position, Quaternion.identity);
    }

    public void PlayerExploded(PlayerSpaceShip player)
    {
        Instantiate(playerExplodedEffect, player.transform.position, Quaternion.identity);
    }
}
//Please Give A to 1620701795 senPai :)