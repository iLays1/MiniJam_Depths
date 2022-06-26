using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    public bool startRun;
    public int index;

    public void OnClicked()
    {
        if(startRun)
        {
            SceneSystem.Instance.LoadFirstLevel();
            return;
        }

        SceneSystem.Instance.LoadScene(index);
    }
}
