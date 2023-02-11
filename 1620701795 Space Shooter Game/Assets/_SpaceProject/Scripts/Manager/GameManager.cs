using System;
using Shield;
using SpaceShip;
using UnityEngine;
using Random = UnityEngine.Random;
using Dialog;


namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerSpaceShip playerSpaceShip;
        [SerializeField] private EnemySpaceShip enemySpaceShip;
        [SerializeField] private EnemySpaceShip enemyLevel2SpaceShip;
        [SerializeField] private EnemySpaceShip enemyLevel3SpaceShip;
        [SerializeField] private LevelDialog levelDialog;
        
        public event Action OnRestarted;
        public event Action OnGameEnded;

        [SerializeField] private int playerSpaceShipHp = 100;
        [SerializeField] private int playerSpaceShipMoveSpeed = 6;
        [SerializeField] private int enemySpaceShipHp = 100;
        [SerializeField] private int enemySpaceShipMoveSpeed = 2;
        [SerializeField] private int enemyLevel2SpaceShipHp = 200;
        [SerializeField] private int enemyLevel2SpaceShipMoveSpeed = 3;
        [SerializeField] private int enemyLevel3SpaceShipHp = 300;
        [SerializeField] private int enemyLevel3SpaceShipMoveSpeed = 3;

        private bool checkUpdate; //used for control Update
        private float randomEnemySpawn; //for random Range but TODO : dont know why can't implement Range here
        
        [NonSerialized] public PlayerSpaceShip PlayerSpawned; //to define the new playerShipHP and playerPosition

        private EnemySpaceShip enemyLevel1; //to define the new EnemyShipHP and EnemyPosition TODO: for Use Later
        private EnemySpaceShip enemyLevel2;
        private EnemySpaceShip enemyLevel3;
        
        public static GameManager Instance { get; private set; } //Singleton Pattern

        private void Awake()
        {
            //CheckList
            Debug.Assert(levelDialog != null, "levelDialog can't be null");
            Debug.Assert(enemyLevel2SpaceShip != null, "enemyLevel2SpaceShip can't be null");
            Debug.Assert(enemyLevel3SpaceShip != null, "enemyLevel3SpaceShip can't be null");
            Debug.Assert(playerSpaceShip != null, "playerSpaceShip Can't be null");
            Debug.Assert(enemySpaceShip != null, "enemySpaceShip Can't be null");
            Debug.Assert(playerSpaceShipHp > 0, "playerSpaceShipHp HP Can't be under the zero");
            Debug.Assert(playerSpaceShipMoveSpeed > 0, "playerSpaceShipMoveSpeed Speed Can't be under the zero");
            Debug.Assert(enemySpaceShipHp > 0, "enemySpaceShipHp HP Can't be under the zero");
            Debug.Assert(enemySpaceShipMoveSpeed > 0, "enemySpaceShip Move Speed Can't be under the zero");
            Debug.Assert(enemyLevel2SpaceShipMoveSpeed > 0, "enemyLevel2SpaceShip Move Speed Can't be under the zero");
            Debug.Assert(enemyLevel2SpaceShipHp > 0, "enemyLevel2SpaceShipHp Can't be under the zero");
            Debug.Assert(enemyLevel3SpaceShipMoveSpeed > 0, "enemyLevel3SpaceShip Move Speed Can't be under the zero");
            Debug.Assert(enemyLevel3SpaceShipHp > 0, "enemyLevel3SpaceShipHp Can't be under the zero");

            //Singleton Pattern
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
            checkUpdate = true; //To start update loop
            OnLevelOne();
            ScoreManager.Instance.Init(); //this used for send GameManager to ScoreManager
            SpawnPlayerSpaceShip();
            InvokeRepeating("SpawnEnemySpaceShip", 1, 1);
        }

        private void OnLevelOne()
        {
            levelDialog.GameStart();
            UIManager.Instance.ShowLevel(1);
        }

        private void OnLevelTwo()
        {
            levelDialog.NextSentence();
            CancelInvoke("SpawnEnemySpaceShip");
            UIManager.Instance.ShowLevel(2);
            InvokeRepeating("SpawnEnemyLevel2SpaceShip", 1, 1);
        }

        private void OnLevelThree()
        {
            levelDialog.NextSentence();
            CancelInvoke("SpawnEnemyLevel2SpaceShip");
            UIManager.Instance.ShowLevel(3);
            InvokeRepeating("SpawnEnemyLevel3SpaceShip", 1, 2);
        }

        private void OnLevelFour()
        {
            levelDialog.NextSentence();
            UIManager.Instance.ShowLevel(4);
            InvokeRepeating("SpawnEnemySpaceShip", 3, 2);
            InvokeRepeating("SpawnEnemyLevel2SpaceShip", 2, 3);
        }

        private void GameEnded()
        {
            OnGameEnded?.Invoke();
        }

        private void SpawnPlayerSpaceShip()
        {
            PlayerSpawned = Instantiate(playerSpaceShip);
            PlayerSpawned.Init(playerSpaceShipHp, playerSpaceShipMoveSpeed);
            PlayerSpawned.OnExploded += OnPlayerSpaceSpawnedExploded;
            // += is if Spaceship exploded call OnPlayerSpaceShipExploded
        }

        private void OnPlayerSpaceSpawnedExploded()
        {
            checkUpdate = false; //To stop update loop
            LevelDialog.Instance.HideNextLevelText(false); //If player died between dialog show it should disable
            SoundManager.Instance.Play(SoundManager.Sound.PlayerDestroyed);
            CancelInvoke("SpawnEnemySpaceShip");//Cancel spawn
            CancelInvoke("SpawnEnemyLevel2SpaceShip");
            CancelInvoke("SpawnEnemyLevel3SpaceShip");
            OnDestroyRemainingEnemyShip();//Destroy all ship
            Invoke("GameEnded", 2);//Wait for Show Play explode
        }

        private void SpawnEnemySpaceShip()
        {
            randomEnemySpawn = Random.Range(-10f, 10f);//Random Spawn point
            enemyLevel1 = Instantiate(enemySpaceShip, new Vector2(randomEnemySpawn, 6), Quaternion.identity);
            //Instantiate use to spawn Prefab
            enemyLevel1.Init(enemySpaceShipHp, enemySpaceShipMoveSpeed);
            enemyLevel1.OnExploded += OnEnemySpaceShipExploded;
        }

        private void SpawnEnemyLevel2SpaceShip()
        {
            randomEnemySpawn = Random.Range(-10f, 10f);
            enemyLevel2 = Instantiate(enemyLevel2SpaceShip, new Vector2(randomEnemySpawn, 6), Quaternion.identity);
            enemyLevel2.Init(enemyLevel2SpaceShipHp, enemyLevel2SpaceShipMoveSpeed);
            enemyLevel2.OnExploded += OnEnemySpaceShipExploded;
        }

        private void SpawnEnemyLevel3SpaceShip()
        {
            randomEnemySpawn = Random.Range(-10f, 10f);
            enemyLevel3 = Instantiate(enemyLevel3SpaceShip, new Vector2(randomEnemySpawn, 6), Quaternion.identity);
            enemyLevel3.Init(enemyLevel3SpaceShipHp, enemyLevel3SpaceShipMoveSpeed);
            enemyLevel3.OnExploded += OnEnemySpaceShipExploded;
        }


        private void OnDestroyRemainingEnemyShip()
        {
            var remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            var remainingBullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (var enemy in remainingEnemies)
            {
                Destroy(enemy);
            }

            foreach (var bullet in remainingBullets)
            {
                Destroy(bullet);
            }
        }

        private void OnEnemySpaceShipExploded()
        {
            SoundManager.Instance.Play(SoundManager.Sound.EnemyDestroyed);
            ScoreManager.Instance.SetScore();
            //Level chang system
            if (ScoreManager.Instance.Score == 10)
            {
                OnLevelTwo();
            }
            else if (ScoreManager.Instance.Score == 30)
            {
                OnLevelThree();
            }
            else if (ScoreManager.Instance.Score == 50)
            {
                OnLevelFour();
            }

            //It's shield gain system
            if (ScoreManager.Instance.Score % 5 == 0 && ScoreManager.Instance.Score != 0)
            {
                if (playerSpaceShipHp < 400)
                {
                    playerSpaceShipHp += 100;
                    PlayerSpawned.Init(playerSpaceShipHp, playerSpaceShipMoveSpeed);
                }
            }
            ShieldStack();//Show how many shield active
        }

        public void Restart()
        {
            OnRestarted?.Invoke(); //Invoke will call the listener to work 
        }

        private void Update()
        {
            //used for Update when GameStarted because it'll null ref if Update run in start dialog
            if (checkUpdate)
            {
                ShieldStack();
            }
        }

        //Shield Display system
        private void ShieldStack()
        {
            if (PlayerSpawned.Hp == 200)
            {
                playerSpaceShipHp = 200;
                ShieldGain.Instance.ShieldLevel1.gameObject.SetActive(true);
                ShieldGain.Instance.ShieldLevel2.gameObject.SetActive(false);
            }
            else if (PlayerSpawned.Hp == 300)
            {
                playerSpaceShipHp = 300;
                ShieldGain.Instance.ShieldLevel1.gameObject.SetActive(false);
                ShieldGain.Instance.ShieldLevel2.gameObject.SetActive(true);
                ShieldGain.Instance.ShieldLevel3.gameObject.SetActive(false);
            }
            else if (PlayerSpawned.Hp == 400)
            {
                playerSpaceShipHp = 400;
                ShieldGain.Instance.ShieldLevel2.gameObject.SetActive(false);
                ShieldGain.Instance.ShieldLevel3.gameObject.SetActive(true);
            }
            else
            {
                playerSpaceShipHp = 100;
                ShieldGain.Instance.ShieldLevel1.gameObject.SetActive(false);
                ShieldGain.Instance.ShieldLevel2.gameObject.SetActive(false);
                ShieldGain.Instance.ShieldLevel3.gameObject.SetActive(false);
            }
        }
    }
}
//Message form 1620701795 Please Give Me A senpai :)