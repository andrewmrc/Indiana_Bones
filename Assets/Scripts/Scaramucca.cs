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
        public int attacco = 1;
        public int vita = 1;
        public int movimento = 0;
        public bool attivo = false;
        public int TipoMovimento = 1;
        public int RangePattuglia = 3;
        public Transform targetTr;
        public int RangeInterno;
        public int RangeOld = 0;
        private Grid elementi;

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
            attacco = levelsList[powerLevel].Attack;

            elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            targetTr = elementi.scacchiera[xPosition, yPosition].transform;

            RangeInterno = RangePattuglia;

            

        }

        public int ManhattanDist()
        {
            return (Mathf.Abs((int)Player.Self.transform.position.x - (int)this.transform.position.x) + Mathf.Abs((int)Player.Self.transform.position.y - (int)this.transform.position.y));
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

        public void MovimentoScaramucca()
        {
            if (attivo == true)
            {
                if (TipoMovimento == 1)
                    MovimentoAsseY();

                else if (TipoMovimento == 2)
                    MovimentoAsseX();
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
                if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                {

                    elementi.scacchiera[xPosition + 1, yPosition].status = 3;
                    xPosition += 1;
                    targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                    RangeInterno--;
                    RangeOld++;
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




                }
            }

            //GameController.Self.turno = 1;
            GameController.Self.PassTurn();
        }

        public void MovimentoAsseY()
        {
            OldValue();

            if ((RangeInterno > 0) && (elementi.scacchiera[xPosition, yPosition + 1].status != 4))
            {

                elementi.scacchiera[xPosition, yPosition + 1].status = 3;
                yPosition += 1;
                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                RangeInterno--;
                RangeOld++;
            }
            else if ((RangeInterno <= 0) && (elementi.scacchiera[xPosition, yPosition - 1].status != 4))
            {
                elementi.scacchiera[xPosition, yPosition - 1].status = 3;
                yPosition -= 1;
                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                RangeOld++;
                if ((RangeOld / 2) == RangePattuglia)
                {
                    RangeInterno = RangePattuglia;
                    RangeOld = 0;

                }

            }
            GameController.Self.PassTurn();
        }

        public void AttackHandler()
        {
            //Formula calcolo attacco 
            //il risultato si sottrae alla vita del player
            int damage = levelsList[powerLevel].Attack;
            Player.Self.currentLife -= damage;
            GameController.Self.PassTurn();
        }


        void Update()
        {

            if (gameObject.GetComponent<TurnHandler>().itsMyTurn)
            {
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


            //Controlliamo se la vita va a zero e in tal caso aggiungiamo gli exp al player prendendoli dalle stats del livello corretto
            if (vita <= 0)
             {
                elementi.scacchiera[xPosition, yPosition].status = 0;
                Player.Self.expCollected += levelsList[powerLevel].Exp;
                Destroy(this.gameObject);
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

            if (attivo == true)
            {

                MovimentoScaramucca();
                attivo = false;
            }
        }
    }
}

    

