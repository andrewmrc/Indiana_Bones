using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Cella : MonoBehaviour
    {
        public int status = 0;
		//public int height;
		//public int width;

        // Use this for initialization
        void Start()
        {
			
			//Disattiva le celle inutilizzate
			if (this.gameObject.name.Contains ("Empty")) {
				//this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,0);
			}


        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
