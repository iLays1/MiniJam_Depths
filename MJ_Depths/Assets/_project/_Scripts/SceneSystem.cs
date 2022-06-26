using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneSystem : SingletonPersistent<SceneSystem>
{
    public Image fadeImage;
    public int[] levelIndexs;
    public int currentLevel = 0;
    bool loading = false;

    public void LoadFirstLevel()
    {
        currentLevel = 0;
        LoadScene(levelIndexs[currentLevel]);
    }
    public void LoadNextLevel(float delay = 0f)
    {
        currentLevel++;
        LoadScene(levelIndexs[currentLevel], delay);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScene(0);
        }
    }

    public void LoadScene(int i, float delay = 0f)
    {
        if (loading) return;
        StartCoroutine(LoadSceneCoroutine(i, delay));
    }

    IEnumerator LoadSceneCoroutine(int i, float delay)
    {
        yield return new WaitForSeconds(delay);

        RunDataSystem.Instance.SaveData();
        DOTween.KillAll();
        loading = true;
        fadeImage.DOFade(1f, 0.3f);

        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(i);
        fadeImage.DOFade(0f, 0.3f);
        loading = false;
    }
}
