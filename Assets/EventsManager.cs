using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent
{
    UI_ButtonClicked,
    UI_PanelOpened,
    GamePlay_EnemyDied,
    GamePlay_LevelComplete,
    Menu_StartGame,
    Menu_QuitGame,
    SFX_PlayClick,
    SFX_PlayExplosion,
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private Dictionary<GameEvent, Action> eventDictionary = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeAllEvents();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAllEvents()
    {
        foreach (GameEvent evt in Enum.GetValues(typeof(GameEvent)))
        {
            eventDictionary[evt] = null;
        }
    }

    public void AddListener(GameEvent eventType, Action listener)
    {
        eventDictionary[eventType] += listener;
    }

    public void RemoveListener(GameEvent eventType, Action listener)
    {
        eventDictionary[eventType] -= listener;
    }

    public void TriggerEvent(GameEvent eventType)
    {
        eventDictionary[eventType]?.Invoke();
    }

    public void ClearAllListeners()
    {
        InitializeAllEvents(); // Reset all to null
    }
}
