using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace IndianaBones
{

    public class Scaramucca : MonoBehaviour
    {

        bool seen = false;
        public int xPosition;
        public int yPosition;
        public int xOld;
        public int yOld;
        public int attackPower = 1;
        public int vita = 1;
        public int movimento = 0;
        public bool attivo = false;
        public int TipoMovimento = 1;
        public int RangePattuglia = 3;
        public Transform targetTr;
        public int RangeInterno;
        public int RangeOld = 0;
        private Grid elementi;
		private Animator animator;

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
            vita = levelsList[powerLevel].Life;
            attackPower = levelsList[powerLevel].Attack;

            elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            targetTr = elementi.scacchiera[xPosition, yPosition].transform;

            RangeInterno = RangePattuglia;

			//Get the animator component and set the parameter equal to the initial life value
			animator = GetComponent<Animator>();
			animator.SetFloat ("Life", vita);


        }

        public int ManhattanDist()
        {
            return (Mathf.Abs((int)Player.Self.transform.position.x - (int)this.transform.position.x) + Mathf.Abs((int)Player.Self.transform.position.y - (int)this.transform.position.y));
        }


		public void OnTriggerEnter2D(Collider2D coll) 
		{

			//Handle life subtraction
			if (coll.gameObject.name == "up")
			{
				if (Player.Self.croce == 1)
				{

					vita -= Player.Self.currentAttack;

				}
			}
			if (coll.gameObject.name == "down")
			{
				if (Player.Self.croce == 3)
				{

					vita -= Player.Self.currentAttack;

				}
			}
			if (coll.gameObject.name == "right")
			{
				if (Player.Self.croce == 2)
				{

					vita -= Player.Self.currentAttack;

				}
			}
			if (coll.gameObject.name == "left")
			{
				if (Player.Self.croce == 4)
				{

					vita -= Player.Self.currentAttack;

				}
			}

		}


        public void OnCollisionEnter2D(Collision2D coll)
        {


            if (coll.gameObject.name == "dente(Clone)")
            {

                vita -= Player.Self.currentAttack;

            }

        }


        public void MovimentoScaramucca()
        {
			if (TipoMovimento == 1) {
				MovimentoAsseY ();
			} else if (TipoMovimento == 2) {
				MovimentoAsseX ();
			}
        }


        public void OldValue()
        {

            xOld = xPosition;
            yOld = yPosition;
            elementi.scacchiera[xOld, yOld].status = 0;

        }


        public void MovimentoAsseX()
        {
            OldValue();

            if (RangeInterno > 0)
            {
				if (elementi.scacchiera [xPosition + 1, yPosition].status < 2) {
					elementi.scacchiera [xPosition + 1, yPosition].status = 3;
					xPosition += 1;
					targetTr = elementi.scacchiera [xPosition, yPosition].transform;
					RangeInterno--;
					RangeOld++;
				} else if (elementi.scacchiera [xPosition - 1, yPosition].status < 2) {
					elementi.scacchiera [xPosition - 1, yPosition].status = 3;
					xPosition -= 1;
					targetTr = elementi.scacchiera [xPosition, yPosition].transform;
					RangeInterno = 0;
					RangeOld++;
				} else {
					Debug.Log (this.gameObject.name +" salta questo turno perchè circondata da ostacoli!");
				}
            }
            else if (RangeInterno <= 0)
            {
                if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                {
                    elementi.scacchiera[xPosition - 1, yPosition].status = 3;
                    xPosition -= 1;
                    targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                    RangeOld++;
                    if ((RangeOld / 2) == RangePattuglia)
                    {
                        RangeInterno = RangePattuglia;
                        RangeOld = 0;

                    }

				} else if (elementi.scacchiera [xPosition + 1, yPosition].status < 2) {
					elementi.scacchiera [xPosition + 1, yPosition].status = 3;
					xPosition += 1;
					targetTr = elementi.scacchiera [xPosition, yPosition].transform;
					RangeInterno = RangePattuglia;
					RangeOld = 0;
				} else {
					Debug.Log (this.gameObject.name +" salta questo turno perchè circondata da ostacoli!");
				}
            }

            GameController.Self.PassTurn();
			StartCoroutine (ResetMyColor ());

        }


        public void MovimentoAsseY()
        {
            OldValue();

            if ((RangeInterno > 0))
            {
				if (elementi.scacchiera [xPosition, yPosition + 1].status < 2) {
					elementi.scacchiera [xPosition, yPosition + 1].status = 3;
					yPosition += 1;
					targetTr = elementi.scacchiera [xPosition, yPosition].transform;
					RangeInterno--;
					RangeOld++;
				} else if (elementi.scacchiera [xPosition, yPosition - 1].status < 2) {
					elementi.scacchiera [xPosition, yPosition - 1].status = 3;
					yPosition -= 1;
					targetTr = elementi.scacchiera [xPosition, yPosition].transform;
					RangeInterno = 0;
					RangeOld++;
				} else {
					Debug.Log (this.gameObject.name +" salta questo turno perchè circondata da ostacoli!");
				}
            }
            else if ((RangeInterno <= 0))
            {
				if (elementi.scacchiera [xPosition, yPosition - 1].status < 2) {
					elementi.scacchiera [xPosition, yPosition - 1].status = 3;
					yPosition -= 1;
					targetTr = elementi.scacchiera [xPosition, yPosition].transform;
					RangeOld++;
					if ((RangeOld / 2) == RangePattuglia) {
						RangeInterno = RangePattuglia;
						RangeOld = 0;

					}
				} else if (elementi.scacchiera [xPosition, yPosition + 1].status < 2) {
					elementi.scacchiera [xPosition, yPosition + 1].status = 3;
					yPosition += 1;
					targetTr = elementi.scacchiera [xPosition, yPosition].transform;
					RangeInterno = RangePattuglia;
					RangeOld = 0;
				} else {
					Debug.Log (this.gameObject.name +" salta questo turno perchè circondata da ostacoli!");
				}

            }
            GameController.Self.PassTurn();
			StartCoroutine (ResetMyColor ());

        }


		public void AttackHandler()
		{
			//Formula calcolo attackPower Canubi
			int randomX = Random.Range(1, 3);
			int damage = (int)(attackPower*randomX);
			//Sottrae vita al player
			Player.Self.currentLife -= damage;
			Debug.Log("attackPower di: " + this.gameObject.name + "-> toglie al Player: " + damage);
			Player.Self.gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 0, 0, 255);
			StartCoroutine (ResetPlayerColor ());
			//Passa il turno
			GameController.Self.PassTurn();
			StartCoroutine (ResetMyColor ());

		}


		IEnumerator ResetPlayerColor (){
			yield return new WaitForSeconds (0.3f);
			Player.Self.gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
		}

		IEnumerator ResetMyColor (){
			yield return new WaitForSeconds (0.2f);
			gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255 ,255);
		}


        void Update()
        {

            if (vita > 0 && gameObject.GetComponent<TurnHandler>().itsMyTurn)
            {
				gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0 ,255);

                if (ManhattanDist() == 1)
                {
                    AttackHandler();
                }
                else
                {
                    MovimentoScaramucca();
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
				GameController.Self.charactersList.Remove(this.gameObject);
				//Settiamo lo status cella a 10 così il player non può ataccare nè camminare su questa casella fino a che questo nemico non sparisce dalla scena
				elementi.scacchiera[xPosition, yPosition].status = 10;
				StartCoroutine (HandleDeath ());
			}


            if (vita > 0)
                elementi.scacchiera[xPosition, yPosition].status = 3;

            Vector3 distance = targetTr.position - this.transform.position;
            Vector3 direction = distance.normalized;

            transform.position = transform.position + direction * 2 * Time.deltaTime;

            if (distance.magnitude < 0.1f)
            {
                transform.position = targetTr.position;

            }

			/*
            if (attivo == true)
            {

                MovimentoScaramucca();
                attivo = false;
            }*/
        }


		IEnumerator HandleDeath(){
			//Activate the death animation
			animator.SetFloat ("Life", vita);
			if (gameObject.GetComponent<TurnHandler> ().itsMyTurn) {
				GameController.Self.PassTurn ();
			}
			yield return new WaitForEndOfFrame();
			//print("current clip length = " + animator.GetCurrentAnimatorStateInfo(0).length);
			yield return new WaitForSeconds (1.5f);

			//Aggiungiamo gli exp al player prendendoli dalle stats del livello corretto
			Player.Self.IncreaseExp(levelsList[powerLevel].Exp);

			Destroy (this.gameObject);
			elementi.scacchiera[xPosition, yPosition].status = 0;

		}

    }
}

    

