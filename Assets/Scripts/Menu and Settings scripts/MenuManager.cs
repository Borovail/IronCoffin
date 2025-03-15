using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private string SceneForLoad;

    public void PlayGame()
    {
    SceneManager.LoadScene(SceneForLoad);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
