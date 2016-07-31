using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        pause.GetComponent<CanvasGroup>().alpha = 0;
        pause.GetComponent<CanvasGroup>().interactable = false;
    }

    public void goToHome()
    {
        SceneManager.LoadScene("Menu");
    }

	public void Update ()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.GetComponent<CanvasGroup>().alpha = 1;
            pause.GetComponent<CanvasGroup>().interactable = true;
        }
    }

}
