﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace IndianaBones
{

    public class Cammello : Character
    {

        bool seen = false;
        
        public int attackPower = 1;
        public int vita = 1;
        
        public bool attivo = false;
        
        public Transform targetTr;
        
        private Grid elementi;

		private Animator animator;

		GameObject healthBar;

        [Header("Level and Stats")]
        [Space(10)]

        public int powerLevel;
        public List<EnemyLevels> levelsList = new List<EnemyLevels>();

        AudioSource audioCamel;
        public AudioClip SFX_Death;
        public AudioClip SFX_Attack;

        void Awake()
        {
            gameObject.tag = "Enemy";
        }
        
        void Start()
        {
            vita = levelsList[powerLevel].Life;
			attackPower = levelsList[powerLevel].Attack;

            elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            this.transform.position =  elementi.scacchiera[xPosition, yPosition].transform.position;

            elementi.scacchiera[xPosition, yPosition].status = 3;

			healthBar = GameObject.FindGameObjectWithTag ("EnemyHealthBar");

			//Get the animator component and set the parameter equal to the initial life value
			animator = GetComponent<Animator>();
			animator.SetFloat ("Life", vita);

            audioCamel = GetComponent<AudioSource>();

        }

        public int ManhattanDist()
        {
            return (Mathf.Abs((int)Player.Self.transform.position.x - (int)this.transform.position.x) + Mathf.Abs((int)Player.Self.transform.position.y - (int)this.transform.position.y));
        }


		IEnumerator UpdateHealthBar()
		{
			healthBar.GetComponentInParent<Mask> ().enabled = false;
			healthBar.SetActive (true);
			healthBar.GetComponent<Slider> ().maxValue = levelsList[powerLevel].Life;
			healthBar.transform.GetChild(3).GetComponent<Text>().text = (vita.ToString() + "/" + levelsList[powerLevel].Life.ToString());
			healthBar.transform.GetChild(1).GetComponent<Text>().text = ("Lv. " + powerLevel.ToString());
			healthBar.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = Resources.Load("EnemyIcons/Head_Cammello", typeof(Sprite)) as Sprite;
			healthBar.GetComponent<Slider> ().value = vita;
			yield return new WaitForSeconds (0.7f);
			healthBar.SetActive (false);

		}


        public void OnCollisionEnter2D(Collision2D coll)
        {


            if (coll.gameObject.name == "dente(Clone)")
            {

                vita -= Player.Self.currentAttack;

				StartCoroutine(UpdateHealthBar());

                Destroy(coll.gameObject);




            }

        }

    

		public void AttackHandler()
		{
            
            audioCamel.clip = SFX_Attack;
            audioCamel.Play();

            //Formula calcolo attacco Canubi
            int randomX = Random.Range(1, 3);
			int damage = (int)(attackPower*randomX/2);
			//Sottrae vita al player
			Player.Self.currentLife -= damage;
			Debug.Log("Attacco di: " + this.gameObject.name + "-> toglie al Player: " + damage);
			Player.Self.gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 0, 0, 255);
			StartCoroutine (ResetPlayerColor ());
			//Passa il turno
			GameController.Self.PassTurn();
			StartCoroutine (ResetMyColor ());

		}


        IEnumerator ResetPlayerColor()
        {
            yield return new WaitForSeconds(0.3f);
            Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }


        IEnumerator ResetMyColor()
        {
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }


		public void OnTriggerEnter2D(Collider2D coll) 
		{

			//Handle life subtraction
			if (coll.gameObject.name == "up")
			{
				if (Player.Self.croce == 1)
				{
					HandleDamageFromPlayer ();
				}
			}
			if (coll.gameObject.name == "down")
			{
				if (Player.Self.croce == 3)
				{
					HandleDamageFromPlayer ();
				}
			}
			if (coll.gameObject.name == "right")
			{
				if (Player.Self.croce == 2)
				{
					HandleDamageFromPlayer ();
				}
			}
			if (coll.gameObject.name == "left")
			{
				if (Player.Self.croce == 4)
				{
					HandleDamageFromPlayer ();
				}
			}


		}


		public void HandleDamageFromPlayer () {

			//Formula calcolo attacco Player
			int randomX = Random.Range(1, 3);
			int damage = (int)(Player.Self.currentAttack*randomX/2);
			//Sottrae vita a questo nemico
			vita -= damage;
			Debug.Log("Questo nemico: " + this.gameObject.name + "-> subisce dal Player un totale danni di: " + damage);
			StartCoroutine(UpdateHealthBar());

		}


        void Update()
        {

			if (vita > 0 &&  gameObject.GetComponent<TurnHandler>().itsMyTurn)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);

				if (ManhattanDist () > 1) {
					if (Player.Self.xPosition > xPosition) {
						gameObject.GetComponent<SpriteRenderer> ().flipX = true;
					} else {
						gameObject.GetComponent<SpriteRenderer> ().flipX = false;
					}
				} 

				if (ManhattanDist() == 1) {
                    AttackHandler();
                } else {
                    GameController.Self.PassTurn();
                    StartCoroutine(ResetMyColor());
                }
            }


            if (GetComponent<Renderer>().isVisible)
            {
                if (!GameController.Self.charactersList.Contains(this.gameObject))
                {
                    GameController.Self.charactersList.Add(this.gameObject);
                }
                seen = true;
            }
            else if (!GetComponent<Renderer>().isVisible)
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
				Debug.Log ("Questo Cammello è sconfitto");
				GameController.Self.charactersList.Remove(this.gameObject);
				//Settiamo lo status cella a 10 così il player non può ataccare nè camminare su questa casella fino a che questo nemico non sparisce dalla scena
				elementi.scacchiera[xPosition, yPosition].status = 10;
				StartCoroutine(HandleDeath());
			}
            
        }


		IEnumerator HandleDeath()
		{
            audioCamel.Stop();
            audioCamel.clip = SFX_Death;
            audioCamel.Play();

            //Activate the death animation
            animator.SetFloat("Life", vita);
			if (gameObject.GetComponent<TurnHandler>().itsMyTurn)
			{
				GameController.Self.PassTurn();
			}
			yield return new WaitForEndOfFrame();
			// print("current clip length = " + animator.GetCurrentAnimatorStateInfo(0).length);
			yield return new WaitForSeconds(2f);

			//Aggiungiamo gli exp al player prendendoli dalle stats del livello corretto
			Player.Self.IncreaseExp(levelsList[powerLevel].Exp);

			Destroy(this.gameObject);
			elementi.scacchiera[xPosition, yPosition].status = 0;

			//Chiama la funzione di drop item
			DropHandler.Self.DropItems("Cammello", this.transform.position.x, this.transform.position.y);

		}



    }
}

    

