using System;
using System.Collections.Generic;
using UnityEngine;



public class EventManager : MonoBehaviour
{
    public enum GameEvent
    {
        // UI
        UI_ButtonClicked,
        UI_PanelOpened,
        //GamePlay
        GamePlay_EnemyDied,
        GamePlay_LevelComplete,
        //Menu
        Menu_StartGame,
        Menu_QuitGame,
        //SFX
        SFX_PlayClick,
        SFX_PlayExplosion,
        // Stats
        Stats_OnValueChange_PhysicalHealth,
        Stats_OnValueChange_MentalHealth,
        Stats_OnValueChange_Poise,
        Stats_OnValueZero_PhysicalHealth,
        Stats_OnValueZero_MentalHealth,
        Stats_OnValueZero_Poise,
        GamePlay_PlayerDied,
    }
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

    public void InvokeEvent(GameEvent eventType)
    {
        eventDictionary[eventType]?.Invoke();
    }

    public void ClearAllListeners()
    {
        InitializeAllEvents(); // Reset all to null
    }
}
