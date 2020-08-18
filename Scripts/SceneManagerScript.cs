using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void StartTheGame(){
        SceneManager.LoadScene("level1");
    }
    public void MainMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void closeGame(){
        Application.Quit();
    }

}
