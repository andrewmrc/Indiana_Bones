using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Bomba : Character {

        Grid elementi;
        Rigidbody2D rb;
        BoxCollider2D bc;
        

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

            switch (objPlayer.bulletDir)
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
        yield return new WaitForSeconds(0.85f);
        
            rb.drag = 500;
            bc.enabled = true;

            yield return new WaitForSeconds(0.4f);
            GameController.Self.PassTurn();
            Destroy(this.gameObject);
            Debug.LogWarning("sono qui");
        }


        public void OnCollisionEnter2D(Collision2D coll)
        {

            if (coll.gameObject.tag == "Walls" || coll.gameObject.tag == "Colonne")
            {
                
                GameController.Self.PassTurn();
                Player.Self.ResetPlayerVar();

                Destroy(this.gameObject);

            }
           
        }

        void Update () {

           
        }
}
}
