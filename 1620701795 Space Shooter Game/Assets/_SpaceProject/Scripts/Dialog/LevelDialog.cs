using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;

namespace Dialog
{
    public class LevelDialog : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textDisplay;
        [SerializeField] private string[] sentences;

        public int Index { get; private set; }
        [SerializeField] private float typingSpeed;

        public static LevelDialog Instance { get; private set; }

        private void Awake()
        {
            Debug.Assert(textDisplay != null, "textDisplay Can't be null");
            Debug.Assert(sentences.Length > 0, "sentences Need to be Fill");

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void GameStart()
        {
            SoundManager.Instance.Play(SoundManager.Sound.ChangeLevel);
            Index = 0; //To reset the nextLevelText
            textDisplay.text = "";
            HideNextLevelText(true);
            StartCoroutine(Waiter());
        }

        IEnumerator Waiter()
        {
            //Show text for 3 second 
            StartCoroutine(Type());
            yield return new WaitForSeconds(3);
            HideNextLevelText(false);
        }

        IEnumerator Type()
        {
            foreach (var letter in sentences[Index].ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        public void NextSentence()
        {
            SoundManager.Instance.Play(SoundManager.Sound.ChangeLevel);
            if (Index < sentences.Length - 1)
            {
                Index++;
                HideNextLevelText(true);
                textDisplay.text = "";
                StartCoroutine(Waiter());
            }
            else
            {
                textDisplay.text = "";
                HideNextLevelText(false);
            }
        }

        public void HideNextLevelText(bool onAndOff)
        {
            if (onAndOff == false)
            {
                textDisplay.gameObject.SetActive(false);
            }
            else
            {
                textDisplay.gameObject.SetActive(true);
            }
        }
    }
}
//Please Give A to 1620701795 senPai :)