using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

namespace IndianaBones
{
    

    public class Mummia : Character
    {
        bool seen = false;


        public GameObject cartaIgienica;
        public int attackPower = 1;
        public int vita = 1;
        public float speed = 2;
        public Transform targetTr;
        public bool attivo = false;
        public bool rangeActive = false;
        public bool onMove;
        GameObject giocatore;
        public int distanzaAttivazione = 10;
		private Grid elementi;
		private Animator animator;
		bool isAttacking = false;
        bool isDestroyed;

        AudioSource audioMummy;

        int prestabilito = 1;
       
        public int direzioneLancio = 0;
        float forza = 0.5f;
        public int tipologia;

        [Header("Level and Stats")]
        [Space(10)]

		public int powerLevel;
        public List<EnemyLevels> levelsList = new List<EnemyLevels>();

        void Awake()
        {
            gameObject.tag = "Enemy";
        }

        void Start()
        {     
			vita = levelsList [powerLevel].Life;
			attackPower = levelsList [powerLevel].Attack;

            elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            targetTr = elementi.scacchiera[xPosition, yPosition].transform;

			//Get the animator component and set the parameter equal to the initial life value
			animator = GetComponent<Animator>();
            //animator.SetFloat ("Life", vita);
            audioMummy = GetComponent<AudioSource>();

        }


        public int ManhattanDist()
        {
            return (Mathf.Abs((int)Player.Self.transform.position.x - (int)this.transform.position.x) + Mathf.Abs((int)Player.Self.transform.position.y - (int)this.transform.position.y));
        }


		IEnumerator AttaccoADistanza()
        {
            audioMummy.clip = AudioContainer.Self.SFX_Mummy_Attack;
            audioMummy.Play();

            Debug.Log ("La mummia ha deciso di attaccare");
			isAttacking = true;
			//Set true to the attack parameter of animation
			animator.SetBool ("Attack", true);
			yield return new WaitForEndOfFrame();
			yield return new WaitForSeconds (0.5f);
            GameObject carta;

            BoxCollider2D bc = GetComponent<BoxCollider2D>();
            bc.enabled = false;      
                  
            carta = Instantiate(cartaIgienica);
            carta.transform.position = this.transform.position;

			//Set false to the attack parameter of animation
			animator.SetBool("Attack", false);

            StartCoroutine(ResetMyColor());
            StartCoroutine(EnabledCollider());


        }


        public int Casuale()
        {
            return Random.Range(1, 3);
           

        }

        public void AggiornamentoBarraVitaNemico()
        {
            EnemyScrollBar elementi = FindObjectOfType<EnemyScrollBar>();
            elementi.EnemyLifeBar.text = "Mummia : " + vita.ToString();
        }

        //gestisce i tre tipi di movimento della mummia, se il player non è nella sua linea di tiro ne fa partire uno random
        public void GestoreMovimenti()
        {
           

                switch (Casuale())
                {
                    case 1:
                        
                        Posizione();        
                        break;

                    case 2:

                    Fermo();
                    break;

                
                      
                        

                }
           
        }


     /*   public void MovimentoPrestabilito()
        {
            OldValue();
            

            if (prestabilito == 1)
            {
                if (Player.Self.x > xPosition)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    yPosition += 1;
                }
                else if (Player.Self.x < xPosition)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    yPosition += 1;
                }
                prestabilito++;
            }

            if (prestabilito == 2)
            {
                if (Player.Self.x > xPosition)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    xPosition += 1;
                }
                else if (Player.Self.x < xPosition)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    xPosition += 1;
                }
                prestabilito++;
            }

            if (prestabilito == 3)
            {
                if (Player.Self.x > xPosition)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    yPosition -= 1;
                }
                else if (Player.Self.x < xPosition)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    yPosition -= 1;
                }
                prestabilito++;
            }

            if (prestabilito == 4)
            {
                if (Player.Self.x > xPosition)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    xPosition -= 1;
                }
                else if (Player.Self.x < xPosition)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    xPosition -= 1;
                }
                prestabilito++;
            }

            if (prestabilito == 5)
                prestabilito = 1;

            targetTr = elementi.scacchiera[xPosition, yPosition].transform;
          

            StartCoroutine(ResetMyColor());

            GameController.Self.PassTurn();
        } */


        public void Fermo()
        {
           
            StartCoroutine(ResetMyColor());

            GameController.Self.PassTurn();
        }

        //controlla se il player è nella sua linea di tiro
        public void APortatadiTiro()
        {
			if (direzioneLancio == 0 && isAttacking == false)
            {
                if (Player.Self.xPosition == this.xPosition)
                {
                    if (Player.Self.yPosition > this.yPosition)
                    {
                        direzioneLancio = 1;
						StartCoroutine(AttaccoADistanza());
                    }
                    else if (Player.Self.yPosition < this.yPosition)
                    {
                        direzioneLancio = 2;
						StartCoroutine(AttaccoADistanza());
                    }
                }

                else if (Player.Self.yPosition == this.yPosition)
                {
                    if (Player.Self.xPosition > this.xPosition)
                    {
                        direzioneLancio = 3;
                        gameObject.GetComponent<SpriteRenderer>().flipX = true;
                        StartCoroutine(AttaccoADistanza());
                    }
                    else if (Player.Self.xPosition < this.xPosition)
                    {
                        direzioneLancio = 4;
                        gameObject.GetComponent<SpriteRenderer>().flipX = false;
                        StartCoroutine(AttaccoADistanza());
                    }
                }
                else
                    GestoreMovimenti();

            }


        }

