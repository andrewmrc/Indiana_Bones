using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace IndianaBones
{

    public class Cammello : MonoBehaviour
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
        
        
        public Transform targetTr;
        
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

            this.transform.position =  elementi.scacchiera[xPosition, yPosition].transform.position;

            elementi.scacchiera[xPosition, yPosition].status = 3;





        }

        public int ManhattanDist()
        {
            return (Mathf.Abs((int)Player.Self.transform.position.x - (int)this.transform.position.x) + Mathf.Abs((int)Player.Self.transform.position.y - (int)this.transform.position.y));
        }


        

        public void OnCollisionEnter2D(Collision2D coll)
        {


            if (coll.gameObject.name == "dente(Clone)")
            {

                vita -= Player.Self.currentAttack;

                Destroy(coll.gameObject);




            }

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
                    GameController.Self.PassTurn();
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

            
                

            

            if (attivo == true)
            {

                
                attivo = false;
            }
        }
    }
}

    

