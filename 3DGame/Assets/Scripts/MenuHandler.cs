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

    private void ChangePanel(int value)
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
                panels[0].SetActive(true);
                panelState = PanelState.MainMenu;
                break;
        }
    }

    public void PlayButton()
    {
        gameState = GameStates.GameState;
        SwitchStates();
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

        ChangePanel(0);
    }

    private void Update()
    {
        if (Input.GetButton("Escape") && gameState == GameStates.GameState)
        {
            gameState = GameStates.MenuState;
            SwitchStates();
        }
    }

    private IEnumerator MenuState()
    {
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;

        ChangePanel(0);

        yield return null;
    }

    private IEnumerator GameState()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        ChangePanel(2);

        yield return null;
    }

    #region Menu and Game State

    public void SwitchStates()
    {
        switch (gameState)
        {
            case GameStates.MenuState:
                StartCoroutine(MenuState());
                break;
            case GameStates.GameState:
                StartCoroutine(GameState());
                break;
            default:
                StartCoroutine(MenuState());
                break;
        }
    }
    #endregion
}