        public void Posizione()
        {

            OldValue();
            //GameController gamec = FindObjectOfType<GameController>();
            //Canubi canubiEnemy = FindObjectOfType<Canubi>();
            if (rangeActive == true)
            {
                Debug.Log("range");

                if (vita > 0)
                {
                    if (ManhattanDist() > 1)

                    {
                        if ((Player.Self.xPosition != xPosition) && (Player.Self.yPosition != yPosition))
                        {
                            if (Player.Self.xPosition < xPosition)
                                if (elementi.scacchiera[xPosition - 1, yPosition].status < 1)
                                {
                                    elementi.scacchiera[xPosition - 1, yPosition].status = 3;

                                    //Flip the sprite
                                    gameObject.GetComponent<SpriteRenderer>().flipX = false;

                                    xPosition -= 1;
                                }

                                else if ((elementi.scacchiera[xPosition, yPosition - 1].status < 1))
                                {
                                    elementi.scacchiera[xPosition, yPosition - 1].status = 3;
                                    yPosition -= 1;
                                }

                                else if (elementi.scacchiera[xPosition, yPosition + 1].status < 1)
                                {
                                    elementi.scacchiera[xPosition, yPosition + 1].status = 3;
                                    yPosition += 1;
                                }
                            if (Player.Self.xPosition > xPosition)

                                if (elementi.scacchiera[xPosition + 1, yPosition].status < 1)
                                {
                                    elementi.scacchiera[xPosition + 1, yPosition].status = 3;

                                    //Flip the sprite
                                    gameObject.GetComponent<SpriteRenderer>().flipX = true;

                                    xPosition += 1;
                                }
                                else if (elementi.scacchiera[xPosition, yPosition - 1].status < 1)
                                {
                                    elementi.scacchiera[xPosition, yPosition - 1].status = 3;
                                    yPosition -= 1;
                                }
                        }
                        else if (Player.Self.yPosition == yPosition)
                        {
							if (Player.Self.xPosition < xPosition)
                                if (elementi.scacchiera[xPosition - 1, yPosition].status < 1)
                                {
                                    elementi.scacchiera[xPosition - 1, yPosition].status = 3;

                                    //Flip the sprite
									gameObject.GetComponent<SpriteRenderer>().flipX = false;

                                    xPosition -= 1;
                                }
                                else if (elementi.scacchiera[xPosition, yPosition + 1].status < 1)
                                {
                                    elementi.scacchiera[xPosition, yPosition + 1].status = 3;
                                    yPosition += 1;
                                }
							if (Player.Self.xPosition > xPosition)

                                if (elementi.scacchiera[xPosition + 1, yPosition].status < 1)
                                {
                                    elementi.scacchiera[xPosition + 1, yPosition].status = 3;

                                    //Flip the sprite
									gameObject.GetComponent<SpriteRenderer>().flipX = true;

                                    xPosition += 1;
                                }
                                else if (elementi.scacchiera[xPosition, yPosition - 1].status < 1)
                                {
                                    elementi.scacchiera[xPosition, yPosition - 1].status = 3;
                                    yPosition -= 1;
                                }


                        }
                        else if (Player.Self.xPosition == xPosition)
                        {
                            if (Player.Self.yPosition < yPosition)
                                if (elementi.scacchiera[xPosition, yPosition - 1].status < 1)
                                {
                                    elementi.scacchiera[xPosition, yPosition - 1].status = 3;
                                    yPosition -= 1;
                                }
                                else if (elementi.scacchiera[xPosition + 1, yPosition].status < 1)
                                {
                                    elementi.scacchiera[xPosition + 1, yPosition].status = 3;

                                    //Flip the sprite
                                    gameObject.GetComponent<SpriteRenderer>().flipX = true;

                                    xPosition += 1;
                                }
                            if (Player.Self.yPosition > yPosition)

                                if (elementi.scacchiera[xPosition, yPosition + 1].status < 1)
                                {
                                    elementi.scacchiera[xPosition, yPosition + 1].status = 3;
                                    yPosition += 1;
                                }
                                else if (elementi.scacchiera[xPosition - 1, yPosition].status < 1)
                                {
                                    elementi.scacchiera[xPosition - 1, yPosition].status = 3;

                                    //Flip the sprite
                                    gameObject.GetComponent<SpriteRenderer>().flipX = false;

                                    xPosition -= 1;
                                }

                        }

                        targetTr = elementi.scacchiera[xPosition, yPosition].transform;
						Debug.Log(this.gameObject.name + "ha deciso di muoversi sulla cella: " + targetTr);

                    }

                }

            }
            onMove = false;
            GameController.Self.PassTurn();
			StartCoroutine (ResetMyColor ());
            

        }


