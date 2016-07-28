using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Veleno : Character {

        Grid elementi;
        Rigidbody2D rb;
        BoxCollider2D bc;

        bool attivo = true;
        int turnoVeleno = 0; 

        float forza = 0.1f;
        bool seen = false;

        void Start () {

            elementi = FindObjectOfType<Grid>();
            rb = GetComponent<Rigidbody2D>();
            bc = GetComponent<BoxCollider2D>();

        }

        void FixedUpdate()
        {
             

            Player objPlayer = FindObjectOfType<Player>();

            if (attivo == true)
            {
                switch (objPlayer.bulletDir)
                {
                    case 1:
                        rb.AddForce(transform.up * forza, ForceMode2D.Impulse);
                        StartCoroutine("LancioVeleno");
                        break;
                    case 2:
                        rb.AddForce(transform.up * -forza, ForceMode2D.Impulse);
                        StartCoroutine("LancioVeleno");
                        break;
                    case 3:
                        rb.AddForce(transform.right * forza, ForceMode2D.Impulse);
                        StartCoroutine("LancioVeleno");
                        break;
                    case 4:
                        rb.AddForce(transform.right * -forza, ForceMode2D.Impulse);
                        StartCoroutine("LancioVeleno");
                        break;
                }
            }

        }

       
    IEnumerator LancioVeleno()
        {
        yield return new WaitForSeconds(0.85f);
        
            rb.drag = 500;
            bc.enabled = true;
            turnoVeleno = 1;
            attivo = false;

            
        }


        public void OnCollisionEnter2D(Collision2D coll)
        {

            if (coll.gameObject.tag == "Walls" || coll.gameObject.tag == "Colonne")
            {
                
                GameController.Self.PassTurn();
                
                Destroy(this.gameObject);
                

            }
           
        }

        void Update()
        {

            if (turnoVeleno < 3 && gameObject.GetComponent<TurnHandler>().itsMyTurn)
            {
                
                turnoVeleno += 1;
                GameController.Self.PassTurn();
                Debug.LogWarning("eccomi");

            }
            else if ( turnoVeleno >= 3)
            {
                GameController.Self.PassTurn();
                Destroy(this.gameObject);

            }


        }
}
}
