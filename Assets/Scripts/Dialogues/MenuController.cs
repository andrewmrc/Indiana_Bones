using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
	
	float indexSelected;

	bool  received_input;
	public GameObject[] menu;
	GameObject selection;
	GameObject selectionOld;
	public int selectionNum = 0;

	public bool horizontal;
	public bool vertical;
	bool buttonPressed;

	void  Start (){
		
		received_input = false;
		
		selectionNum = 0;
		selection = menu[0];
		selection.GetComponent<Button> ().Select ();

	}
	
	
	void  Update (){
		//Debug.Log ("moveVertical " + moveVertical);
		
		if(indexSelected == 0){
			received_input = false;
		}

		if (horizontal) {

			if (!received_input) {
				if (Input.GetKeyDown (KeyCode.LeftArrow) || indexSelected == -1) {
					received_input = true;
					if (selectionNum >= 0) {
						selectionNum++;
						
						if (selectionNum <= 1) {
							selection = menu [selectionNum];
							selection.GetComponent<Button> ().Select ();
							//selectionNum = selectionNum -1;
							selectionOld = menu [selectionNum - 1];

						} else {
							selectionOld = menu [1];
							selectionNum = 0;
							selection = menu [0]; 
							selection.GetComponent<Button> ().Select ();

						}
						
					}
				}
				
			}
			
			if (!received_input) {
				
				if (Input.GetKeyDown (KeyCode.RightArrow) || indexSelected == 1) {
					received_input = true;
					
					if (selectionNum >= 0) {
						selectionNum--; 
						if (selectionNum >= 0) {
							selection = menu [selectionNum];
							selection.GetComponent<Button> ().Select ();
							//selectionNum = selectionNum -1;
							selectionOld = menu [selectionNum + 1];

						} else {
							selectionOld = menu [0];
							selectionNum = 1;
							selection = menu [1];
							selection.GetComponent<Button> ().Select ();

						}
					}
				}
			}

		} else if (vertical) {

			if (!received_input) {
				if (Input.GetKeyDown (KeyCode.DownArrow) || indexSelected == -1) {
					received_input = true;
					if (selectionNum >= 0) {
						selectionNum++;

						if (selectionNum <= 1) {
							selection = menu [selectionNum];
							selection.GetComponent<Button> ().Select ();
							//selectionNum = selectionNum -1;
							selectionOld = menu [selectionNum - 1];

						} else {
							selectionOld = menu [1];
							selectionNum = 0;
							selection = menu [0]; 
							selection.GetComponent<Button> ().Select ();

						}

					}
				}

			}

			if (!received_input) {

				if (Input.GetKeyDown (KeyCode.UpArrow) || indexSelected == 1) {
					received_input = true;

					if (selectionNum >= 0) {
						selectionNum--; 
						if (selectionNum >= 0) {
							selection = menu [selectionNum];
							selection.GetComponent<Button> ().Select ();
							//selectionNum = selectionNum -1;
							selectionOld = menu [selectionNum + 1];

						} else {
							selectionOld = menu [0];
							selectionNum = 1;
							selection = menu [1];
							selection.GetComponent<Button> ().Select ();

						}
					}
				}
			}

		}

		if (!buttonPressed) {
			//do things based on actual selection 
			if (selection == menu [0] && Input.GetKeyDown (KeyCode.Return)) {
				buttonPressed = true;
				Debug.Log ("Button 1 selected");
				selection.GetComponent<Button> ().onClick.Invoke ();

			} else if (selection == menu [1] && Input.GetKeyDown (KeyCode.Return)) {
				buttonPressed = true;
				Debug.Log ("Button 2 selected");
				selection.GetComponent<Button> ().onClick.Invoke ();

			}
		}
	}
	
}
