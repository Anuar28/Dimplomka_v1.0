using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    public static FadeCanvas fader;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image loadingBar;
    [SerializeField] private float changeValue;
    [SerializeField] private float waitTime;
    [SerializeField] private bool fadeStarted = false;
    private void Awake()
    {
        if (fader == null)
        {
            fader = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);
    }
    void Start()
    {
        StartCoroutine(FadeIn());
    }
    public void FaderLoadString(string levelName)
    {
        StartCoroutine(FadeOutString(levelName));
    }
    public void FaderLoadInt(int levelInd)
    {
        StartCoroutine(FadeOutInt(levelInd));
    }
    IEnumerator FadeIn()
    { 
        loadingScreen.SetActive(false);
        fadeStarted = false;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= changeValue;
            yield return new WaitForSeconds(waitTime);
            Debug.Log("while from FADE IN");
        }
     }

    IEnumerator FadeOutString(string levelName)
    {
        if (canvasGroup.alpha != 0)
            yield break;
        if (fadeStarted)
            yield break;
        fadeStarted = true;
        while (canvasGroup.alpha < 1) 
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
            Debug.Log("while from FADE OUT");
        }
        //SceneManager.LoadScene(levelName);
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName);
        ao.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        loadingBar.fillAmount = 0;
        while (ao.isDone == false)
        {
            loadingBar.fillAmount = ao.progress / 0.9f;
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOutInt(int levelIndex)
    {
        if (fadeStarted)
            yield break;
        fadeStarted = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelIndex);
        ao.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        loadingBar.fillAmount = 0;
        while (ao.isDone == false)
        {
            loadingBar.fillAmount = ao.progress / 0.9f;
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
        StartCoroutine(FadeIn());
    }
}
