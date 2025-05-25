using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMapChanger : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameManager gameManager;
    public static ActionMapChanger instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public static ActionMapChanger GetInstance()
    {
        return instance;
    }

    private void HandleGameStateChanged(GameManager.GameState state)
    {
        Debug.Log("ActionMap changed to " + state);
        switch (state)
        {
            case GameManager.GameState.UI:
                playerInput.SwitchCurrentActionMap("UI");
                break;
            case GameManager.GameState.Dialogue:
                playerInput.SwitchCurrentActionMap("Dialogue");
                break;
            case GameManager.GameState.Gameplay:
                playerInput.SwitchCurrentActionMap("Gameplay");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void OnEnable()
    {
        try
        {
            gameManager.OnGameStateChanged += HandleGameStateChanged;
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    private void OnDisable()
    {
        try
        {
            gameManager.OnGameStateChanged -= HandleGameStateChanged;
        }
        catch(NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
    }
}