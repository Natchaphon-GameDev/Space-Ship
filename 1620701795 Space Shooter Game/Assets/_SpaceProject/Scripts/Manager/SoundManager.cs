using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundClip[] soundClips;

        public static SoundManager Instance { get; private set; }

        public void Awake()
        {
            Debug.Assert(soundClips != null && soundClips.Length != 0, "Sound clips need to setup");

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this); //Do not Destroy this when reset hierarchy
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            //Play BGM
            Play(Sound.BGM);
        }

        public enum Sound
        {
            //SoundClip
            BGM,
            PlayerFire,
            EnemyFire,
            PlayerDestroyed,
            EnemyDestroyed,
            ShieldDown,
            ChangeLevel
        }

        [Serializable]
        public class SoundClip
        {
            //Init of every SoundClip
            public Sound Sound;
            public AudioClip AudioClip;
            [Range(0, 2)] public float SoundVolume;
            public bool Loop = false;
            [HideInInspector] public AudioSource AudioSource;
        }

        public void Play(Sound sound)
        {
            //play Sound system
            var soundClip = GetSoundClip(sound);
            if (soundClip.AudioSource == null)
            {
                soundClip.AudioSource = gameObject.AddComponent<AudioSource>();
            }
            soundClip.AudioSource.clip = soundClip.AudioClip;
            soundClip.AudioSource.volume = soundClip.SoundVolume;
            soundClip.AudioSource.loop = soundClip.Loop;
            soundClip.AudioSource.Play();
        }

        private SoundClip GetSoundClip(Sound sound)
        {
            foreach (var soundClip in soundClips)
            {
                if (soundClip.Sound == sound)
                {
                    return soundClip;
                }
            }
            return null;
        }
    }
}
//Please Give A to 1620701795 senPai :)