        public void OnTriggerEnter2D(Collider2D coll) 
        {

			//Handle life subtraction
            if (coll.gameObject.name == "up")
            {
                if (Player.Self.croce == 1)
                {

                    vita -= Player.Self.currentAttack;
                    AggiornamentoBarraVitaNemico();

                }
            }
            if (coll.gameObject.name == "down")
            {
                if (Player.Self.croce == 3)
                {

                    vita -= Player.Self.currentAttack;
                    AggiornamentoBarraVitaNemico();

                }
            }
            if (coll.gameObject.name == "right")
            {
                if (Player.Self.croce == 2)
                {

                    vita -= Player.Self.currentAttack;
                    AggiornamentoBarraVitaNemico();

                }
            }
            if (coll.gameObject.name == "left")
            {
                if (Player.Self.croce == 4)
                {

                    vita -= Player.Self.currentAttack;
                    AggiornamentoBarraVitaNemico();

                }
            }

            
        }

        public void OnCollisionEnter2D(Collision2D coll)
        {


            if (coll.gameObject.name == "dente(Clone)")
            {

                vita -= Player.Self.currentAttack;

                AggiornamentoBarraVitaNemico();

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

		/*
        public void AttackHandler()
        {
            //Formula calcolo attacco Canubi
			int randomX = Random.Range(1, 3);
			int damage = (int)(attackPower*randomX);
			//Sottrae vita al player
			Player.Self.currentLife -= damage;
			Debug.Log("Attacco di: " + this.gameObject.name + "-> toglie al Player: " + damage);
			Player.Self.gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 0, 0, 255);
			StartCoroutine (ResetPlayerColor ());
			//Passa il turno
            GameController.Self.PassTurn();
			StartCoroutine (ResetMyColor ());

        }*/


		IEnumerator ResetPlayerColor (){
			yield return new WaitForSeconds (0.3f);
			Player.Self.gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
		}

		IEnumerator ResetMyColor (){
			Debug.Log ("Resetta il colore mummia");
			yield return new WaitForSeconds (0.2f);
			gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255 ,255);
		}

        IEnumerator EnabledCollider()
        {
            yield return new WaitForSeconds(0.2f);
            BoxCollider2D bc = GetComponent<BoxCollider2D>();
            bc.enabled = true;
            direzioneLancio = 0;
            
        }


        void Update()
        {
			if (vita > 0 && gameObject.GetComponent<TurnHandler>().itsMyTurn && !isAttacking)
            {
               
				gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0 ,255);

                APortatadiTiro();
            }


			if (!gameObject.GetComponent<TurnHandler> ().itsMyTurn) {
				isAttacking = false;
			}


            if (GetComponent<Renderer>().isVisible)
            {
                if (!GameController.Self.charactersList.Contains(this.gameObject))
                {
                    GameController.Self.charactersList.Add(this.gameObject);
                }
                seen = true;
            } else if (!GetComponent<Renderer>().isVisible)
            {
                if (GameController.Self.charactersList.Contains(this.gameObject))
                {
                    GameController.Self.charactersList.Remove(this.gameObject);
                }
                seen = false;
            }


            //Controlliamo se la vita va a zero e chiamiamo il metodo che gestisce questo evento
            if (vita <= 0)
            {
                GameController.Self.charactersList.Remove(this.gameObject);
                if (isDestroyed == false)
                {
                    isDestroyed = true;

                    //Settiamo lo status cella a 10 così il player non può ataccare nè camminare su questa casella fino a che questo nemico non sparisce dalla scena
                    elementi.scacchiera[xPosition, yPosition].status = 10;
                    StartCoroutine(HandleDeath());
                }
            }


            if (vita > 0)
                elementi.scacchiera[xPosition, yPosition].status = 3;

            if (ManhattanDist() < distanzaAttivazione)
                rangeActive = true;

            

            Vector3 distance = targetTr.position - this.transform.position;
            Vector3 direction = distance.normalized;

            transform.position = transform.position + direction * 2 * speed * Time.deltaTime;

            if (distance.magnitude < 0.1f)
            {
                transform.position = targetTr.position;

            }
				
        }


		IEnumerator HandleDeath(){
			//Activate the death animation
			animator.SetFloat ("Life", vita);
			if (gameObject.GetComponent<TurnHandler> ().itsMyTurn) {
				GameController.Self.PassTurn ();
			}

            audioMummy.Stop();
            audioMummy.clip = AudioContainer.Self.SFX_Mummy_Death;
            audioMummy.Play();

            yield return new WaitForEndOfFrame();
			//print("current clip length = " + animator.GetCurrentAnimatorStateInfo(0).length);
			yield return new WaitForSeconds (2.5f);

			//Aggiungiamo gli exp al player prendendoli dalle stats del livello corretto
			Player.Self.IncreaseExp(levelsList[powerLevel].Exp);

			elementi.scacchiera[xPosition, yPosition].status = 0;
            Destroy(this.gameObject);

        }

    }
}