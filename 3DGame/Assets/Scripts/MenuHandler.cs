using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { MenuState, GameState }

public class MenuHandler : MonoBehaviour
{
    public GameStates gameState;

    public GameObject[] panels;

    private static MenuHandler _menuManager;
    public static MenuHandler menuHandlerInstance
    {
        get => _menuManager;
        private set
        {
            if (_menuManager == null)
            {
                _menuManager = value;
            }
            else if (_menuManager != value)
            {
                Debug.Log($"{nameof(MenuHandler)} instance already exists. Destroy duplicate. [insert Highlander quote]");
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        menuHandlerInstance = this;
    }

    private void Start()
    {
        gameState = GameStates.MenuState;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            if (gameState == GameStates.GameState)
            {
                gameState = GameStates.MenuState;
                SwtichStates();
            }
            else
            {
                gameState = GameStates.GameState;
                SwtichStates();
            }
        }
    }

    public void SwtichStates()
    {
        switch (gameState)
        {
            case GameStates.MenuState:
                if (Cursor.visible == false)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
            case GameStates.GameState:
                if (Cursor.visible == true)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;
            default:
                if (Cursor.visible == true)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;
        }
    }
}

