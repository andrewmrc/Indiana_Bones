using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Dente : MonoBehaviour
    {

        public GameObject dente;
        

        public float forza = 0.5f;

        
        

        void Awake()
        {
           
        }

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

        public void OnCollisionEnter2D(Collision2D coll)
        {



            if (coll.gameObject.name == "muro")
            {

           
                Destroy(this.gameObject);
               


            }

            



        }
    }
}
