using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class ArmaFinale : MonoBehaviour
    {
        public bool attvazioneArma = false;

        public float forza = 5f;
        bool seen = false;

		Grid elementi;
		Rigidbody2D rb;
		BoxCollider2D bc;

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
            if (!GetComponent<Renderer>().isVisible)
            {
                Debug.Log("Dente uscito dallo schermo -> Player passa il turno");
                GameController.Self.PassTurn();
                Player.Self.ResetPlayerVar();

                Destroy(this.gameObject);
            }

            //tasto di attivazione dell'abilità da cambiare in base al richiamo dell'inventario
            


        }


        public void OnCollisionEnter2D(Collision2D coll)
        {


            if (coll.gameObject.tag == "Walls" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Colonne")
            {
                Debug.Log("Nome oggetto toccato: " + coll.gameObject.name);
                GameController.Self.PassTurn();
                Player.Self.ResetPlayerVar();

                Destroy(this.gameObject);

            }



        }
    }
}
