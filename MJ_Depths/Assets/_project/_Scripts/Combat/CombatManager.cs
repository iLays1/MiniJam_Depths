using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.OnPlayerMove.AddListener(PlayerMoved);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            DOTween.KillAll();
            RunDataSystem.Instance.SaveData();
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PlayerMoved()
    {
        GameEvents.OnEndTurn.Invoke();
    }
}
