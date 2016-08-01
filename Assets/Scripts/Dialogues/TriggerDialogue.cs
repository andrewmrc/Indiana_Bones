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

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void OnTriggerStay2D(Collider2D coll) 
		{
			if (coll.gameObject.name == "Player" && !Player.Self.endMove && !started) {
				started = true;
				if(Player.Self.GetComponent<DialoguesManager>().CheckSeen(dialogueIndex) == false){
					Debug.Log ("Start Dialogue: " + DialoguesManager.Self.CheckSeen(dialogueIndex));
					DialoguesManager.Self.SetDialogueSeen (dialogueIndex);
					dialogueToEnable.SetActive (true);
					//Destroy (this.gameObject);
				}
				this.gameObject.SetActive (false);
			}

		}


		 
	}

}
