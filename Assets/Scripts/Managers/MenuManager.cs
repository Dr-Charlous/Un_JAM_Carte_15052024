using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject TitleUi;
    [SerializeField] GameObject ContinueUI;
    [SerializeField] GameObject LoseUI;

    public void ContinueButton(TimeManager timeManager)
    {
        timeManager.Initialize();
        HideUi();
    }

    public void ChangeSceneButton(SceneAsset sceneDestination)
    {
        SceneManager.LoadScene(sceneDestination.name);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    void HideUi()
    {
        TitleUi.SetActive(false);
        ContinueUI.SetActive(false);
        LoseUI.SetActive(false);
    }

    public void ShowContinueUI()
    {
        TitleUi.SetActive(true);
        ContinueUI.SetActive(true);
    }

    public void ShowLoseUI()
    {
        TitleUi.SetActive(true);
        LoseUI.SetActive(true);
    }
}
