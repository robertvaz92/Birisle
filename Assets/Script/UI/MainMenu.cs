using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UIMenuBase
{
    public TMPro.TextMeshProUGUI m_rowNumber;
    public TMPro.TextMeshProUGUI m_columnNumber;

    public Slider m_musicSlider;
    public Slider m_sfxSlider;

    private GameManager m_manger;

    public override void OnEnter()
    {
        base.OnEnter();
        m_manger = GameManager.Instance;
        RefreshGridNumbers();
        RefreshAudioSettings();

        m_manger.m_audioManager.PlayBGM();
    }


    private void RefreshGridNumbers()
    {
        m_rowNumber.text = m_manger.m_rows.ToString();
        m_columnNumber.text = m_manger.m_columns.ToString();
    }

    public void OnClickGridNumberIncrement(bool isX)
    {
        if (isX)
        {
            if (m_manger.m_rows >= 10)
            {
                m_manger.m_rows = 1;
            }
            m_manger.m_rows++;
        }
        else
        {
            if (m_manger.m_columns >= 10)
            {
                m_manger.m_columns = 1;
            }
            m_manger.m_columns++;
        }
        RefreshGridNumbers();
    }



    private void RefreshAudioSettings()
    {
        m_musicSlider.value = m_manger.m_isMusicEnabled ? 1 : 0;
        m_sfxSlider.value = m_manger.m_isSfxEnabled ? 1 : 0;
    }

    public void OnClickMusicOnOff()
    {
        m_manger.UpdateMusicOnOff(m_musicSlider.value == 1);
        RefreshAudioSettings();
    }

    public void OnClickSfxOnOff()
    {
        m_manger.UpdateSfxOnOff(m_sfxSlider.value == 1);
        RefreshAudioSettings();
    }

    

    public void OnClickStartGame()
    {
        m_manger.SaveSelectedGridSize();
        m_manger.SwitchMenu(MenuScreenTypes.GAME_PLAY_SCREEN);
    }

    public void OnClickResumeGame()
    {
        //Bring Confirmation Popup
    }

    public void OnClickConfirm()
    {
        //Update the Row/Column in the Game manager and start the game
    }
}
