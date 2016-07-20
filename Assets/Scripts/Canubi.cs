using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

namespace IndianaBones
{
    

    public class Canubi : MonoBehaviour
    {
        bool seen = false;
        public int xPosition;
        public int yPosition;
        public int xOld;
        public int yOld;
        public int movimento = 0;
        public int attacco = 1;
        public int vita = 1;
        public float speed = 2;
        public Transform targetTr;
        public bool attivo = false;
        public bool active = false;
        GameObject giocatore;
        public int distanzaAttivazione = 10;
		private Grid elementi;
		private Animator animator;

        [Header("Level and Stats")]
        [Space(10)]

		public int powerLevel;
        public List<EnemyLevels> levelsList = new List<EnemyLevels>();

        void Start()
        {     
			vita = levelsList [powerLevel].Life;
			attacco = levelsList [powerLevel].Attack;

            elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            targetTr = elementi.scacchiera[xPosition, yPosition].transform;

			//Get the animator component and set the parameter equal to the initial life value
			animator = GetComponent<Animator>();
			animator.SetFloat ("Life", vita);

        }


       


        public int ManhattanDist()
        {
            Player elementiPlayer = FindObjectOfType<Player>();
            giocatore = elementiPlayer.gameObject;

            return (Mathf.Abs((int)giocatore.transform.position.x - (int)this.transform.position.x) + Mathf.Abs((int)giocatore.transform.position.y - (int)this.transform.position.y));
        }

        public void Posizione()
        {
            OldValue();
            
            //GameController gamec = FindObjectOfType<GameController>();
            //Canubi canubiEnemy = FindObjectOfType<Canubi>();
            if (active == true)
            {
                if (vita > 0)
                {
                    if (ManhattanDist() > 1)

                    {
                        if ((Player.Self.xPosition != xPosition) && (Player.Self.yPosition != yPosition))
                        {
                            if (Player.Self.xPosition < xPosition)
                                if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                                {
                                    elementi.scacchiera[xPosition - 1, yPosition].status = 3;
									
									//Flip the sprite
									gameObject.GetComponent<SpriteRenderer> ().flipX = false;

                                    xPosition -= 1;
                                }
                                else if (elementi.scacchiera[xPosition, yPosition + 1].status < 2)
                                {
                                    elementi.scacchiera[xPosition, yPosition + 1].status = 3;
                                    yPosition += 1;
                                   }
                            if (Player.Self.xPosition > xPosition)

                                if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                                {
                                    elementi.scacchiera[xPosition + 1, yPosition].status = 3;

									//Flip the sprite
									gameObject.GetComponent<SpriteRenderer> ().flipX = true;

                                    xPosition += 1;
                                }
                                else if (elementi.scacchiera[xPosition, yPosition - 1].status < 2)
                                {
                                    elementi.scacchiera[xPosition, yPosition - 1].status = 3;
                                    yPosition -= 1;
                                }




                        }
                        else if (Player.Self.yPosition == yPosition)
                        {
                            if (Player.Self.yPosition < yPosition)
                                if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                                { 
                                    elementi.scacchiera[xPosition - 1, yPosition].status = 3;

									//Flip the sprite
									gameObject.GetComponent<SpriteRenderer> ().flipX = true;

                                    xPosition -= 1;
                                }
                                else if (elementi.scacchiera[xPosition , yPosition+1].status < 2)
                                { 
                                    elementi.scacchiera[xPosition, yPosition + 1].status = 3;
                                    yPosition += 1;
                                }
                            if (Player.Self.yPosition > yPosition)

                                if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                                { 
                                    elementi.scacchiera[xPosition + 1, yPosition].status = 3;
									
									//Flip the sprite
									gameObject.GetComponent<SpriteRenderer> ().flipX = false;

                                    xPosition += 1;
                                }
                                else if (elementi.scacchiera[xPosition , yPosition-1].status < 2)
                                {
                                    elementi.scacchiera[xPosition, yPosition - 1].status = 3;
                                    yPosition -= 1;
                                }


                        }
                        else if (Player.Self.xPosition == xPosition)
                        {
                            if (Player.Self.yPosition < yPosition)
                                if (elementi.scacchiera[xPosition, yPosition - 1].status < 2)
                                {
                                    elementi.scacchiera[xPosition, yPosition - 1].status = 3;
                                    yPosition -= 1;
                                }
                                else if (elementi.scacchiera[xPosition+1, yPosition].status < 2)
                                {
                                    elementi.scacchiera[xPosition + 1, yPosition].status = 3;
									
									//Flip the sprite
									gameObject.GetComponent<SpriteRenderer> ().flipX = true;

                                    xPosition += 1;
                                }
                            if (Player.Self.yPosition > yPosition)

                                if (elementi.scacchiera[xPosition, yPosition + 1].status < 2)
                                {
                                    elementi.scacchiera[xPosition, yPosition + 1].status = 3;
                                    yPosition += 1;
                                }
                                else if (elementi.scacchiera[xPosition-1, yPosition].status < 2)
                                {
                                    elementi.scacchiera[xPosition - 1, yPosition].status = 3;
									
									//Flip the sprite
									gameObject.GetComponent<SpriteRenderer> ().flipX = false;

                                    xPosition -= 1;
                                }

                        }

                        targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                    }

                    


                    else if (ManhattanDist() == 1)

                    {
                        GameController.Self.turno = 1;
                        foreach (var statistiche in levelsList)
                        
                         //viene richiamato il valore di attacco corrente del nemico e passato al metodo in Player
                         //per essere sottratto alla sua vita  
                        
                        if (attacco == 1)
                            Player.Self.controlloVita(statistiche.Attack);
                        attacco = 0;
                        GameController.Self.turno = 1;
                    }

                    
                }
                
            }
            GameController.Self.turno = 1;

        }

