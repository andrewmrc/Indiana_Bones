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
			GetTotalSlots ();
		}

		void Update()
		{
			for (int i = 0; i < slotsInTotal; i++)
			{
				if (Input.GetKeyDown(keyCodesForSlots[i]))
				{
					if (transform.GetChild (i).childCount != 0) {
							//transform.GetChild (i).GetComponent<Image> ().color = new Color32 (0, 160, 255, 255);
							//transform.GetChild (i).GetComponent<Button> ().onClick.Invoke();
							Debug.Log ("i: " + i);
							ActivateItem (i);
					}
				}
			}

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
					transform.GetChild (itemIndex).GetChild (1).GetComponent<Text> ().text = ("x"+healthMilkPotionCount.ToString ());
					if (healthMilkPotionCount <= 0) {
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().sprite = null;
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().color = new Color32(255,255,255,0);
						transform.GetChild (itemIndex).GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);
						healthMilk = false;
						itemsOnButton [itemIndex].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied = false;

					} 
					break;


				case "ItemMozzy":
					Debug.Log ("Use MOZZARELLA");
					healthMozzyPotionCount--;
					Player.Self.currentLife += 10;
					transform.GetChild (itemIndex).GetChild (1).GetComponent<Text> ().text = ("x"+healthMozzyPotionCount.ToString ());
					if (healthMozzyPotionCount <= 0) {
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().sprite = null;
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().color = new Color32(255,255,255,0);
						transform.GetChild (itemIndex).GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);
						healthMozzy = false;
						itemsOnButton [itemIndex].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied = false;

					} 
					break;
				

				case "ManaPotion":
					Debug.Log ("Use MANA POTION");
					manaPotionCount--;
					Player.Self.currentMana += 5;
					transform.GetChild (itemIndex).GetChild (1).GetComponent<Text> ().text = ("x"+manaPotionCount.ToString ());
					if (manaPotionCount <= 0) {
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().sprite = null;
						transform.GetChild (itemIndex).GetChild (0).GetComponent<Image> ().color = new Color32(255,255,255,0);
						transform.GetChild (itemIndex).GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (50, 50);
						manaPotion = false;
						itemsOnButton [itemIndex].gameObject.GetComponent<SlotButtonHandler> ().slotOccupied = false;

					} 
					break;

				}


			} else {
				Debug.Log ("FREE SLOT");
			}
				
		}



		public int GetTotalSlots()
		{
			return slotsInTotal = transform.childCount;
		}


	}
}