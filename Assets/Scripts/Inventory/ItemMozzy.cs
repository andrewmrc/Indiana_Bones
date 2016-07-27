using UnityEngine;
using System.Collections;

namespace IndianaBones
{

	public class ItemMozzy : MonoBehaviour {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}


		public void OnCollisionEnter2D(Collision2D coll)
		{
			if (coll.gameObject.tag == "Player")
			{
				Debug.Log ("Oggetto raccolto: " + gameObject.name);
				//Raccoglie questo oggetto e lo passa ad un metodo della hotbar
				//Se c'è già un oggetto di questo tipo lo somma altrimenti lo aggiunge al primo slot libero
				//Se la hotbar è piena l'oggetto resta a terra
				if (Hotbar.Self.freeSlotsCount < 5) {
					Hotbar.Self.CheckItem ("ItemMozzy", this.gameObject.GetComponent<SpriteRenderer> ().sprite);
					Destroy(this.gameObject);
				}


			}

		}

	}
}