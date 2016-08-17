using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Button butto;

    void Start()
    {
        //butto.Select();
    }

	public void goToNextLevel()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void exitToWindows()
    {
        Application.Quit();
    }

    public void goToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

	public void reloadScene ()
	{
		Scene thisScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (thisScene.name);
	}

}
