using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace CliffLeeCL
{
    /// <summary>
    /// This singleton and facade class manage main processes in the game.
    /// </summary>
    public class GameManager : SingletonMono<GameManager>
    {
        /// <summary>
        /// Define how long a single round is.
        /// </summary>
        public float roundTime = 60.0f;
        
        public int chooseSkillCount = 3;
        
        public float ElapsedTime { get; private set; }
        
        ProgressBar progressBar;

        /// <summary>
        /// Is true when the game is over.
        /// </summary>
        bool isGameOver = false;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (!isGameOver)
            {
                ElapsedTime += Time.deltaTime;
                if (ElapsedTime >= roundTime)
                {
                    OnRoundTimeIsUp();
                    return;
                }
                progressBar.SetProgress(ElapsedTime / roundTime);
            }
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled () or inactive.
        /// </summary>
        void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        /// <summary>
        /// This function is called after a new level was loaded.
        /// </summary>
        /// <param name="level">The index of the level that was loaded.</param>
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Time.timeScale = 1.0f;
            if (string.Compare(scene.name, "Main", StringComparison.Ordinal) == 0)
            {
                InitializeGame();
            }
        }
        
        void InitializeGame()
        {
            ElapsedTime = 0.0f;
            isGameOver = false;
            progressBar = FindObjectOfType<ProgressBar>();
            progressBar.GenerateSkillTrigger(chooseSkillCount);
        }

        void OnRoundTimeIsUp()
        {
            GameOver();
        }

        void GameOver()
        {
            isGameOver = true;
            EventManager.Instance.OnGameOver();
            Time.timeScale = 0.0f;
        }
    }
}
