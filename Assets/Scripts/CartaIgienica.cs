using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class CartaIgienica : MonoBehaviour
    {

        public float forza = 0.5f;
        bool seen = false;


        // Update is called once per frame
        void FixedUpdate()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            Mummia objMummia = FindObjectOfType<Mummia>();

            switch (objMummia.direzioneLancio)
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
				Debug.Log ("Carta igienica uscita dallo schermo -> Mummia passa il turno");
				GameController.Self.PassTurn ();
				Destroy(this.gameObject);
			}
        }


        public void OnCollisionEnter2D(Collision2D coll)
        {
            Mummia elementi = FindObjectOfType<Mummia>();

			if (coll.gameObject.tag == "Walls" || coll.gameObject.tag == "Colonne")
            {
				Debug.Log ("Nome oggetto toccato: " + coll.gameObject.name);

				GameController.Self.PassTurn ();

                Destroy(this.gameObject);

			} else if (coll.gameObject.tag == "Player") {

                Player.Self.currentLife -= elementi.attackPower;
				GameController.Self.PassTurn ();

				Destroy(this.gameObject);
            }

        }


    }
}
