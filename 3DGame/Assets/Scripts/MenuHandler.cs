using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { MenuState, GameState }

public enum PanelState { MainMenu, Settings, PlayerHUD }

public class MenuHandler : MonoBehaviour
{
    public GameStates gameState;

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

    #region Panels
    public GameObject[] panels;

    public PanelState panelState;

    public void ChangePanel(int value)
    {
        panelState = (PanelState)value;

        switch (panelState)
        {
            case PanelState.MainMenu:
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].SetActive(false);
                }
                panels[0].SetActive(true);
                break;

            case PanelState.Settings:
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].SetActive(false);
                }
                panels[1].SetActive(true);
                break;

            case PanelState.PlayerHUD:
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].SetActive(false);
                }
                panels[2].SetActive(true);
                break;

            default:
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].SetActive(false);
                }
                panels[2].SetActive(true);
                panelState = PanelState.PlayerHUD;
                break;
        }
    }

    public void PlayButton()
    {
        gameState = GameStates.GameState;
        SwtichStates();
        Debug.Log("Button Pressed");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    #endregion

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
        if (Input.GetButtonDown("Escape") && gameState == GameStates.GameState)
        {
            gameState = GameStates.MenuState;
            ChangePanel(0);
            SwtichStates();
            Debug.Log("Switched to Main Menu");
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

