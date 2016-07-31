using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class SpecialDoors : MonoBehaviour
    {
        int x;
        int y;

        Grid elementi;

		void Awake () {
			if (this.gameObject.name.Contains ("Ankh")) {
				this.tag = "Door_Ankh";
			} else if (this.gameObject.name.Contains ("Eye")) {
				this.tag = "Door_Eye";
			} else if (this.gameObject.name.Contains ("Bird")) {
				this.tag = "Door_Bird";
			}
		}

        void Start()
        {
            elementi = FindObjectOfType<Grid>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            elementi.scacchiera[x, y].status = 15;
            elementi.scacchiera[x, y].name = "SpecialDoor";
        }


        void Update()
        {
            
        }

    }
}
