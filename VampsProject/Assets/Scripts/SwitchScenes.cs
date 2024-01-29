using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelUpOptions>().ResetAttacks();
            SceneManager.LoadScene(sceneName);
        }
    }
}
