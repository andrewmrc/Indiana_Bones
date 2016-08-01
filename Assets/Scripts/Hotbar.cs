using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IndianaBones
{

	public class Hotbar : MonoBehaviour
	{

		// Singleton Implementation
		protected static Hotbar _self;
		public static Hotbar Self
		{
			get
			{
				if (_self == null)
					_self = FindObjectOfType(typeof(Hotbar)) as Hotbar;
				return _self;
			}
		}

		[SerializeField]
		public KeyCode[] keyCodesForSlots = new KeyCode[8];
		[SerializeField]
		public int slotsInTotal;

		public int inventoryObjectCount;

		public int freeSlotsCount;

		public List<GameObject> itemsOnButton = new List<GameObject>();

		public List<bool> itemsTypeBool = new List<bool> ();

		[Header("Items Quantity")]
		[Space(10)]
		public int healthMilkPotionCount;
		public int healthMozzyPotionCount;
		public int manaPotionCount;
		public int bombsCount;
		public int toiletPaperCount;
		public int poisonCount;
		public int escapeRopeCount;

		[Header("Items Collected")]
		[Space(10)]
		public bool healthMilk;
		public bool healthMozzy;
		public bool manaPotion;
		public bool bomb;
		public bool toiletPaper;
		public bool poison;
		public bool escapeRope;
		/*
		[Header("Items Index In Bar")]
		[Space(10)]
		//public int item index;
		public int indexSlotMilk;
		public int indexSlotMana;
		public int indexSlotMozz;
		public int indexSlotBomb;
		public int indexSlotToiletPaper;
		public int indexSlotPoison;
		public int indexSlotEscapeRope;
*/

		void Start()
		{
			//GetTotalSlots ();
			slotsInTotal = 5;
		}

		void Update()
		{
			for (int i = 0; i < slotsInTotal; i++)
			{
				if(Input.GetKeyDown(keyCodesForSlots[i])){
					if (Player.Self.GetComponent<TurnHandler>().itsMyTurn)
					{
						if (transform.GetChild (i).childCount != 0) {
								//transform.GetChild (i).GetComponent<Image> ().color = new Color32 (0, 160, 255, 255);
								//transform.GetChild (i).GetComponent<Button> ().onClick.Invoke();
							if (i <= 4) {
								Debug.Log ("Item i: " + i);
								ActivateItem (i);
							} 
						}
					}
				}
			}


			for (int i = 5; i < 8; i++)
			{
				if(Input.GetKeyDown(keyCodesForSlots[i])){
					if (Player.Self.GetComponent<TurnHandler>().itsMyTurn)
					{
						if (transform.GetChild (i).childCount != 0) {
							//transform.GetChild (i).GetComponent<Image> ().color = new Color32 (0, 160, 255, 255);
							//transform.GetChild (i).GetComponent<Button> ().onClick.Invoke();
							if (i >= 5) {
								Debug.Log ("Ability i: " + i);
								ActivateAbility (i);
							}
						}
					}
				}
			}


		}


		public int GetTotalSlots()
		{
			return slotsInTotal = transform.childCount;
		}


		public void CheckItem(string itemName, Sprite itemSprite){

			switch (itemName) {

			case "ItemMilk":
				if (healthMilkPotionCount > 0) {
					healthMilkPotionCount++;
					UpdateItemQuantity (itemName, healthMilkPotionCount);
				} else {
					healthMilk = true;
					healthMilkPotionCount++;
					AddItem (itemSprite, itemName, healthMilkPotionCount);
					//indexSlotMilk = freeSlotsCount;
				}
				break;

			case "ItemMozzy":
				if (healthMozzyPotionCount > 0) {
					healthMozzyPotionCount++;
					UpdateItemQuantity (itemName, healthMozzyPotionCount);
				} else {
					healthMozzy = true;
					healthMozzyPotionCount++;
					AddItem (itemSprite, itemName, healthMozzyPotionCount);
					//indexSlotMilk = freeSlotsCount;
				}
				break;

			case "ManaPotion":
				if (manaPotionCount > 0) {
					manaPotionCount++;
					UpdateItemQuantity (itemName, manaPotionCount);
				} else {
					manaPotion = true;
					manaPotionCount++;
					//indexSlotMana = freeSlotsCount;
					AddItem (itemSprite, itemName, manaPotionCount);
				}
				break;


			case "Bomb":
				if (bombsCount > 0) {
					bombsCount++;
					UpdateItemQuantity (itemName, bombsCount);
				} else {
					bomb = true;
					bombsCount++;
					//indexSlotMana = freeSlotsCount;
					AddItem (itemSprite, itemName, bombsCount);
				}
				break;


			case "Poison":
				if (poisonCount > 0) {
					poisonCount++;
					UpdateItemQuantity (itemName, poisonCount);
				} else {
					poison = true;
					poisonCount++;
					//indexSlotMana = freeSlotsCount;
					AddItem (itemSprite, itemName, poisonCount);
				}
				break;


			}


		}


		public void AddItem (Sprite itemSprite, string itemName, int itemCount) {
			if(freeSlotsCount <= slotsInTotal){
				for (int i = 0; i < itemsOnButton.Count; i++) {
					if (!itemsOnButton [i].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied) {
						itemsOnButton [i].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied = true;
						itemsOnButton [i].gameObject.GetComponent<SlotButtonHandler> ().itemOnThisButton = itemName;
						itemsOnButton [i].gameObject.GetComponent<SlotButtonHandler> ().itemIndex = i;

						transform.GetChild (i).GetChild (1).GetComponent<Text> ().text = ("x"+itemCount.ToString ());

						transform.GetChild (i).GetChild (0).GetComponent<Image> ().sprite = itemSprite;
						transform.GetChild (i).GetChild (0).GetComponent<Image> ().color = Color.white;
						transform.GetChild (i).GetChild (0).GetComponent<Image> ().SetNativeSize ();
						freeSlotsCount++;
						break;
					}
				}
			}



		}


		public void UpdateItemQuantity (string itemName, int itemCount) {
			for (int i = 0; i < itemsOnButton.Count; i++) {
				if (itemsOnButton [i].gameObject.GetComponent<SlotButtonHandler> ().itemOnThisButton == itemName) {
					transform.GetChild (i).GetChild (1).GetComponent<Text> ().text = ("x"+itemCount.ToString ());
				}
			}

		}

        IEnumerator White()
        {
            Player.Self.gameObject.transform.GetChild(8).GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(1);
            Player.Self.gameObject.transform.GetChild(8).GetComponent<SpriteRenderer>().enabled = false;
        }

        IEnumerator Mana()
        {
            Player.Self.gameObject.transform.GetChild(7).GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(1);
            Player.Self.gameObject.transform.GetChild(7).GetComponent<SpriteRenderer>().enabled = false;
        }

        public void ActivateItem (int keyCode){
			Debug.Log ("Try activate object");

			if (itemsOnButton [keyCode].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied) {
				string itemName = itemsOnButton [keyCode].gameObject.GetComponent<SlotButtonHandler> ().itemOnThisButton;
				int itemIndex = itemsOnButton [keyCode].gameObject.GetComponent<SlotButtonHandler> ().itemIndex;

				switch (itemName) {

				case "ItemMilk":
					Debug.Log ("Use MILK");
					healthMilkPotionCount--;
					Player.Self.currentLife += 5;
                        StartCoroutine(White());
                        if (Player.Self.currentLife > Player.Self.startingLife) {
						Player.Self.currentLife = Player.Self.startingLife;
					}
					GameController.Self.PassTurn ();
					transform.GetChild (itemIndex).GetChild (1).GetComponent<Text> ().text = ("x"+healthMilkPotionCount.ToString ());
					if (healthMilkPotionCount <= 0) {
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().sprite = null;
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().color = new Color32(255,255,255,0);
						transform.GetChild (itemIndex).GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);
						healthMilk = false;
						itemsOnButton [itemIndex].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied = false;
						freeSlotsCount--;
					} 
					break;


				case "ItemMozzy":
					Debug.Log ("Use MOZZARELLA");
					healthMozzyPotionCount--;
					Player.Self.currentLife += 10;
                        StartCoroutine(White());
                        if (Player.Self.currentLife > Player.Self.startingLife) {
						Player.Self.currentLife = Player.Self.startingLife;
					}
					GameController.Self.PassTurn ();
					transform.GetChild (itemIndex).GetChild (1).GetComponent<Text> ().text = ("x"+healthMozzyPotionCount.ToString ());
					if (healthMozzyPotionCount <= 0) {
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().sprite = null;
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().color = new Color32(255,255,255,0);
						transform.GetChild (itemIndex).GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);
						healthMozzy = false;
						itemsOnButton [itemIndex].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied = false;
						freeSlotsCount--;
					} 
					break;
				

				case "ManaPotion":
					Debug.Log ("Use MANA POTION");
					manaPotionCount--;
					Player.Self.currentMana += 5;
                        StartCoroutine(Mana());
                        if (Player.Self.currentMana > Player.Self.startingMana) {
						Player.Self.currentMana = Player.Self.startingMana;
					}
					GameController.Self.PassTurn ();
					transform.GetChild (itemIndex).GetChild (1).GetComponent<Text> ().text = ("x"+manaPotionCount.ToString ());
					if (manaPotionCount <= 0) {
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().sprite = null;
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().color = new Color32(255,255,255,0);
						transform.GetChild (itemIndex).GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);
						manaPotion = false;
						itemsOnButton [itemIndex].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied = false;
						freeSlotsCount--;
					} 
					break;


				case "Bomb":
					Debug.Log ("Use Bomb");
					bombsCount--;

					//Inserire effetto script della bomba
					Player.Self.MolotovAttack();

					transform.GetChild (itemIndex).GetChild (1).GetComponent<Text> ().text = ("x"+bombsCount.ToString ());
					if (bombsCount <= 0) {
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().sprite = null;
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().color = new Color32(255,255,255,0);
						transform.GetChild (itemIndex).GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);
						bomb = false;
						itemsOnButton [itemIndex].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied = false;
						freeSlotsCount--;
					} 
					break;


				case "Poison":
					Debug.Log ("Use Poison");
					poisonCount--;

					//Inserire effetto script veleno
					Player.Self.PoisonAttack();

					transform.GetChild (itemIndex).GetChild (1).GetComponent<Text> ().text = ("x"+poisonCount.ToString ());
					if (poisonCount <= 0) {
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().sprite = null;
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().color = new Color32(255,255,255,0);
						transform.GetChild (itemIndex).GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);
						poison = false;
						itemsOnButton [itemIndex].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied = false;
						freeSlotsCount--;
					} 
					break;
				}


			} else {
				Debug.Log ("FREE SLOT");
			}
				
		}


		public void ActivateAbility (int keyCode){
			Debug.Log ("Try activate ability");

			if (itemsOnButton [keyCode].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied) {
				string itemName = itemsOnButton [keyCode].gameObject.GetComponent<SlotButtonHandler> ().itemOnThisButton;
				//int itemIndex = itemsOnButton [keyCode].gameObject.GetComponent<SlotButtonHandler> ().itemIndex;
				Debug.Log ("Searching ability");

				switch (itemName) {

				case "Restore":
					AbilityHandler.Self.Restore ();

					break;


				case "Fever":
					AbilityHandler.Self.Fever ();

					break;


				case "EscapeRope":
					AbilityHandler.Self.EscapeRope ();

					break;


				}

			}

		}





	}
}