using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class DropHandler : MonoBehaviour {

	public List<GameObject> itemsList = new List<GameObject>();

	[Header("Canubi")]
	[Space(10)]
	public int minDropRate;
	public int maxDropRate;


	int randomValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void DropItems (string enemyName){
		randomValue = Random.Range (0, 100);

		//Execute the correct code based on the enemy that call this method
		switch (enemyName){

		case "Canubi":
			
			if (randomValue > 0 && randomValue < 20) {
				//Drop Latte

			} else if ((randomValue > 20 && randomValue < 40)) {
				//Drop Pozione Mana

			} else if ((randomValue > 40 && randomValue < 80)) {
				//Drop Denti

			} else if ((randomValue > 80 && randomValue < 100)) {
				//Non droppa niente

			}
				
			break;


		case "Scaramucca":
			
			if (randomValue > 0 && randomValue < 20) {
				//Drop Latte

			} else if ((randomValue > 20 && randomValue < 40)) {
				//Drop Pozione Mana

			} else if ((randomValue > 40 && randomValue < 80)) {
				//Drop Denti

			} else if ((randomValue > 80 && randomValue < 100)) {
				//Non droppa niente

			}

			break;


		case "Cammello":
			
			if (randomValue > 0 && randomValue < 20) {
				//Drop Latte

			} else if ((randomValue > 20 && randomValue < 40)) {
				//Drop Pozione Mana

			} else if ((randomValue > 40 && randomValue < 80)) {
				//Drop Denti

			} else if ((randomValue > 80 && randomValue < 100)) {
				//Non droppa niente

			}

			break;


		case "Gatto":
			
			if (randomValue > 0 && randomValue < 20) {
				//Drop Latte

			} else if ((randomValue > 20 && randomValue < 40)) {
				//Drop Pozione Mana

			} else if ((randomValue > 40 && randomValue < 80)) {
				//Drop Denti

			} else if ((randomValue > 80 && randomValue < 100)) {
				//Non droppa niente

			}

			break;


		case "Ra":

			if (randomValue > 0 && randomValue < 20) {
				//Drop Latte

			} else if ((randomValue > 20 && randomValue < 40)) {
				//Drop Pozione Mana

			} else if ((randomValue > 40 && randomValue < 80)) {
				//Drop Denti

			} else if ((randomValue > 80 && randomValue < 100)) {
				//Non droppa niente

			}
			break;


		case "Vaso":

			if (randomValue > 0 && randomValue < 20) {
				//Drop Latte

			} else if ((randomValue > 20 && randomValue < 40)) {
				//Drop Pozione Mana

			} else if ((randomValue > 40 && randomValue < 80)) {
				//Drop Denti

			} else if ((randomValue > 80 && randomValue < 100)) {
				//Non droppa niente

			}
			break;


		default:
			//No enemy recognized
			break;

		}


	}
}
