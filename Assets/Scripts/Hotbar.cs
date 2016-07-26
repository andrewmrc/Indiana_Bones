using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

	int freeSlotIndex;

	public List<string> itemsType = new List<string>();

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
	public bool healthPotion;
	public bool healthMozz;
	public bool manaPotion;
	public bool bomb;
	public bool toiletPaper;
	public bool poison;
	public bool escapeRope;

	//public int abilityCount;
	int indexSlotHealth;
	int indexSlotMana;
	int indexSlotMozz;
	int indexSlotBomb;
	int indexSlotToiletPaper;
	int indexSlotPoison;
	int indexSlotEscapeRope;


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
					transform.GetChild (i).GetComponent<Image> ().color = new Color32 (0, 160, 255, 255);
					transform.GetChild (i).GetComponent<Button> ().onClick.Invoke();

				}
			}
		}

	}


	public void CheckItem(string itemName, Sprite itemSprite){

		switch (itemName) {

		case "HealthPotion":
			if (healthMilkPotionCount > 0) {
				healthMilkPotionCount++;
				transform.GetChild (indexSlotHealth).GetChild (1).GetComponent<Text> ().text = healthMilkPotionCount.ToString ();
			} else {
				healthPotion = true;
				healthMilkPotionCount++;
				indexSlotHealth = freeSlotIndex;
				transform.GetChild (indexSlotHealth).GetChild (1).GetComponent<Text> ().text = healthMilkPotionCount.ToString ();
				AddItem (itemSprite);
			}
			break;

		case "ManaPotion":
			if (manaPotionCount > 0) {
				manaPotionCount++;
				transform.GetChild (indexSlotMana).GetChild (1).GetComponent<Text> ().text = manaPotionCount.ToString ();
			} else {
				manaPotion = true;
				manaPotionCount++;
				indexSlotMana = freeSlotIndex;
				transform.GetChild (indexSlotMana).GetChild (1).GetComponent<Text> ().text = manaPotionCount.ToString ();
				AddItem (itemSprite);
			}
			break;
		}
	}


	public void AddItem (Sprite itemSprite) {
		if(freeSlotIndex <= slotsInTotal){
			transform.GetChild (freeSlotIndex).GetChild(0).GetComponent<Image> ().sprite = itemSprite;
			transform.GetChild (freeSlotIndex).GetChild (0).GetComponent<Image> ().SetNativeSize ();
			freeSlotIndex++;
		}
	}


	public void Test (){
		Debug.Log ("Funziona");
		for (int i = freeSlotIndex; i < slotsInTotal; i++)
		{
			if (transform.GetChild (i).childCount != 0) {
				transform.GetChild (i).GetComponent<Image> ().color = new Color32 (0, 160, 255, 255);
				transform.GetChild (i).GetComponent<Button> ().onClick.Invoke();

			}
		}


	}

	public int GetTotalSlots()
	{
		return slotsInTotal = transform.childCount;
	}
}
