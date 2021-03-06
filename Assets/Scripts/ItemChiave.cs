﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace IndianaBones
{
   

    public class ItemChiave : MonoBehaviour
    {

        public Transform targetTr;
        public float speed = 1;
        public GameObject stemma;
        // Use this for initialization
        void Start()
        {
            stemma = GameObject.FindGameObjectWithTag("CanvasUI");
        }

        // Update is called once per frame
        void Update()
        {
			if (stemma == null) {
				stemma = GameObject.FindGameObjectWithTag("CanvasUI");
			}

			//targetTr = Player.Self.transform;
			Vector3 distance = Player.Self.transform.position - this.transform.position;

            if (distance.magnitude < 0.10f)
            {

				if (this.gameObject.tag == "Item_Key_1") {
					stemma.transform.GetChild (7).GetChild (1).GetComponent<Image> ().enabled = true;
				} else if (this.gameObject.tag == "Item_Key_2") {
					stemma.transform.GetChild (7).GetChild (0).GetComponent<Image> ().enabled = true;
				} else if (this.gameObject.tag == "Item_Key_3") {
					stemma.transform.GetChild (7).GetChild (2).GetComponent<Image> ().enabled = true;
				}


				transform.position = Player.Self.transform.position;
				
				Player.Self.keyScarabeo += 1;

                Destroy(this.gameObject);

            }

        }
    }
}
