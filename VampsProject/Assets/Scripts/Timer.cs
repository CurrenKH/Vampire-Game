using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float currentTime;
    public float startingTime = 60f;
   

    [SerializeField] private string sceneName;
    [SerializeField] TextMeshProUGUI countdownText;

    

    void Start()
    {
        currentTime = startingTime;
    }
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0")+"s";

        if(currentTime <= 0)
        {
            currentTime = 0;

            SceneManager.LoadScene(sceneName);
        }
      
        
    }

    
    
}
