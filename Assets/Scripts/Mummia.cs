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

		public GameObject itemKey;
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

        GameObject healthBar;

        int prestabilito = 1;
       
        public int direzioneLancio = 0;
        float forza = 0.5f;
        public int tipologia;

        [Header("Level and Stats")]
        [Space(10)]

		public int powerLevel;
        public List<EnemyLevels> levelsList = new List<EnemyLevels>();

        SpriteRenderer feedback;

        void Awake()
        {
            gameObject.tag = "Enemy";



            //Settiamo l'animator e i parametri per le animazioni
            animator = GetComponent<Animator>();
            animator.SetBool("Walk", false);
        }

        void Start()
        {     
			vita = levelsList [powerLevel].Life;
			attackPower = levelsList [powerLevel].Attack;

            elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            targetTr = elementi.scacchiera[xPosition, yPosition].transform;

            healthBar = GameObject.FindGameObjectWithTag("EnemyHealthBar");

            //Get the animator component and set the parameter equal to the initial life value
            animator = GetComponent<Animator>();
            //animator.SetFloat ("Life", vita);
            audioMummy = GetComponent<AudioSource>();

            feedback = this.transform.GetChild(0).GetComponent<SpriteRenderer>();

        }


        public int ManhattanDist()
        {
            return (Mathf.Abs((int)Player.Self.transform.position.x - (int)this.transform.position.x) + Mathf.Abs((int)Player.Self.transform.position.y - (int)this.transform.position.y));
        }

        IEnumerator UpdateHealthBar()
        {
			if (healthBar == null) {
				healthBar = GameObject.FindGameObjectWithTag ("EnemyHealthBar");
			}
            healthBar.GetComponentInParent<Mask>().enabled = false;
            //healthBar.SetActive(true);
            healthBar.GetComponent<Slider>().maxValue = levelsList[powerLevel].Life;
            healthBar.transform.GetChild(3).GetComponent<Text>().text = (vita.ToString() + "/" + levelsList[powerLevel].Life.ToString());
            healthBar.transform.GetChild(1).GetComponent<Text>().text = ("Lv. " + powerLevel.ToString());
            healthBar.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Resources.Load("EnemyIcons/Head_Mummia", typeof(Sprite)) as Sprite;
            healthBar.GetComponent<Slider>().value = vita;
            yield return new WaitForSeconds(0.7f);
            //healthBar.SetActive(false);
            feedback.enabled = false;
            //Abilitiamo la maschera
            healthBar.GetComponentInParent<Mask>().enabled = true;

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

        /*public void AggiornamentoBarraVitaNemico()
        {
            EnemyScrollBar elementi = FindObjectOfType<EnemyScrollBar>();
            elementi.EnemyLifeBar.text = "Mummia : " + vita.ToString();
        }*/

        //gestisce i tre tipi di movimento della mummia, se il player non è nella sua linea di tiro ne fa partire uno random
        public void GestoreMovimenti()
        {
           

                switch (Casuale())
                {
                    case 1:
                        
                    Posizione();
                    animator.SetBool("Walk", true);
                    break;

                    case 2:

                    Fermo();
                    break;

                
                      
                        

                }
           
        }

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

        public void HandleDamageFromPlayer()
        {
            //Attiva il proprio feedback
            feedback.enabled = true;

            //Formula calcolo attacco Player
            int randomX = Random.Range(1, 3);
            int damage = (int)(Player.Self.currentAttack * randomX / 2);
            //Sottrae vita a questo nemico
            vita -= damage;
            Debug.Log("Questo nemico: " + this.gameObject.name + "-> subisce dal Player un totale danni di: " + damage);
            StartCoroutine(UpdateHealthBar());

        }

        public void OnTriggerEnter2D(Collider2D coll) 
        {

			//Handle life subtraction
            if (coll.gameObject.name == "up")
            {
                if (Player.Self.croce == 1)
                {

                    HandleDamageFromPlayer();

                }
            }
            if (coll.gameObject.name == "down")
            {
                if (Player.Self.croce == 3)
                {

                    HandleDamageFromPlayer();

                }
            }
            if (coll.gameObject.name == "right")
            {
                if (Player.Self.croce == 2)
                {

                    HandleDamageFromPlayer();

                }
            }
            if (coll.gameObject.name == "left")
            {
                if (Player.Self.croce == 4)
                {

                    HandleDamageFromPlayer();

                }
            }

			//Special Weapons
			if (coll.gameObject.tag == "Molotov")
			{
				feedback.enabled = true;
				vita -= 4;

				StartCoroutine(UpdateHealthBar());

			}
			if (coll.gameObject.tag == "Veleno")
			{
				feedback.enabled = true;
				vita -= 4;

				StartCoroutine(UpdateHealthBar());

			}
        }

        public void OnCollisionEnter2D(Collision2D coll)
        {


            if (coll.gameObject.name == "dente(Clone)")
            {

				feedback.enabled = true;
				//Formula calcolo attacco Player
				int randomX = Random.Range(1, 3);
				int damage = (int)(Player.Self.currentAttack*randomX/2);
				//Sottrae vita a questo nemico
				vita -= damage+1;
				Debug.Log("Questo nemico: " + this.gameObject.name + "-> subisce dal Player un totale danni di: " + damage);
				StartCoroutine(UpdateHealthBar());

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
            yield return new WaitForSeconds(0.4f);
            BoxCollider2D bc = GetComponent<BoxCollider2D>();
            bc.enabled = true;
            direzioneLancio = 0;
            
        }


        void Update()
        {
			if (vita > 0 && gameObject.GetComponent<TurnHandler>().itsMyTurn && !isAttacking)
            {
               
				//Colora di verde il personaggio per far capire che è il suo turno
				//gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);

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
                //inserito per sicurezza il passaggio turno non appena muore la mummia
                GameController.Self.PassTurn();
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

                if (transform.position == targetTr.position)
                {
                    animator.SetBool("Walk", false);
                }

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

            //Abilitiamo la maschera
            healthBar.GetComponentInParent<Mask>().enabled = true;

            //Aggiungiamo gli exp al player prendendoli dalle stats del livello corretto
            Player.Self.IncreaseExp(levelsList[powerLevel].Exp);

			elementi.scacchiera[xPosition, yPosition].status = 0;
            Destroy(this.gameObject);

			//Chiama la funzione di drop item
			Instantiate(itemKey).gameObject.transform.position =  elementi.scacchiera[xPosition,yPosition].transform.position;

			//Abilitiamo la maschera
			healthBar.GetComponentInParent<Mask>().enabled = true;
        }

    }
}