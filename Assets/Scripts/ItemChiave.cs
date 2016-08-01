using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace IndianaBones
{
   

    public class ItemChiave : MonoBehaviour
    {

        public Transform targetTr;
        public float speed = 1;
        GameObject stemma;
        // Use this for initialization
        void Start()
        {
            stemma = GameObject.FindGameObjectWithTag("CanvasUI");
        }

        // Update is called once per frame
        void Update()
        {

			targetTr = Player.Self.transform;
            Vector3 distance = targetTr.position - this.transform.position;

            if (distance.magnitude < 0.10f)
            {

                if (name == "Item_Key_1")
                    stemma.transform.GetChild(7).GetChild(1).GetComponent<Image>().enabled = true;
                else if (name == "Item_Key_2")
                    stemma.transform.GetChild(7).GetChild(0).GetComponent<Image>().enabled = true;
                else if (name == "Item_Key_3")
                    stemma.transform.GetChild(7).GetChild(2).GetComponent<Image>().enabled = true;


                transform.position = targetTr.position;
				
				Player.Self.keyScarabeo += 1;

                Destroy(this.gameObject);

            }

        }
    }
}
