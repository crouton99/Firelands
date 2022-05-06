using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    private GameObject[] audioObjects;
    private GameObject[] pauseObjects;
    private GameObject[] tooltipObjects;

    [SerializeField] private Text tooltipTextbox;

    private void Start()
    {
        audioObjects = GameObject.FindGameObjectsWithTag("Audio");
        pauseObjects = GameObject.FindGameObjectsWithTag("Pause");
        tooltipObjects = GameObject.FindGameObjectsWithTag("Tooltips");
        
        hideAudioMenu();
        hidePause();
        hideTooltips();
    }

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Debug.Log("Exited");
        Application.Quit();
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1; 
        SceneManager.LoadScene(scene.name);
    }

    public void tooltipTextChanger(String tooltipText)
    {
        tooltipTextbox.text = tooltipText;
    }

    public void showAudioMenu()
    {
        foreach (GameObject f in audioObjects)
        {
            f.SetActive(true);
        }

        hidePause();
    }

    public void hideAudioMenu()
    {
        foreach (GameObject f in audioObjects)
        {
            f.SetActive(false);
        }
    }
    
    public void showPause()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }

        Time.timeScale = 0;
    }
    
    public void hidePause()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void showTooltip()
    {
        foreach (GameObject m in tooltipObjects)
        {
            m.SetActive(true);
        }
    }

    public void hideTooltips()
    {
        foreach (GameObject m in tooltipObjects)
        {
            m.SetActive(false);
        }
    }

    public void resetTime()
    {
        Time.timeScale = 1; 
    }
    
    public void completeUIDisable()
    {
        hidePause();
        hideAudioMenu();
        hideTooltips();
    }
}
