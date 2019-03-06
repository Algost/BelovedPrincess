using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public string nextScene;

	// Use this for initialization
	void Awake () {
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void loadGame()
    {
        SceneManager.LoadScene("Tuto");
    }

    public void loadCredit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Princess"))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
