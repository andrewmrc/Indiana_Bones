using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

namespace IndianaBones
{
    

    public class Canubi : Character
    {
        public int attacco = 1;
        public int vita = 1;
        public float speed = 2;
        public Transform targetTr;
        public bool active = false;
        GameObject giocatore;
        public int distanzaAttivazione = 10;

        
        [Header("Level and Stats")]
        [Space(10)]
        
        public List<EnemyLevels> levelsList = new List<EnemyLevels>();
        

        void Start()
        {

            

                Grid elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            targetTr = elementi.scacchiera[xPosition, yPosition].transform;


        }


        protected override void SetupStats()
        {
            lifePoints = 2;
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
            Grid elementi = FindObjectOfType<Grid>();
            GameController gamec = FindObjectOfType<GameController>();
            Player player = FindObjectOfType<Player>();
            Canubi canubiEnemy = FindObjectOfType<Canubi>();
            if (active == true)
            {
                if (vita > 0)
                {
                    if (ManhattanDist() > 1)

                    {
                        if ((player.xPosition != canubiEnemy.xPosition) && (player.yPosition != canubiEnemy.yPosition))
                        {
                            if (player.xPosition < canubiEnemy.xPosition)
                                if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                                    canubiEnemy.xPosition -= 1;
                                else if (elementi.scacchiera[xPosition , yPosition +1].status < 2)
                                    canubiEnemy.yPosition += 1;
                            if (player.xPosition > canubiEnemy.xPosition)

                                if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                                    canubiEnemy.xPosition += 1;
                                else if (elementi.scacchiera[xPosition , yPosition -1].status < 2)
                                    canubiEnemy.yPosition -= 1;




                        }
                        else if (player.yPosition == canubiEnemy.yPosition)
                        {
                            if (player.yPosition < canubiEnemy.yPosition)
                                if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                                    canubiEnemy.xPosition -= 1;
                                else if (elementi.scacchiera[xPosition , yPosition+1].status < 2)
                                    canubiEnemy.yPosition += 1;
                            if (player.yPosition > canubiEnemy.yPosition)

                                if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                                    canubiEnemy.xPosition += 1;
                                else if (elementi.scacchiera[xPosition , yPosition-1].status < 2)
                                    canubiEnemy.yPosition -= 1;


                        }
                        else if (player.xPosition == canubiEnemy.xPosition)
                        {
                            if (player.yPosition < canubiEnemy.yPosition)
                                if (elementi.scacchiera[xPosition, yPosition - 1].status < 2)
                                    canubiEnemy.yPosition -= 1;
                                else if (elementi.scacchiera[xPosition+1, yPosition].status < 2)
                                    canubiEnemy.xPosition += 1;
                            if (player.yPosition > canubiEnemy.yPosition)

                                if (elementi.scacchiera[xPosition, yPosition + 1].status < 2)
                                    canubiEnemy.yPosition += 1;
                                else if (elementi.scacchiera[xPosition-1, yPosition].status < 2)
                                    canubiEnemy.xPosition -= 1;

                        }

                        targetTr = elementi.scacchiera[canubiEnemy.xPosition, canubiEnemy.yPosition].transform;
                    }

                    


                    else if (ManhattanDist() == 1)

                    {
                        gamec.turno = 1;
                        foreach (var statistiche in levelsList)
                        
                         //viene richiamato il valore di attacco corrente del nemico e passato al metodo in Player
                         //per essere sottratto alla sua vita  
                        
                        if (attacco == 1)
                            player.controlloVita(statistiche.Attack);
                        attacco = 0;
                        gamec.turno = 1;
                    }

                    
                }
                
            }
            gamec.turno = 1;

        }
        public void OldValue()
        {
            Grid elementi = FindObjectOfType<Grid>();
            xOld = (int)this.transform.position.x;
            yOld = (int)this.transform.position.y;
            elementi.scacchiera[xOld, yOld].status = 0;

        }

        void Update()
        {
            Player giocatore = FindObjectOfType<Player>();

            //nel caso in cui il valore Life del nemico è minore/uguale a zero vengono incrementati gli exp del player
            foreach (var statistiche in levelsList)
                if (statistiche.Life <= 0)
                {
                    giocatore.expCollected += statistiche.Exp;

                    //da aggiungere la distruzione del nemico da valutare con l'animazione relativa alla morte
                }

            Grid elementi = FindObjectOfType<Grid>();

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
        }


    }
}