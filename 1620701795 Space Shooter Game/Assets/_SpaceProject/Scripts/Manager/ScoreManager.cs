using System;
using TMPro;
using UnityEngine;

namespace Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        public int Score { get; private set; } //used for control score
        
        private int highestScore = 0; //used for control highest score
        
        public static ScoreManager Instance { get; private set; }
        
        private void Awake()
        {
            Debug.Assert(scoreText != null, "scoreText can't be null");
            
            scoreText.gameObject.SetActive(false);

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
            GameManager.Instance.OnRestarted += OnRestarted;
            GameManager.Instance.OnGameEnded += HideScore;
            GameManager.Instance.OnGameEnded += ResetScore;
        }

        public void Init()
        {
            //+= OnRestarted used to call method in ScoreManager when OnRestart.Invoke()
            scoreText.gameObject.SetActive(true);
            ResetScore();
        }

        public void SetScore()
        {
            Score++;
            scoreText.text = $"Score : {Score}"; //set text 
        }

        public void ResetScore()
        {
            Score = 0;
            scoreText.text = $"Score : {Score}";
        }
        
        private void OnRestarted()
        {
            HideScore();
            ResetScore();
        }

        private void HideScore()
        {
            scoreText.gameObject.SetActive(false);
            SetHighScore();
        }

        private void SetHighScore()
        {
            if (Score > highestScore) //keep highest score system
            {
                highestScore = Score;
                UIManager.Instance.ShowHighScore(highestScore);
            }
        }
    }
}
//Please Give A to 1620701795 senPai :)