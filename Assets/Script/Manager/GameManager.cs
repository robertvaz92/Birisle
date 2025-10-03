using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public MenuScreenTypes m_startScreen = MenuScreenTypes.SPLASH_SCREEN;

    public UIMenuBase m_splashScreen;
    public UIMenuBase m_mainMenu;
    public UIMenuBase m_gameMenu;

    public UIMenuBase m_currentMenu { get; private set; }


    public int m_rows { get; set; }
    public int m_columns { get; set; }
    public bool m_isMusicEnabled { get; private set; }
    public bool m_isSfxEnabled { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_currentMenu = null;


        InitializeGridSettings();
        InitializeAudioSettings();
        InitializeMenu();
    }

///////////////////////////////////   Grid Settings  /////////////////////////////////////

    private void InitializeGridSettings()
    {
        m_rows = PlayerPrefs.GetInt(Constants.PlayerPrefRows, 2);
        m_columns = PlayerPrefs.GetInt(Constants.PlayerPrefColumns, 2);
    }

    public void SaveSelectedGridSize()
    {
        PlayerPrefs.SetInt(Constants.PlayerPrefRows, m_rows);
        PlayerPrefs.SetInt(Constants.PlayerPrefColumns, m_columns);
    }


///////////////////////////////////   Audio Settings  /////////////////////////////////////
    private void InitializeAudioSettings()
    {
        m_isMusicEnabled = PlayerPrefs.GetInt(Constants.PlayerPrefMusic, 1) == 1;
        m_isSfxEnabled = PlayerPrefs.GetInt(Constants.PlayerPrefSfx, 1) == 1;
    }

    public void UpdateMusicOnOff(bool isOn)
    {
        m_isMusicEnabled = isOn;
        PlayerPrefs.SetInt(Constants.PlayerPrefMusic, m_isMusicEnabled ? 1 : 0);
    }

    public void UpdateSfxOnOff(bool isOn)
    {
        m_isSfxEnabled = isOn;
        PlayerPrefs.SetInt(Constants.PlayerPrefSfx, m_isSfxEnabled ? 1 : 0);
    }

    ///////////////////////////////////   Menu  /////////////////////////////////////
    private void InitializeMenu()
    {
        m_splashScreen.Setup(MenuScreenTypes.SPLASH_SCREEN);
        m_mainMenu.Setup(MenuScreenTypes.MAIN_MENU_SCREEN);
        m_gameMenu.Setup(MenuScreenTypes.GAME_PLAY_SCREEN);

        SwitchMenu(m_startScreen);
    }

    public void SwitchMenu(MenuScreenTypes screen)
    {
        if (m_currentMenu != null)
        {
            if (m_currentMenu.m_menuScreenType == screen)
            {
                return;
            }
            m_currentMenu.Deactivate();
        }

        m_currentMenu = GetMenu(screen);
        m_currentMenu.Activate();
    }

    public UIMenuBase GetMenu(MenuScreenTypes screen)
    {
        UIMenuBase retVal = null;
        switch (screen)
        {
            case MenuScreenTypes.SPLASH_SCREEN:
                retVal = m_splashScreen;
                break;

            case MenuScreenTypes.MAIN_MENU_SCREEN:
                retVal = m_mainMenu;
                break;

            case MenuScreenTypes.GAME_PLAY_SCREEN:
                retVal = m_gameMenu;
                break;
        }
        return retVal;
    }
}
