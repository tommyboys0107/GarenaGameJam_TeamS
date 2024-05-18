using System;
using UnityEngine;

namespace CliffLeeCL
{
    /// <summary>
    /// This singleton class manage all events in the game.
    /// </summary>
    /// <code>
    /// // Usage in other class example:\n
    /// void Start(){\n
    ///     EventManager.instance.onGameOver += LocalFunction;\n
    /// }\n
    /// \n
    /// // If OnEnable function will cause error, try listen to events in Start function.\n
    /// void OnEnable(){\n
    ///     EventManager.instance.onGameOver += LocalFunction;\n
    /// }\n
    /// \n
    /// void OnDisable(){\n
    ///     EventManager.instance.onGameOver -= LocalFunction;\n
    /// }\n
    /// \n
    /// void LocalFunction(){\n
    ///     //Do something here\n
    /// }
    /// </code>
    public class EventManager : Singleton<EventManager>
    {
        /// <summary>
        /// The event is called when game over.
        /// </summary>
        public event Action onGameOver;
        public event Action onGameStart;
        
        public event Action<ChaserSkill> onChooseChaserSkill;
        public event Action<EscaperSkill> onChooseEscaperSkill;

        public event Action<EscaperSkill,int> onUseEscaperSkill;


        public void OnGameOver()
        {
            onGameOver?.Invoke();
            Debug.Log("OnGameOver event is invoked!");
        }
        
        public void OnGameStart()
        {
            onGameStart?.Invoke();
            Debug.Log("OnGameStart event is invoked!");
        }
        
        public void OnChooseChaserSkill(ChaserSkill skill)
        {
            onChooseChaserSkill?.Invoke(skill);
            Debug.Log($"OnChooseChaserSkill {skill} event is invoked!");
        }
        
        public void OnChooseEscaperSkill(EscaperSkill skill)
        {
            onChooseEscaperSkill?.Invoke(skill);
            Debug.Log($"OnChooseEscaperSkill {skill} event is invoked!");
        }


        public void OnUseEscaperSkill(EscaperSkill skill, int coolTime)
        {
            onUseEscaperSkill?.Invoke(skill,coolTime);
            Debug.Log($"OnChooseEscaperSkill {skill} cd:{coolTime} event is invoked!");
        }



    }
}

