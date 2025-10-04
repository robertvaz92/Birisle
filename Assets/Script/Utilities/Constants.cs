using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuScreenTypes
{
    NONE,
    SPLASH_SCREEN,
    MAIN_MENU_SCREEN,
    GAME_PLAY_SCREEN
}

public class Constants : MonoBehaviour
{
    public static readonly string PlayerPrefRows = "ROW_SETTINGS_PREF";
    public static readonly string PlayerPrefColumns = "COLUMN_SETTINGS_PREF";

    public static readonly string PlayerPrefMusic = "MUSIC_SETTINGS_PREF";
    public static readonly string PlayerPrefSfx = "SFX_SETTINGS_PREF";

    public static readonly string GameSaveFileName = "CardsSavedData.txt";

}
