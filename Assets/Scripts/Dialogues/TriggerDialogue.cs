using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IndianaBones
{

	public class TriggerDialogue : MonoBehaviour {

		public GameObject dialogueToEnable;
		public int dialogueIndex;
		bool started;
		public GameObject specialObjects;

		// Use this for initialization
		void Start () {
			if (DialoguesManager.Self.dialoguesActivated [dialogueIndex]) {
				if (specialObjects != null) {
					specialObjects.SetActive (false);
				}
			}
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void OnTriggerStay2D(Collider2D coll) 
		{
			if (coll.gameObject.name == "Player" && !Player.Self.endMove && !started) {
				started = true;
				if(DialoguesManager.Self.CheckSeen(dialogueIndex) == false){
					Debug.Log ("Start Dialogue: " + DialoguesManager.Self.CheckSeen(dialogueIndex));
					DialoguesManager.Self.SetDialogueSeen (dialogueIndex);
					dialogueToEnable.SetActive (true);
					//Destroy (this.gameObject);
					if (specialObjects != null) {
						specialObjects.SetActive (false);
					}
				}
				this.gameObject.SetActive (false);
			}

		}


		 
	}

}
