using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Dente : MonoBehaviour
    {

        public GameObject dente;
        
        public float forza = 0.5f;

        bool seen = false;



     

        // Update is called once per frame
        void FixedUpdate()
        {
            Rigidbody2D rb = dente.GetComponent<Rigidbody2D>();

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
            if (GetComponent<Renderer>().isVisible)
                seen = true;

            if (seen && !GetComponent<Renderer>().isVisible)
                Destroy(gameObject);
        }

        public void OnCollisionEnter2D(Collision2D coll)
        {



            if (coll.gameObject.name == "muro")
            {

           
                Destroy(this.gameObject);
               


            }

            



        }
    }
}
