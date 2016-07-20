using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hotbar : MonoBehaviour
{

	[SerializeField]
	public KeyCode[] keyCodesForSlots = new KeyCode[8];
	[SerializeField]
	public int slotsInTotal;

	public int inventoryObjectCount;
	public int healthMilkPotionCount;
	public int healthMozzyPotionCount;
	public int manaPotionCount;
	public int bombsCount;
	public int poisonCount;

	//public int abilityCount;

	void Start()
	{
		getSlotsInTotal ();
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

	public void Test (){
		Debug.Log ("Funziona");
	}

	public int getSlotsInTotal()
	{
		return slotsInTotal = transform.childCount;
	}
}
