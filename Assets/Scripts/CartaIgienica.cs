﻿using UnityEngine;
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
            if (GetComponent<Renderer>().isVisible)
                seen = true;

            if (seen && !GetComponent<Renderer>().isVisible)
            {
                Destroy(gameObject);

                
            }
        }


        public void OnCollisionEnter2D(Collision2D coll)
        {
            Mummia elementi = FindObjectOfType<Mummia>();

			if (coll.gameObject.name == "muro" || coll.gameObject.tag == "Colonne")
            {
				Debug.Log ("Nome oggetto toccato: " + coll.gameObject.name);

                //Player.Self.ResetPlayerVar ();


                Destroy(this.gameObject);

            }

            if (coll.gameObject.tag == "Player")
            {

                Player.Self.currentLife -= elementi.attackPower;
                    Destroy(this.gameObject);
            }

        }


    }
}
