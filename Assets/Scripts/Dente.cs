using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Dente : MonoBehaviour
    {  
		
        public float forza = 0.5f;
        bool seen = false;

        // Update is called once per frame
        void FixedUpdate()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            Player objPlayer = FindObjectOfType<Player>();

            switch (objPlayer.bulletDir)
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


			if (coll.gameObject.name == "muro" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Colonne")
            {
				Debug.Log ("Nome oggetto toccato: " + coll.gameObject.name);
				GameController.Self.PassTurn ();
				Player.Self.ResetPlayerVar ();

                Destroy(this.gameObject);

            }

        }

        
    }
}
