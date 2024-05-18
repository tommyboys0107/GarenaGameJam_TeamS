﻿using System;
using System.Collections.Generic;
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
        public float ElapsedRoundTime { get; private set; }
        
        [SerializeField] float roundTime = 60.0f;
        [SerializeField] float chooseSkillTime = 10.0f;
        [SerializeField] int chooseSkillCount = 3;
        
        ProgressBar progressBar;
        List<ChooseSkillUI> chooseSkillUIList;
        List<ChaserSkill> chaserSkillList;
        List<EscaperSkill> escaperSkillList;
        float currentChooseSkillIndex = 0;
        bool isGameOver = false;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
            EventManager.Instance.onGameOver += OnGameOver;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (!isGameOver)
            {
                ElapsedRoundTime += Time.deltaTime;
                var nextChooseSkillTime = (currentChooseSkillIndex + 1f) / (chooseSkillCount + 1f) * roundTime;
                if (ElapsedRoundTime >= nextChooseSkillTime)
                {
                    foreach (var chooseSkill in chooseSkillUIList)
                    {
                        chooseSkill.Init(chaserSkillList, escaperSkillList, chooseSkillTime);
                    }
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
            EventManager.Instance.onGameOver -= OnGameOver;
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
        }

        void OnRoundTimeIsUp()
        {
            EventManager.Instance.OnGameOver();
        }

        void OnGameOver()
        {
            isGameOver = true;
            Time.timeScale = 0.0f;
        }
    }
}