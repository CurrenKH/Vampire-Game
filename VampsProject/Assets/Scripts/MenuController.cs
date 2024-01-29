using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject FirstMenu;
    [SerializeField] GameObject SecondMenu;

    private void Start()
    {
        FirstMenu.SetActive(true);
        SecondMenu.SetActive(false);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void PlayTutorialButton()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void OptionButton()
    {
        Debug.Log("Options");
        FirstMenu.SetActive(false);
        SecondMenu.SetActive(true);
    }

    public void ExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void BackButton()
    {
        FirstMenu.SetActive(true);
        SecondMenu.SetActive(false);
    }

    public void MuteButton(bool value)
    {
        Debug.Log(value);
        if (value)
        {
            PlayerPrefs.SetInt("mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 0);
        }
    }
}
