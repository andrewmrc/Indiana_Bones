using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Vaso : MonoBehaviour
    {
        int x;
        int y;
        bool isDestroyed;

        public int DannoPerDistruggerlo = 1;
        Grid elementi;

        AudioSource audioVaso;

        void Awake()
        {
            gameObject.tag = "Enemy";
        }


        void Start()
        {
            elementi = FindObjectOfType<Grid>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            elementi.scacchiera[x, y].status = 3;
            elementi.scacchiera[x, y].name = "vaso";


            audioVaso = this.GetComponent<AudioSource>();
            

        }

        public void OnCollisionEnter2D(Collision2D coll)
        {


            if (coll.gameObject.name == "dente(Clone)")
            {

                DannoPerDistruggerlo -= Player.Self.currentAttack;

                

                Destroy(coll.gameObject);




            }

        }


        public void OnTriggerEnter2D(Collider2D coll)
        {

            //Handle life subtraction
            if (coll.gameObject.name == "up")
            {
                if (Player.Self.croce == 1)
                {

                    gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                    StartCoroutine(ResetMyColor());
                    DannoPerDistruggerlo -= Player.Self.currentAttack;

                }
            }
            if (coll.gameObject.name == "down")
            {
                if (Player.Self.croce == 3)
                {

                    gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                    StartCoroutine(ResetMyColor());
                    DannoPerDistruggerlo -= Player.Self.currentAttack;

                }
            }
            if (coll.gameObject.name == "right")
            {
                if (Player.Self.croce == 2)
                {

                    gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                    StartCoroutine(ResetMyColor());
                    DannoPerDistruggerlo -= Player.Self.currentAttack;

                }
            }
            if (coll.gameObject.name == "left")
            {
                if (Player.Self.croce == 4)
                {


                    gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                    StartCoroutine(ResetMyColor());
                    DannoPerDistruggerlo -= Player.Self.currentAttack;

                }
            }


        }

        IEnumerator ResetMyColor()
        {
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }

        IEnumerator HandleDestroy()
        {

            audioVaso.clip = AudioContainer.Self.SFX_RotturaVaso;
            audioVaso.Play();


            yield return new WaitForSeconds(0.6f);


            elementi.scacchiera[x, y].status = 0;
            Destroy(this.gameObject);

			//Chiama la funzione di drop item
			DropHandler.Self.DropItems("Cammello", this.transform.position.x, this.transform.position.y);

        }

        void Update()
        {
            if (DannoPerDistruggerlo <= 0 && isDestroyed == false)
            { 
                isDestroyed = true;
                StartCoroutine(HandleDestroy());
            }
        }

    }
}
