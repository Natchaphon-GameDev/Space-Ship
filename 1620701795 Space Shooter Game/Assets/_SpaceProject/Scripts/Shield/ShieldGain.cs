using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Shield
{
    public class ShieldGain : MonoBehaviour
    {
        public GameObject ShieldLevel1;
        public GameObject ShieldLevel2;
        public GameObject ShieldLevel3;
        
        public static ShieldGain Instance { get; private set; }

        public void Awake()
        {
            Debug.Assert(ShieldLevel1 != null , "ShieldLevel1 can't be null");
            Debug.Assert(ShieldLevel2 != null , "ShieldLevel2 can't be null");
            Debug.Assert(ShieldLevel3 != null , "ShieldLevel3 can't be null");

            if (Instance == null)
            {
                Instance =this;
            }
        }
        
        private void Start()
        {
            ShieldLevel1.SetActive(false);
            ShieldLevel2.SetActive(false);
            ShieldLevel3.SetActive(false);
        }
    }
}