using UnityEngine;
using System.Collections;

namespace IndianaBones
{

	public class Bomba : MonoBehaviour {

        Grid elementi;
        Rigidbody2D rb;
        BoxCollider2D bc;
        

        float forza = 0.1f;
        bool seen = false;
		bool started;

        void Awake()
        {
            gameObject.tag = "Molotov";
        }

        void Start () {

            elementi = FindObjectOfType<Grid>();
            rb = GetComponent<Rigidbody2D>();
            bc = GetComponent<BoxCollider2D>();

        }

        void FixedUpdate()
        {
             

			switch (Player.Self.bulletDir)
            {
                case 1:
                    rb.AddForce(transform.up * forza, ForceMode2D.Impulse);
                    StartCoroutine("DestroyBomb");
                    break;
                case 2:
                    rb.AddForce(transform.up * -forza, ForceMode2D.Impulse);
                    StartCoroutine("DestroyBomb");
                    break;
                case 3:
                    rb.AddForce(transform.right * forza, ForceMode2D.Impulse);
                    StartCoroutine("DestroyBomb");
                    break;
                case 4:
                    rb.AddForce(transform.right * -forza, ForceMode2D.Impulse);
                    StartCoroutine("DestroyBomb");
                    break;
            }


        }

       
    IEnumerator DestroyBomb()
        {
			if (started == false) {
				started = true;
				yield return new WaitForSeconds (0.85f);
	        
				rb.drag = 500;
				bc.enabled = true;

				//Attiviamo la sprite dell'esplosione
				this.gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = true;

				yield return new WaitForSeconds (0.4f);
				GameController.Self.PassTurn ();
				Player.Self.ResetPlayerVar ();

				yield return new WaitForSeconds (1f);

				Destroy (this.gameObject);
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

        void Update () {
			//Controlla se la bomba esce dallo schermo
			if (!GetComponent<Renderer> ().isVisible) {
				Debug.Log ("Bomba uscita dallo schermo -> Player passa il turno");
				GameController.Self.PassTurn ();
				Player.Self.ResetPlayerVar ();

				Destroy(this.gameObject);
			}
           
        }
}
}
