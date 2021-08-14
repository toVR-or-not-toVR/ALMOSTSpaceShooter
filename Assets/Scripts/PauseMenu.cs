using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] float currentTime;
    [SerializeField] GameObject pauseMenuPanel;

    private void Awake()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);

    }


    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }


    public void Resume()
    {
        currentTime = 3f;
        StartCoroutine(CountAndGo());
    }

    IEnumerator CountAndGo()
    {

        while (currentTime >= 0)
        {
            yield return new WaitForSecondsRealtime(1);
            
            timeText.text = currentTime.ToString("0");
            currentTime -= 1;
        }

        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
        timeText.text = "";
    }

    public void BackMenu()
    {
        FindObjectOfType<SceneLoader>().LoadStartMenu();
    }
  

}
