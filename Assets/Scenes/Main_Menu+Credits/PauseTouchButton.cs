using UnityEngine;
using System.Collections;

public class PauseTouchButton : MonoBehaviour {

	GameObject canvasPause;

	void OnLevelWasLoaded(){
		if (canvasPause == null) {
			Debug.Log ("Canvas Pause recuperato");
			canvasPause = GameObject.FindGameObjectWithTag ("CanvasPause");
		}
	}

	// Use this for initialization
	void Start () {
		if (canvasPause == null) {
			Debug.Log ("Canvas Pause recuperato");
			canvasPause = GameObject.FindGameObjectWithTag ("CanvasPause");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void EnablePauseTouch () {
		if (canvasPause == null) {
			Debug.Log ("Canvas Pause recuperato");
			canvasPause = GameObject.FindGameObjectWithTag ("CanvasPause");
		}
		canvasPause.gameObject.transform.GetChild(0).GetComponent<Pause> ().EnablePause ();
	}


}