        public void OnTriggerEnter2D(Collider2D coll) 
        {


            if (coll.gameObject.name == "up")
            {
                if (Player.Self.croce == 1)
                {

                    vita -= Player.Self.currentAttack;

                    //sottraggo vita al player

                    Player.Self.currentLife -= attacco;

                }
            }
            if (coll.gameObject.name == "down")
            {
                if (Player.Self.croce == 3)
                {

                    vita -= Player.Self.currentAttack;

                    Player.Self.currentLife -= attacco;

                }
            }
            if (coll.gameObject.name == "right")
            {
                if (Player.Self.croce == 2)
                {

                    vita -= Player.Self.currentAttack;

                    Player.Self.currentLife -= attacco;

                }
            }
            if (coll.gameObject.name == "left")
            {
                if (Player.Self.croce == 4)
                {

                    vita -= Player.Self.currentAttack;

                    Player.Self.currentLife -= attacco;

                }
            }

            
        }

        public void OnCollisionEnter2D(Collision2D coll)
        {


            if (coll.gameObject.name == "dente(Clone)")
            {

                vita -= Player.Self.currentAttack;

                Destroy(coll.gameObject);




            }

        }

        public void Valore3()
        {
            elementi.scacchiera[xPosition, yPosition].status = 3;
        }

        public void OldValue()
        {

            xOld = xPosition;
            yOld = yPosition;
            elementi.scacchiera[xOld, yOld].status = 0;

        }

        


        void Update()
        {

            if (GetComponent<Renderer>().isVisible)
            {
                seen = true;
            }

            if (seen && !GetComponent<Renderer>().isVisible)
            {
                seen = false;
            }

            //Controlliamo se la vita va a zero e in tal caso aggiungiamo gli exp al player prendendoli dalle stats del livello corretto
            if (vita <= 0)
            {
				Player.Self.expCollected += levelsList[powerLevel].Exp;
				StartCoroutine (HandleDeath ());
            }


            if (vita > 0)
                elementi.scacchiera[xPosition, yPosition].status = 3;

            if (ManhattanDist() < distanzaAttivazione)
                active = true;

            

            Vector3 distance = targetTr.position - this.transform.position;
            Vector3 direction = distance.normalized;

            transform.position = transform.position + direction * 2 * speed * Time.deltaTime;

            if (distance.magnitude < 0.1f)
            {
                transform.position = targetTr.position;

            }

            if (attivo == true && seen == true)
            {

                Posizione();
                attivo = false;
            }

        }

		IEnumerator HandleDeath(){
			//Activate the death animation
			animator.SetFloat ("Life", vita);
			yield return new WaitForEndOfFrame();
			print("current clip length = " + animator.GetCurrentAnimatorStateInfo(0).length);
			yield return new WaitForSeconds (2f);

			elementi.scacchiera[xPosition, yPosition].status = 0;
			Destroy(this.gameObject);
		}

    }
}