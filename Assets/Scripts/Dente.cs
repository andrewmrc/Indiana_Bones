using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Dente : MonoBehaviour
    {  
		Rigidbody2D rb;
        public float forza = 0.5f;
        bool seen = false;

		void Start (){
			rb = GetComponent<Rigidbody2D>();

		}

        // Update is called once per frame
        void FixedUpdate()
        {

			switch (Player.Self.bulletDir)
            {
                case 1:
                    rb.AddForce(transform.up * forza, ForceMode2D.Impulse); 
                    break;
                case 2:
                    rb.AddForce(transform.up * -forza, ForceMode2D.Impulse);
                    break;
                case 3:
                    rb.AddForce(transform.right * forza, ForceMode2D.Impulse);
                    break;
                case 4:
                    rb.AddForce(transform.right * -forza, ForceMode2D.Impulse);
                    break;
            }


        }


        void Update()
        {
			if (!GetComponent<Renderer> ().isVisible) {
				Debug.Log ("Dente uscito dallo schermo -> Player passa il turno");
				GameController.Self.PassTurn ();
				Player.Self.ResetPlayerVar ();

				Destroy(this.gameObject);
			}
                
        }


        public void OnCollisionEnter2D(Collision2D coll)
        {


			if (coll.gameObject.tag == "Walls" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Colonne" || coll.gameObject.tag == "Door_Ankh" || coll.gameObject.tag == "Door_Eye" || coll.gameObject.tag == "Door_Bird")
            {
				Debug.Log ("Nome oggetto toccato: " + coll.gameObject.name);
				GameController.Self.PassTurn ();
				Player.Self.ResetPlayerVar ();

                Destroy(this.gameObject);

            }

        }

        
    }
}
