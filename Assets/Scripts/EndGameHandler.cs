using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace IndianaBones
{

	public class EndGameHandler : MonoBehaviour {

		bool started;
		public GameObject canvasToOpen;
		//public bool destroyLater;

		// Use this for initialization
		void Start () {
			
		}

		// Update is called once per frame
		void Update () {

		}

		public void OnTriggerStay2D(Collider2D coll) 
		{
			if (coll.gameObject.name == "Player" && !Player.Self.endMove && !started) {
				Debug.Log ("Ontrigger");

				started = true;
				canvasToOpen.SetActive (true);
				ElementsReference.Self.canvasUI.SetActive (false);
				Player.Self.gameObject.SetActive (false);
				StartCoroutine(StartCreditsScene());
			}

		}


		IEnumerator StartCreditsScene(){
			Debug.Log ("Credits");
			yield return new WaitForSeconds (2f);
			SceneManager.LoadScene ("Credits");
		}
	}

}

