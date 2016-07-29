using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace IndianaBones
{

    public class Player : Character
    {

		// Singleton Implementation
		protected static Player _self;
		public static Player Self

		{
			get
			{
				if (_self == null)
					_self = FindObjectOfType(typeof(Player)) as Player;
				return _self;
			}
		}


        bool canMove = true;
        bool onOff = false;
        bool crossActive = false;
        //bool attacco = false;
		bool isAttacking = false;

        public bool endMove;
		bool isDead;

        public int croce = 1;

        AudioSource audioPlayer;
        //inserisco le clip audio in previsione che i designer possano sostituirle con delle altre
        public AudioClip SFX_LancioDente;


        public GameObject up;
        public GameObject down;
        public GameObject right;
        public GameObject left;

		GameObject healthBar;
		GameObject manaBar;
		GameObject expBar;      

        public int proiettili = 5;
        public GameObject dente;
        public int x;
        public int y;
        public int bulletDir = 3;
        public float speed = 2;
        public Transform targetTr;
        private Animator animator;
        public Text nDenti;
        public Text healthText;
		public Text manaText;
		public Text playerLevelText;
		public Text expCollText;
		public GameObject canvasGameOver;
        public GameObject child;
		public Grid elementi;
		GameObject canvasUI;
		SpriteRenderer feedback;

        [Header("Level and Stats")]
        [Space(10)]
        public int playerLevel = 1;
        public int currentLife;
        public int currentAttack;
        public int currentMana;

        public float expToLevelUp;
        public int expCollected;
        public int startingLife;
        public int startingAttack;
        public int startingMana;

        //public List<PlayerLevels> levelsList = new List<PlayerLevels> ();

        public static Player Instance;

        void Awake()
        {
            //Metodo per passare l'oggetto da una scena all'altra
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            if (playerLevel == 1)
            {
                //Set the starting stats of the player
                currentLife = startingLife;
                currentAttack = startingAttack;
                currentMana = startingMana;
            }

			//Settiamo l'animator e i parametri per le animazioni
			animator = GetComponent<Animator>();
			animator.SetBool("Walk", false);

        }


        void Start()
        {
			/*
            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            elementi = FindObjectOfType<Grid>();
            this.transform.position = elementi.scacchiera[xPosition, yPosition].transform.position;
            targetTr = elementi.scacchiera[xPosition, yPosition].transform;
			elementi.scacchiera[xPosition, yPosition].status = 4;*/

			//Settiamo tutti i parametri e le reference per il player
			UpdatePlayer ();

			//Troviamo il componente AudioSource sul Player
			audioPlayer = GetComponent<AudioSource>();
						       
        }


		public void SetPlayerUI () {
			//Prepara la UI
			healthText = GameObject.Find ("VitaText").GetComponent<Text>();
			manaText = GameObject.Find ("ManaText").GetComponent<Text>();
			nDenti = GameObject.Find ("CounterDentiText").GetComponent<Text> ();
			playerLevelText = GameObject.Find ("PlayerLevelText").GetComponent<Text> ();
			healthBar = GameObject.FindGameObjectWithTag ("PlayerHealthBar");
			manaBar = GameObject.FindGameObjectWithTag ("PlayerManaBar");
			expBar = GameObject.FindGameObjectWithTag ("PlayerExpBar");
			expCollText = expBar.transform.GetChild(1).GetComponent<Text> ();
			expCollText.gameObject.SetActive (false);

			healthText = GameObject.Find ("VitaText").GetComponent<Text>();
			nDenti = GameObject.Find ("CounterDentiText").GetComponent<Text> ();

			healthBar.GetComponent<Slider> ().maxValue = startingLife;
			healthBar.GetComponent<Slider> ().value = startingLife;

			manaBar.GetComponent<Slider> ().maxValue = startingMana;
			manaBar.GetComponent<Slider> ().value = startingMana;

			expBar.GetComponent<Slider> ().maxValue = expToLevelUp;
			expBar.GetComponent<Slider> ().value = expCollected;

			canvasUI = GameObject.FindGameObjectWithTag ("CanvasUI");

			canvasGameOver = GameObject.FindGameObjectWithTag ("CanvasGameOver");
			canvasGameOver.SetActive (false);
			Debug.Log ("Scena caricata, Player UI recuperata");
		}


		public void UpdatePlayer () {
			xPosition = (int)this.transform.position.x;
			yPosition = (int)this.transform.position.y;
			elementi = FindObjectOfType<Grid>();
			targetTr = elementi.scacchiera[(int)GameController.Self.startPoint.transform.position.x, (int)GameController.Self.startPoint.transform.position.y].transform;
			elementi.scacchiera[xPosition, yPosition].status = 4;
			OldValue ();
			gameObject.GetComponent<TurnHandler> ().itsMyTurn = true;
			SetPlayerUI ();
			Debug.Log ("Scena caricata, Player Reference recuperate");
		}


		public void ResetPlayerVar(){
			onOff = false;
			crossActive = false;
			canMove = true;
			isAttacking = false;
			CrossActivationHandler ();
		}


        public void AttaccoADistanza()
        {
			if (proiettili > 0) {
				GameObject nuovoDente;
				switch (croce) {

				case 1:
                        audioPlayer.clip = SFX_LancioDente;
                        audioPlayer.Play();
                    proiettili -= 1;
					bulletDir = 1;
					nuovoDente = Instantiate (dente);
					nuovoDente.transform.position = up.transform.position;
					break;

				case 2:
                        audioPlayer.clip = SFX_LancioDente;
                        audioPlayer.Play();
                    proiettili -= 1;
					bulletDir = 3;
					nuovoDente = Instantiate (dente);
					nuovoDente.transform.position = right.transform.position;
					break;

				case 3:
                        audioPlayer.clip = SFX_LancioDente;
                        audioPlayer.Play();
                    proiettili -= 1;
					nuovoDente = Instantiate (dente);
					nuovoDente.transform.position = down.transform.position;
					bulletDir = 2;
					break;

				case 4:
                        audioPlayer.clip = SFX_LancioDente;
                        audioPlayer.Play();
                    proiettili -= 1;
					nuovoDente = Instantiate (dente);
					nuovoDente.transform.position = left.transform.position;
					bulletDir = 4;
					break;

				}

			}


        }


        public void Attacco()
        {
			if (croce == 1 && endMove == false)
            {
                up.GetComponent<BoxCollider2D>().enabled = true;

            }

			if (croce == 2 && endMove == false)
            {
                right.GetComponent<BoxCollider2D>().enabled = true;

            }

			if (croce == 3 && endMove == false)
            {
                down.GetComponent<BoxCollider2D>().enabled = true;

            }

			if (croce == 4 && endMove == false)
            {
                left.GetComponent<BoxCollider2D>().enabled = true;

            }

            StartCoroutine(PostAttacco(0.4f));
        }


        IEnumerator PostAttacco(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            //Set false to the attack parameter of animation
            animator.SetBool("Attack", false);

            up.GetComponent<BoxCollider2D>().enabled = false;
            right.GetComponent<BoxCollider2D>().enabled = false;
            down.GetComponent<BoxCollider2D>().enabled = false;
            left.GetComponent<BoxCollider2D>().enabled = false;
			GameController.Self.PassTurn ();
			ResetPlayerVar ();

        }


        public void OldValue()
        {
			xOld = xPosition;
            yOld = yPosition;
			elementi.scacchiera[x, y].status = 0;
        }


		public void SetPlayerCellStatus (){
			elementi.scacchiera[xOld, yOld].status = 4;
		}

        public void PickUp()
        {
            //Grid elementi = FindObjectOfType<Grid>();
            if (elementi.scacchiera[xPosition, yPosition].status == -1)
                proiettili += 5;
        }


        IEnumerator Camminata(float seconds)
        {
            yield return new WaitForSeconds(seconds); ;
            animator.SetBool("Walk", false);
        }


		//Metodo chiamato dai nemici per incrementare gli exp del Player
		public void IncreaseExp(int expToAdd){
			expCollected += expToAdd;
			StartCoroutine (ExpUIHandler (expToAdd));
		}


		//Gestisce la visibilità dei punti exp nella UI
		public IEnumerator ExpUIHandler (int expValue){
			Debug.Log ("UI EXP");
			expCollText.gameObject.SetActive (true);
			expCollText.text = ("  +"+ expValue +" exp");
			yield return new WaitForSeconds (1f);
			expCollText.gameObject.SetActive (false);
		}


		//Gestisce il level up del player
        public void LevelUp()
        {
            //Save the previous required exp
            float expToPreviousLevelUp = expToLevelUp;

            //Set the new exp required to level up to the next level
            expToLevelUp = expToPreviousLevelUp * 1.5f;

            //Increase Life each level up
			startingLife++;
			currentLife = startingLife;

            //Increase Attack if the player level is pair
            if (playerLevel % 2 == 0)
            {
				currentAttack++;
            }

            //Increase Mana if the player level is odd
            if (playerLevel % 2 != 0)
            {
				//currentMana = currentMana + 3;
				startingMana = startingMana + 3;
				currentMana = startingMana;
            }

			//Update exp collected starting from zero
			expCollected = expCollected - (int) expToPreviousLevelUp;
			if (expCollected < 0) {
				expCollected = 0;
			}

			//Aggiorna le barre con i nuovi valori max
			healthBar.GetComponent<Slider> ().maxValue = startingLife;
			manaBar.GetComponent<Slider> ().maxValue = currentMana;
			expBar.GetComponent<Slider> ().maxValue = expToLevelUp;

			//Ricarica i denti
			proiettili = 5;
        }


        public void AttackDirection()
        {
            if (croce == 1)
            {
                ResetCrossColor();
                SpriteRenderer colore = up.GetComponent<SpriteRenderer>();
				colore.sprite = Resources.Load("Freccia_On", typeof(Sprite)) as Sprite;
            }

            if (croce == 2)
            {
                ResetCrossColor();
                SpriteRenderer colore = right.GetComponent<SpriteRenderer>();
				colore.sprite = Resources.Load("Freccia_On", typeof(Sprite)) as Sprite;
            }

            if (croce == 3)
            {
                ResetCrossColor();
                SpriteRenderer colore = down.GetComponent<SpriteRenderer>();
				colore.sprite = Resources.Load("Freccia_On", typeof(Sprite)) as Sprite;
            }

            if (croce == 4)
            {
                ResetCrossColor();
                SpriteRenderer colore = left.GetComponent<SpriteRenderer>();
				colore.sprite = Resources.Load("Freccia_On", typeof(Sprite)) as Sprite;
            }

        }



        public void ResetCrossColor()
        {

            SpriteRenderer colore = up.GetComponent<SpriteRenderer>();
            colore.sprite = Resources.Load("Freccia_Off", typeof(Sprite)) as Sprite;


            SpriteRenderer colore1 = right.GetComponent<SpriteRenderer>();
			colore1.sprite = Resources.Load("Freccia_Off", typeof(Sprite)) as Sprite;


            SpriteRenderer colore2 = down.GetComponent<SpriteRenderer>();
			colore2.sprite = Resources.Load("Freccia_Off", typeof(Sprite)) as Sprite;


            SpriteRenderer colore3 = left.GetComponent<SpriteRenderer>();
			colore3.sprite = Resources.Load("Freccia_Off", typeof(Sprite)) as Sprite;


        }


        void Update()
        {

			x = xPosition;
			y = yPosition;

			if (currentLife > 0 && gameObject.GetComponent<TurnHandler>().itsMyTurn && isAttacking == false)
            {
				
				//Movimento verso destra
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    croce = 2;
                    AttackDirection();
                    //Flip the sprite of the player
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;

					if (canMove == true && isAttacking == false)
                    {
                        if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                        {
							Debug.Log ("Muovi Destra");
                            canMove = false;
                            PickUp();
                            animator.SetBool("Walk", true);
                            //StartCoroutine(Camminata(0.5f));
                            bulletDir = 3;
                            OldValue();
                            xPosition += 1;
                            targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                            elementi.scacchiera[xPosition, yPosition].status = 4;
                            endMove = true;
						} 
                    }
                }


				//Attacco verso destra
				if (Input.GetKey (KeyCode.RightArrow)) {
					if (elementi.scacchiera [xPosition + 1, yPosition].status == 3 && isAttacking == false && endMove == false && canMove) {
						Debug.Log ("Player Attacca Destra");
						isAttacking = true;
						//Set true to the attack parameter of animation
						animator.SetBool ("Attack", true);
						Attacco ();
					}
				}


				//Movimento verso sinistra
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    croce = 4;
                    AttackDirection();
                    //Flip the sprite of the player
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;

					if (canMove == true && isAttacking == false)
                    {
                        if (xPosition > 0)
                        {
                            if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                            {
								Debug.Log ("Muovi sinistra");

                                canMove = false;
                                PickUp();
                                animator.SetBool("Walk", true);
                                //StartCoroutine(Camminata(0.5f));
                                bulletDir = 4;
                                OldValue();
                                xPosition -= 1;
                                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                                elementi.scacchiera[xPosition, yPosition].status = 4;
                                endMove = true;

							} 
                        }
                    }
                }


				//Attacco verso sinistra
				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					if (elementi.scacchiera [xPosition - 1, yPosition].status == 3 && isAttacking == false && endMove == false && canMove) {
						Debug.Log ("Player Attacca Sinistra");
						isAttacking = true;
						//Set true to the attack parameter of animation
						animator.SetBool ("Attack", true);
						Attacco ();
					}
				}


				//Movimento verso il basso
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    croce = 3;
                    AttackDirection();
					if (canMove == true && isAttacking == false)
                    {
                        if (yPosition > 0)
                        {
                            if (elementi.scacchiera[xPosition, yPosition - 1].status < 2)
                            {
								Debug.Log ("Muovi Giù");

                                canMove = false;
                                PickUp();
                                animator.SetBool("Walk", true);
                                //StartCoroutine(Camminata(0.5f));
                                bulletDir = 2;
                                OldValue();
                                yPosition -= 1;
                                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                                elementi.scacchiera[xPosition, yPosition].status = 4;
                                endMove = true;
							} 
                        }
                    }
                }


				//Attacco verso il basso
				if (Input.GetKeyDown (KeyCode.DownArrow)) {
					if (elementi.scacchiera [xPosition, yPosition - 1].status == 3 && isAttacking == false && endMove == false && canMove) {
						Debug.Log ("Player Attacca Giù");
						isAttacking = true;
						//Set true to the attack parameter of animation
						animator.SetBool ("Attack", true);
						Attacco ();
					}
				}


				//Movimento verso l'alto
				if (Input.GetKey(KeyCode.UpArrow))
                {
                    croce = 1;
                    AttackDirection();
					if (canMove == true && isAttacking == false)
                    {
                        if (yPosition < 100)
                        {
                            if (elementi.scacchiera[xPosition, yPosition + 1].status < 2)
                            {
								Debug.Log ("Muovi Su");

                                canMove = false;
                                PickUp();
                                animator.SetBool("Walk", true);
                                //StartCoroutine(Camminata(0.5f));
                                bulletDir = 1;
                                OldValue();
                                yPosition += 1;
                                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                                elementi.scacchiera[xPosition, yPosition].status = 4;
                                endMove = true;
							} 
                        }
                    }
                }


				//Attacco verso l'alto
				if (Input.GetKeyDown (KeyCode.UpArrow)) {
					if (elementi.scacchiera [xPosition, yPosition + 1].status == 3 && isAttacking == false && endMove == false && canMove) {
						Debug.Log ("Player Attacca Su");
						isAttacking = true;
						//Set true to the attack parameter of animation
						animator.SetBool ("Attack", true);
						Attacco ();
					}
				}
            

                //Check if the player have reached the required exp to level up
                if (expCollected >= expToLevelUp)
                {
                    playerLevel++;
                    LevelUp();
                }

				if (elementi == null) {
					UpdatePlayer ();
				}

                Vector3 distance = targetTr.position - this.transform.position;
                Vector3 direction = distance.normalized;

                transform.position = transform.position + direction * speed * Time.deltaTime;


				//Gestisce il fine turno dopo un movimento
                if (endMove)
                {
					//Centra il player nella casella di destinazione alla fine di un movimento
                    if (distance.magnitude < 0.20f && crossActive == false)
                    {

                        transform.position = targetTr.position;
                        if (transform.position == targetTr.position)
                        {
                            endMove = false;
							canMove = true;
							animator.SetBool("Walk", false);
							//elementi.scacchiera[xPosition, yPosition].status = 4;

                            GameController.Self.PassTurn();
                            Debug.Log("Finito turno con movimento del Player");
                        }
                        
                    }

                }


                if (Input.GetKeyDown("space"))
                {
					if (endMove == false) {
						//Set true to the attack parameter of animation
						animator.SetBool ("Attack", true);
						Attacco ();
					}
                }

                
                if (Input.GetKeyDown(KeyCode.Z) && onOff == false && endMove == false)
                {
                    onOff = true;
					CrossActivationHandler ();
                    canMove = false;
                    crossActive = true;
                }
                else if (Input.GetKeyDown(KeyCode.Z) && onOff == true)
                {
                    onOff = false;
					CrossActivationHandler ();
                    canMove = true;
                    crossActive = false;
                }


				if (Input.GetKeyDown (KeyCode.LeftControl)) {
					if (onOff == true && proiettili > 0) {
						isAttacking = true;
						AttaccoADistanza ();
					}
				}
             
            }


			//Aggiorna la UI: vita/mana/exp/munizioni
			nDenti.text = (proiettili.ToString());
			healthText.text = (currentLife.ToString() + "/" + startingLife.ToString());
			manaText.text = (currentMana.ToString() + "/" + startingMana.ToString());
			playerLevelText.text = ("Lv. " + playerLevel.ToString());
			healthBar.GetComponent<Slider> ().value = currentLife;
			manaBar.GetComponent<Slider> ().value = currentMana;
			expBar.GetComponent<Slider> ().value = expCollected;


			//Gestisce la morte del player
			if (currentLife <= 0 && isDead == false) {
				currentLife = 0;
				isDead = true;
				//Activate the death animation
				animator.SetFloat ("Life", currentLife);

				//Attivare pop up che chiede se si vuole continuare il gioco o ricominciare
				StartCoroutine("ActivateGameOverPanel");
			}

        }


		IEnumerator ActivateGameOverPanel (){
			Debug.Log ("GAME OVER PANEL");
			canvasUI.SetActive (false);
			yield return new WaitForSeconds (2f);
			canvasGameOver.SetActive (true);
			currentLife = startingLife;
			currentMana = startingMana;
			proiettili = 5;
			isDead = false;
			animator.SetFloat ("Life", currentLife);
			gameObject.GetComponent<TurnHandler> ().itsMyTurn = false;
		}


		public void CrossActivationHandler() {
			//Attiva e disattiva visualizzazione croce
			if (onOff == true)
			{
				up.gameObject.GetComponent<SpriteRenderer> ().enabled = true; 
				down.gameObject.GetComponent<SpriteRenderer> ().enabled = true; 
				right.gameObject.GetComponent<SpriteRenderer> ().enabled = true; 
				left.gameObject.GetComponent<SpriteRenderer> ().enabled = true; 
				AttackDirection();

			}
			else if (onOff == false)
			{
				up.gameObject.GetComponent<SpriteRenderer> ().enabled = false; 
				down.gameObject.GetComponent<SpriteRenderer> ().enabled = false; 
				right.gameObject.GetComponent<SpriteRenderer> ().enabled = false; 
				left.gameObject.GetComponent<SpriteRenderer> ().enabled = false; 

			}
		}


    }
}
