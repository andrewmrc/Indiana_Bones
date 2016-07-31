using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using IndianaBones;

public class Pause : MonoBehaviour
{
    
    public Canvas pause;

    void Awake()
    {
        pause.GetComponent<CanvasGroup>().alpha = 0;
        pause.GetComponent<CanvasGroup>().interactable = false;

    }

	
    public void Return()
    {
		ElementsReference.Self.canvasUI.SetActive (true);
		Player.Self.gameObject.SetActive (true);
		Time.timeScale = 1;
        pause.GetComponent<CanvasGroup>().alpha = 0;
        pause.GetComponent<CanvasGroup>().interactable = false;
    }


    public void GoToHome()
    {
        SceneManager.LoadScene("Menu");
    }

	public void Update ()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			Time.timeScale = 0;
			ElementsReference.Self.canvasUI.SetActive (false);
			Player.Self.gameObject.SetActive (false);
            pause.GetComponent<CanvasGroup>().alpha = 1;
            pause.GetComponent<CanvasGroup>().interactable = true;
        }
    }

}
