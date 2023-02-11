using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private RectTransform startDialog;
        [SerializeField] private RectTransform ruleDialog;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button ruleButton;
        [SerializeField] private RectTransform endDialog;
        [SerializeField] private TextMeshProUGUI finalScore;
        [SerializeField] private TextMeshProUGUI showLevel;
        [SerializeField] private TextMeshProUGUI showHighestScore;
        
        private int countButtonRule = 0; //used for control Double click
        
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            Debug.Assert(startButton != null, "StartButton can't be null");
            Debug.Assert(showHighestScore != null, "showHighestScore can't be null");
            Debug.Assert(showLevel != null, "level can't be null");
            Debug.Assert(restartButton != null, "RestartButton can't be null");
            Debug.Assert(ruleButton != null, "ruleButton can't be null");
            Debug.Assert(ruleDialog != null, "ruleDialog can't be null");
            Debug.Assert(startDialog != null, "Dialog Can't be null");
            Debug.Assert(endDialog != null, "endDialog Can't be null");
            Debug.Assert(quitButton != null, "quitButton Can't be null");
            Debug.Assert(finalScore != null, "finalScore Can't be null");
            
            //Unity event ***To define a listener name that was done : use On + past tense(ed)***
            startButton.onClick.AddListener(OnStartButtonClicked);
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
            ruleButton.onClick.AddListener(OnRuleButtonClicked);
            
            //Hide other Dialog
            ruleDialog.gameObject.SetActive(false);
            endDialog.gameObject.SetActive(false);
            showLevel.gameObject.SetActive(false);

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            GameManager.Instance.OnGameEnded += EndUI;
        }

        private void OnStartButtonClicked()
        {
            showLevel.gameObject.SetActive(!false);
            startDialog.gameObject.SetActive(false); //SetActive(false) to close dialog
            GameManager.Instance.GameStart();
        }
        
        private void OnRuleButtonClicked()
        {
            //This is easiest to double clicked system that I can though
            countButtonRule++;
            if (countButtonRule % 2 == 1)
            {
                ruleDialog.gameObject.SetActive(true);
            }
            else
            {
                ruleDialog.gameObject.SetActive(false);
            }
        }
        
        private void OnQuitClicked()
        {
            //Quit Program
            Application.Quit();
        }
        
        private void OnRestartButtonClicked()
        {
            endDialog.gameObject.SetActive(false);
            startDialog.gameObject.SetActive(true);
            GameManager.Instance.Restart();
        }

        public void ShowLevel(int level)
        {
            showLevel.text = $"Level : {level}";
        }

        public void ShowHighScore(int highScore)
        {
            showHighestScore.text = $"Your Highest Score : {highScore}";
        }

        private void EndUI()
        {
            finalScore.text = $"Your score : {ScoreManager.Instance.Score}";
            showLevel.gameObject.SetActive(false);
            endDialog.gameObject.SetActive(true);
        }
    } 
}

