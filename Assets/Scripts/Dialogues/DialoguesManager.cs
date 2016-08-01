using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IndianaBones
{

	public class DialoguesManager : MonoBehaviour {

		// Singleton Implementation
		protected static DialoguesManager _self;
		public static DialoguesManager Self
		{
			get
			{
				if (_self == null)
					_self = FindObjectOfType(typeof(DialoguesManager)) as DialoguesManager;
				return _self;
			}
		}

		public List<GameObject> dialoguesToSee = new List<GameObject>();
		public List<bool> dialoguesActivated = new List<bool>();

		public int dialogueAbilityRestore;
		public int dialogueAbilityRope;
		public int dialogueAbilityFever;

		// Use this for initialization
		void Start () {
			//CheckDialogues ();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		//Set to true the corrsipondent dialogue index
		public void SetDialogueSeen (int index) {
			for (int i = 0; i < dialoguesActivated.Count; i++) {
				if (i == index) {
					dialoguesActivated[i] = true;
				}
			}

			//CheckEvent ();
		}


		//I dont know
		public void CheckDialogues () {
			for (int i = 0; i < dialoguesToSee.Count; i++) {
				if (!dialoguesActivated [i]) {
					dialoguesToSee [i].SetActive (true);
				}
			}
			CheckEvent ();
		}


		public bool CheckSeen (int index) {
			for (int i = 0; i < 4; i++) {
				Debug.Log ("CHECKSEEN: " + index);

				if (i== index) {
					Debug.Log ("IL DIALOGO TROVATO E': " + dialoguesActivated [i]);
					return dialoguesActivated [i];
				} else {
					Debug.Log ("INDEX NON TROVATO");

				}
			}
			return false;
		}


		//Attiva le abilità se i relativi dialoghi sono stati visti
		public void CheckEvent () {
			for (int i = 0; i < dialoguesActivated.Count; i++) {
				if (i == dialogueAbilityRestore) {
					if (dialoguesActivated [i]) {
						Hotbar.Self.gameObject.transform.GetChild (5).GetComponent<SlotButtonHandler> ().slotOccupied = true;
						Hotbar.Self.gameObject.transform.GetChild (5).GetChild (0).GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
					}
				} 

				if (i == dialogueAbilityRope) {
					if (dialoguesActivated [i]) {
						Hotbar.Self.gameObject.transform.GetChild (6).GetComponent<SlotButtonHandler> ().slotOccupied = true;
						Hotbar.Self.gameObject.transform.GetChild (6).GetChild (0).GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
					}
				}

				if (i == dialogueAbilityFever) {
					if (dialoguesActivated [i]) {
						Hotbar.Self.gameObject.transform.GetChild (7).GetComponent<SlotButtonHandler> ().slotOccupied = true;
						Hotbar.Self.gameObject.transform.GetChild (7).GetChild (0).GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
					}
				}
			}

		}


	}

}
