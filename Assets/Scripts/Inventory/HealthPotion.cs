using UnityEngine;
using System.Collections;

public class HealthPotion : MonoBehaviour {

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
		/*	if (Hotbar.Self.healthPotion) {
				Hotbar.Self.healthMilkPotionCount++;
			} else {
				Hotbar.Self.healthMilkPotionCount++;
				Hotbar.Self.CheckItem ("HealthPotion", this.gameObject.GetComponent<SpriteRenderer>().sprite);
			}*/
			Hotbar.Self.CheckItem ("HealthPotion", this.gameObject.GetComponent<SpriteRenderer>().sprite);

			Destroy(this.gameObject);

		}

	}

}
