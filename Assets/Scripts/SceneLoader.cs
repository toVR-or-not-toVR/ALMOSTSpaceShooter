using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 1f;
    public Animator transition_1;


    IEnumerator WaitAndLoad(string scene_name)
    {
        Debug.Log(delayInSeconds);
        Time.timeScale = 1f;
        transition_1.SetTrigger("End");
        
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(scene_name);
    }
    public void LoadGame()
    {
        if (FindObjectOfType<GameSession>() != null)
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
        
        StartCoroutine(WaitAndLoad("Game"));
    }
    public void LoadShop()
    {
        StartCoroutine(WaitAndLoad("Shop Scene"));
    }
    public void LoadStartMenu()
    {
        StartCoroutine(WaitAndLoad("Start Scene"));
    }

    public void QuitGame()
    {

        PlayerPrefs.DeleteAll();

        Application.Quit();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("Finish Scene"));
    }



    public void LoadInstructions()
    {
        StartCoroutine(WaitAndLoad("Instructions"));
    }


}
