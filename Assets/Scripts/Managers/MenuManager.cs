using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] SceneAsset _sceneDestination;

    public void StartButton()
    {
        SceneManager.LoadScene(_sceneDestination.name);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
