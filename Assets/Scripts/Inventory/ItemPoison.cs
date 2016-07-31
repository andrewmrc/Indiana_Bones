using UnityEngine;
using System.Collections;

namespace IndianaBones
{
	public class ItemPoison : MonoBehaviour {

		public Transform targetTr;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
			targetTr = Player.Self.transform;
			Vector3 distance = targetTr.position - this.transform.position;

			if (distance.magnitude < 0.10f) {
				transform.position = targetTr.position;
				Debug.Log ("Oggetto raccolto: " + gameObject.name);
				//Raccoglie questo oggetto e lo passa ad un metodo della hotbar
				//Se c'è già un oggetto di questo tipo lo somma altrimenti lo aggiunge al primo slot libero
				//Se la hotbar è piena l'oggetto resta a terra a meno che non ne abbiamo già uno di questo tipo
				if (Hotbar.Self.freeSlotsCount < Hotbar.Self.slotsInTotal) {
					Hotbar.Self.CheckItem ("Poison", this.gameObject.GetComponent<SpriteRenderer> ().sprite);
					Destroy(this.gameObject);
				} else if (Hotbar.Self.poison) {
					Hotbar.Self.CheckItem ("Poison", this.gameObject.GetComponent<SpriteRenderer> ().sprite);
					Destroy(this.gameObject);
				}
					
			}
		}


	}
}