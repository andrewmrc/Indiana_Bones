using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace IndianaBones
{

	public class DropHandler : MonoBehaviour {

		// Singleton Implementation
		protected static DropHandler _self;
		public static DropHandler Self

		{
			get
			{
				if (_self == null)
					_self = FindObjectOfType(typeof(DropHandler)) as DropHandler;
				return _self;
			}
		}


		//public List<GameObject> itemsList = new List<GameObject>();
		/*
		[Header("Canubi")]
		[Space(10)]
		public int minDropRate;
		public int maxDropRate;
		*/

		[Header("Items")]
		[Space(10)]
		public GameObject milk;
		public GameObject mozzarella;
		public GameObject manaPotion;
		public GameObject dente;

		int randomValue;


		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}


		public void DropItems (string enemyName, float xPosition, float yPosition){
			randomValue = Random.Range (0, 100);

			//Execute the correct code based on the enemy that call this method
			switch (enemyName){

			case "Canubi":
				
				if (randomValue > 0 && randomValue < 20) {
					//Drop Latte
					Instantiate(milk).gameObject.transform.position = new Vector2(xPosition,yPosition);

				} else if ((randomValue > 20 && randomValue < 40)) {
					//Drop Pozione Mana
					Instantiate(manaPotion).gameObject.transform.position = new Vector2(xPosition,yPosition);

				} else if ((randomValue > 40 && randomValue < 80)) {
					//Drop Denti
					Debug.Log("Denti Droppati");
					Instantiate(dente).gameObject.transform.position = new Vector2(xPosition,yPosition);

				} else if ((randomValue > 80 && randomValue < 100)) {
					//Non droppa niente
					Debug.Log("NIENTE DROP");

				}
					
				break;


			case "Scaramucca":
				
				if (randomValue > 0 && randomValue < 40) {
					//Drop Latte
					Instantiate (milk).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 40 && randomValue < 60)) {
					//Drop Mozzarella
					Instantiate (mozzarella).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 60 && randomValue < 70)) {
					//Drop Pozione Mana
					Instantiate (manaPotion).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 70 && randomValue < 90)) {
					//Drop Denti
					Debug.Log ("Denti Droppati");
					Instantiate (dente).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 90 && randomValue < 100)) {
					//Non droppa niente
					Debug.Log ("NIENTE DROP");

				}

				break;


			case "Cammello":
				
				if (randomValue > 0 && randomValue < 20) {
					//Drop Latte
					Instantiate(milk).gameObject.transform.position = new Vector2(xPosition,yPosition);

				} else if ((randomValue > 20 && randomValue < 30)) {
					//Drop Mozzarella
					Instantiate (mozzarella).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 30 && randomValue < 70)) {
					//Drop Pozione Mana
					Instantiate (manaPotion).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 70 && randomValue < 90)) {
					//Drop Denti
					Debug.Log ("Denti Droppati");
					Instantiate (dente).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 90 && randomValue < 100)) {
					//Non droppa niente
					Debug.Log ("NIENTE DROP");

				}

				break;


			case "Gatto":
				
				if (randomValue > 0 && randomValue < 20) {
					//Drop Latte
					Instantiate (milk).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 20 && randomValue < 50)) {
					//Drop Mozzarella
					Instantiate (mozzarella).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 50 && randomValue < 70)) {
					//Drop Pozione Mana
					Instantiate (manaPotion).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 70 && randomValue < 90)) {
					//Drop Denti
					Debug.Log ("Denti Droppati");
					Instantiate (dente).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 90 && randomValue < 100)) {
					//Non droppa niente
					Debug.Log ("NIENTE DROP");

				}

				break;


			case "Ra":

				if (randomValue > 0 && randomValue < 20) {
					//Drop Latte
					Instantiate(milk).gameObject.transform.position = new Vector2(xPosition,yPosition);

				} else if ((randomValue > 20 && randomValue < 40)) {
					//Drop Mozzarella
					Instantiate (mozzarella).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 40 && randomValue < 60)) {
					//Drop Pozione Mana
					Instantiate (manaPotion).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 60 && randomValue < 90)) {
					//Drop Denti
					Debug.Log ("Denti Droppati");
					Instantiate (dente).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 90 && randomValue < 100)) {
					//Non droppa niente
					Debug.Log ("NIENTE DROP");

				}

				break;


			case "Vaso":

				if (randomValue > 0 && randomValue < 25) {
					//Drop Latte
					Instantiate(milk).gameObject.transform.position = new Vector2(xPosition,yPosition);

				} else if ((randomValue > 25 && randomValue < 50)) {
					//Drop Pozione Mana
					Instantiate (manaPotion).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 50 && randomValue < 70)) {
					//Drop Denti
					Instantiate (dente).gameObject.transform.position = new Vector2 (xPosition, yPosition);

				} else if ((randomValue > 70 && randomValue < 100)) {
					//Non droppa niente

				}

				break;


			default:
				//No enemy recognized
				break;

			}


		}
	}
}