using System;
using UnityEngine;

public class GameManager : MonoBehaviour

    
{
    public static GameManager instance;
    public event Action<GameState> OnGameStateChanged;

    private GameState currentGameState = GameState.Gameplay;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public static GameManager GetInstace()
        { return instance; }
    public void ChangeState(GameState state)
    {
        if (state == currentGameState)
            return;

        switch (state)
        {
            case GameState.UI:
                EnterUIState();
                break;
            case GameState.Dialogue:
                EnterDialogueState();
                break;
            case GameState.Gameplay:
                EnterGameplayState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        currentGameState = state;
        OnGameStateChanged?.Invoke(currentGameState);
    }

    private void EnterUIState()
    {
        Time.timeScale = 1f;
    }

    private void EnterDialogueState()
    {
        Time.timeScale = 1f;
    }

    private void EnterGameplayState()
    {
        Time.timeScale = 1f;
    }


    public enum GameState
    {
        UI,
        Dialogue,
        Gameplay
    }
}