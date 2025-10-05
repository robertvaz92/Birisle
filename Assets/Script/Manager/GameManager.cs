using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Menu")]
    public MenuScreenTypes m_startScreen = MenuScreenTypes.SPLASH_SCREEN;

    public UIMenuBase m_splashScreen;
    public UIMenuBase m_mainMenu;
    public UIMenuBase m_gameMenu;

    public UIMenuBase m_currentMenu { get; private set; }

    [Header("Audio")]
    public AudioManager m_audioManager;

    public int m_rows { get; set; }
    public int m_columns { get; set; }
    public bool m_isMusicEnabled { get; private set; }
    public bool m_isSfxEnabled { get; private set; }
    public SavedData m_loadedGame { get; private set; }

    public bool m_isSavedGameExist => m_loadedGame != null;
    public bool m_isLoadingSavedGame = false;


    private FileManager m_fileManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_currentMenu = null;
        m_fileManager = new FileManager();
        LoadGameData();

        m_audioManager.Initialize();

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

        if (m_isMusicEnabled)
        {
            m_audioManager.PlayBGM();
        }
        else
        {
            m_audioManager.StopBGM();
        }
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

    ///////////////////////////////////   Data Save/Load  /////////////////////////////////////
    ///
    public void SaveGameData(CardGridManager cardManager)
    {
        SavedData saveData = new SavedData();
        saveData.AttemptCount = cardManager.m_totalFlipTries;
        saveData.Score = cardManager.m_scoreManager.m_baseScore;
        saveData.Rows = m_rows;
        saveData.Columns = m_columns;

        float a = (m_rows * m_columns / 2f);
        saveData.Progress = (int)(((a - cardManager.m_totalMatches) / a) * 100);

        saveData.CardIds = new List<SavedCardInfo>();
        for (int i = 0; i < cardManager.m_cards.Length; i++)
        {
            SavedCardInfo info = new SavedCardInfo();
            info.Id = cardManager.m_cards[i].m_id;
            info.IsActive = (cardManager.m_cards[i].m_updater.m_currentStateType == STATE_TYPE.OPEN || cardManager.m_cards[i].m_updater.m_currentStateType == STATE_TYPE.CLOSE);
            saveData.CardIds.Add(info);
        }

        m_fileManager.writeFile(Constants.GameSaveFileName, JsonUtility.ToJson(saveData));

        m_loadedGame = saveData;
    }

    public void LoadGameData()
    {
        string stringData = m_fileManager.readFile(Constants.GameSaveFileName);
        m_loadedGame = JsonUtility.FromJson<SavedData>(stringData);
        Debug.Log(stringData);
    }

    public void ClearSavedGame()
    {
        m_loadedGame = null;
        m_fileManager.writeFile(Constants.GameSaveFileName, JsonUtility.ToJson(m_loadedGame));
    }
}
