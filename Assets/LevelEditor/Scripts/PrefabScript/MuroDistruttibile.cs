using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class MuroDistruttibile : MonoBehaviour
    {
        int x;
        int y;

        public int DannoPerDistruggerlo = 4;
        Grid elementi;

        void Start()
        {
            elementi = FindObjectOfType<Grid>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            elementi.scacchiera[x, y].status = 3;
            elementi.scacchiera[x, y].name = "muro";

			this.gameObject.tag = "Walls";
            


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


			//Special Weapons
			if (coll.gameObject.tag == "Molotov")
			{
				gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
				StartCoroutine(ResetMyColor());
				DannoPerDistruggerlo -= Player.Self.currentAttack;

			}
			if (coll.gameObject.tag == "Veleno")
			{
				gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
				StartCoroutine(ResetMyColor());
				DannoPerDistruggerlo -= Player.Self.currentAttack;
			}

        }


		public void OnCollisionEnter2D(Collision2D coll)
		{

			if (coll.gameObject.name == "dente(Clone)") {
				gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 0, 0, 255);
				StartCoroutine (ResetMyColor ());
				DannoPerDistruggerlo -= Player.Self.currentAttack;
			} else if (coll.gameObject.tag == "FeverAttack") {
				gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 0, 0, 255);
				StartCoroutine (ResetMyColor ());
				DannoPerDistruggerlo = 0;
			}
		}


        IEnumerator ResetMyColor()
        {
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }

        void Update()
        {
            if (DannoPerDistruggerlo <= 0)
            {
                elementi.scacchiera[x, y].status = 0;
                Destroy(this.gameObject);
            }
        }

    }
}
