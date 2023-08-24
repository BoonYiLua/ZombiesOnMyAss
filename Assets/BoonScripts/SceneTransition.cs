using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {
  
    // Start is called before the first frame update




    
   

    public void Play() {
        SceneManager.LoadScene("Level");


    }

    public void Quit() {
        Application.Quit();
    }

    public void Instructions() {
        SceneManager.LoadScene("Instructions");

    }

    public void Credits() {
        SceneManager.LoadScene("Credits");

    }

    public void Back() {
        SceneManager.LoadScene("MainMenu");

    }

    // Update is called once per frame
    void Update() {

    }
}
