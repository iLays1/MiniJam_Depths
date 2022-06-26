using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    private void Start()
    {
        RunDataSystem.Instance.ResetData();
        MusicSystem.Instance.PlaySong(Song.MenuTheme);
    }
}
