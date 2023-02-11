using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BG
{
    public class ScrollingBackground : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed;
        [SerializeField] private float positionSpawn;
        private Vector2 startPosition;

        private void Awake()
        {
            Debug.Assert(positionSpawn > 0, "positionSpawn HP Can't be under the zero");
            Debug.Assert(scrollSpeed > 0, "scrollSpeed HP Can't be under the zero");
        }

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            var newPosition = Mathf.Repeat(Time.time * scrollSpeed, positionSpawn);
            transform.position = startPosition + Vector2.down * newPosition;
        }
    }
}
//Please Give A to 1620701795 senPai :)