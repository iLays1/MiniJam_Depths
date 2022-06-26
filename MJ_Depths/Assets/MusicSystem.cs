using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Song
{
    MenuTheme,
    BattleTheme,
    BattleClearTheme,
    GameoverTheme
}

public class MusicSystem : SingletonPersistent<MusicSystem>
{
    public float volume = 0.3f;
    public AudioSource menuThemeSource;
    public AudioSource battleSource;
    public AudioSource battleClearSource;
    public AudioSource gameoverSource;

    public void PlaySong(Song song)
    {
        menuThemeSource.DOFade(0f, 1f);
        battleSource.DOFade(0f, 1f);
        battleClearSource.DOFade(0f, 1f);
        gameoverSource.DOFade(0f, 1f);

        switch (song)
        {
            case Song.MenuTheme:
                menuThemeSource.DOKill();
                menuThemeSource.DOFade(volume, 1f);
                menuThemeSource.Play();
                break;
            case Song.BattleTheme:
                battleSource.DOKill();
                battleSource.DOFade(volume, 1f);
                break;
            case Song.BattleClearTheme:
                battleClearSource.DOKill();
                battleClearSource.DOFade(volume, 1f);
                break;
            case Song.GameoverTheme:
                gameoverSource.DOKill();
                gameoverSource.DOFade(volume, 1f);
                gameoverSource.Play();
                break;
        }
    }
}
