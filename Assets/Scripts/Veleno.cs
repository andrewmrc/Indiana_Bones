using UnityEngine;
using System.Collections;

namespace IndianaBones
{

	public class Veleno : MonoBehaviour {

        Grid elementi;
        Rigidbody2D rb;
        BoxCollider2D bc;

        float forza = 0.1f;

		public int playerTurnsCount = 0;
		public int nTurnStay = 3;

		public bool counted;
		public bool isExploded;
		bool started;

        void Start () {

            elementi = FindObjectOfType<Grid>();
            rb = GetComponent<Rigidbody2D>();
            bc = GetComponent<BoxCollider2D>();

        }


		void Update()
		{
			//Controlla se il veleno esce dallo schermo
			if (!GetComponent<Renderer> ().isVisible && isExploded == false) {
				Debug.Log ("Veleno uscito dallo schermo -> Player passa il turno");
				GameController.Self.PassTurn ();
				Player.Self.ResetPlayerVar ();

				Destroy(this.gameObject);
			}


			if (Player.Self.GetComponent<TurnHandler> ().itsMyTurn == true && counted == false) {
				counted = true;
				playerTurnsCount++;
			} 

			if (Player.Self.GetComponent<TurnHandler> ().itsMyTurn == false) {
				counted = false;
			}

			if (playerTurnsCount >= nTurnStay) {
				Debug.Log ("Player turn count: " + playerTurnsCount + " Turn stay: " + nTurnStay);
				Destroy(this.gameObject);
			} 

		}


        void FixedUpdate()
        {
			switch (Player.Self.bulletDir) {
			case 1:
				rb.AddForce (transform.up * forza, ForceMode2D.Impulse);
				StartCoroutine ("LancioVeleno");
				break;
			case 2:
				rb.AddForce (transform.up * -forza, ForceMode2D.Impulse);
				StartCoroutine ("LancioVeleno");
				break;
			case 3:
				rb.AddForce (transform.right * forza, ForceMode2D.Impulse);
				StartCoroutine ("LancioVeleno");
				break;
			case 4:
				rb.AddForce (transform.right * -forza, ForceMode2D.Impulse);
				StartCoroutine ("LancioVeleno");
				break;
			}
        }

       
    	IEnumerator LancioVeleno()
        {
			if (started == false) {
				started = true;
				yield return new WaitForSeconds (0.85f);
				Debug.Log ("Veleno arrivato a destinazione");
				isExploded = true;
				rb.drag = 500;
				bc.enabled = true;
				yield return new WaitForSeconds (0.4f);

				GameController.Self.PassTurn ();
				Player.Self.ResetPlayerVar ();

				//Disabilitiamo la sua sprite
				this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				//Attiviamo la sprite dell'esplosione
				this.gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = true;
			}
        }


        public void OnCollisionEnter2D(Collision2D coll)
        {

            if (coll.gameObject.tag == "Walls" || coll.gameObject.tag == "Colonne")
            {
				Debug.Log ("Nome oggetto toccato: " + coll.gameObject.name);
				GameController.Self.PassTurn ();
				Player.Self.ResetPlayerVar ();

				Destroy(this.gameObject);

            }
           
        }

        
}
}
