using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public TextMeshProUGUI textMesh;

    private void Awake()
    {
        instance = this;
        textMesh.text = "";
    }

    public void PlayString(DialogueString dstring)
    {
        StopAllCoroutines();
        textMesh.DOKill();
        textMesh.text = "";
        StartCoroutine(StringCoroutine(dstring));
    }
    IEnumerator StringCoroutine(DialogueString dstring)
    {
        foreach(var sentence in dstring.sentences)
        {
            textMesh.text = "";
            StartCoroutine(PlayTextCoroutine(sentence.text, sentence.speed, sentence.holdTime));
            
            yield return new WaitForSeconds(sentence.speed + sentence.holdTime + 0.3f);
        }
    }
    public void PlayText(string text, float speed, float holdTime = 1f)
    {
        StopAllCoroutines();
        textMesh.DOKill();
        textMesh.text = "";
        StartCoroutine(PlayTextCoroutine(text,speed, holdTime));
    }
    IEnumerator PlayTextCoroutine(string text, float speed, float holdTime)
    {
        textMesh.DOText(text,speed).SetEase(Ease.Linear);
        StartCoroutine(SoundCoroutine(speed));
        yield return new WaitForSeconds(speed + holdTime);
        textMesh.text = "";
    }
    IEnumerator SoundCoroutine(float speed)
    {
        float elapsed = 0;
        float voiceElapsed = 0;
        //AudioManager.instance.Play("Voice");
        while (elapsed < speed)
        {
            elapsed += Time.deltaTime;
            voiceElapsed += Time.deltaTime;
            
            if(voiceElapsed > 0.13f)
            {
                voiceElapsed = 0;
                //AudioManager.instance.PlayRandomPitch("Voice", 0.9f, 1.1f);
            }
            
            yield return null;
        }
    }
}
