using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CliffLeeCL
{
    /// <summary>
    /// This singleton and facade class manage main processes in the game.
    /// </summary>
    public class GameManager : SingletonMono<GameManager>
    {
        public float ElapsedRoundTime { get; private set; }
        
        [SerializeField] float roundTime = 60.0f;
        [SerializeField] float chooseSkillTime = 10.0f;
        [SerializeField] int chooseSkillCount = 3;
        
        ProgressBar progressBar;
        List<ChooseSkillUI> chooseSkillUIList;
        List<ChaserSkill> chaserSkillList;
        List<EscaperSkill> escaperSkillList;
        float currentChooseSkillCount = 0;
        bool isGameOver = false;

        public UIManger uiManger;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
            EventManager.Instance.onChooseChaserSkill += OnChooseChaserSkill;
            EventManager.Instance.onChooseEscaperSkill += OnChooseEscaperSkill;
            EventManager.Instance.onGameOver += OnGameOver;
            EventManager.Instance.onReStart += Restart;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (!isGameOver)
            {
                ElapsedRoundTime += Time.deltaTime;
                var nextChooseSkillTime = (currentChooseSkillCount + 1f) / (chooseSkillCount + 1f) * roundTime;
                if (ElapsedRoundTime >= nextChooseSkillTime && currentChooseSkillCount < chooseSkillCount)
                {
                    foreach (var chooseSkill in chooseSkillUIList)
                    {
                        chooseSkill.Init(chaserSkillList, escaperSkillList, chooseSkillTime);
                    }
                    currentChooseSkillCount++;
                }
                else if(ElapsedRoundTime >= roundTime)
                {
                    OnRoundTimeIsUp();
                    return;
                }
                progressBar.SetProgress(ElapsedRoundTime / roundTime);
            }
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled () or inactive.
        /// </summary>
        void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            EventManager.Instance.onChooseChaserSkill -= OnChooseChaserSkill;
            EventManager.Instance.onChooseEscaperSkill -= OnChooseEscaperSkill;
            EventManager.Instance.onGameOver -= OnGameOver;
        }
  
        private void OnChooseChaserSkill(ChaserSkill skill)
        {
            chaserSkillList.Add(skill);
        }
        
        private void OnChooseEscaperSkill(EscaperSkill skill)
        {
            escaperSkillList.Add(skill);
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
            ElapsedRoundTime = 0.0f;
            isGameOver = false;
            progressBar = FindObjectOfType<ProgressBar>();
            progressBar.GenerateSkillTrigger(chooseSkillCount);
            chooseSkillUIList = new List<ChooseSkillUI>(FindObjectsOfType<ChooseSkillUI>());
            chaserSkillList = new List<ChaserSkill>();
            escaperSkillList = new List<EscaperSkill>();
            EventManager.Instance.OnGameStart();
            uiManger = FindObjectOfType<UIManger>();
            currentChooseSkillCount = 0;
        }

        void OnRoundTimeIsUp()
        {
            uiManger.GG.SetActive(true);
            uiManger.eWin.SetActive(true);
            EventManager.Instance.OnGameOver();
        }

        void OnGameOver()
        {
            isGameOver = true;
            Time.timeScale = 0.0f;
        }
        public void Restart()
        {
            Debug.LogError("LoadScene Main");
            isGameOver = false;
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Main");
        }
    }
}
