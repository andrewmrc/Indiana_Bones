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

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void OnCollisionEnter2D(Collision2D coll)
		{
			if (coll.gameObject.name == "Player") {
				DialoguesManager.Self.SetDialogueSeen (dialogueIndex);
				dialogueToEnable.SetActive (true);
				Destroy (this.gameObject);
			}

		}
	}

}
