using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IndianaBones
{
    

    public class Enemy : Character
    {
        public int attacco = 1;
        public int vita = 1;
        public float speed = 2;
        public Transform targetTr;
        public bool active = false;
        GameObject giocatore;
        public int distanzaAttivazione = 10;
       
		public List<EnemyLevels> levelsList = new List<EnemyLevels> ();
        

        void Start()
        {
            Grid elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            targetTr = elementi.scacchiera[xPosition, yPosition].transform;


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
            Canubi lara = FindObjectOfType<Canubi>();
            if (active == true)
            {
                if (vita > 0)
                {
                    if (ManhattanDist() > 1)

                    {
                        if ((player.xPosition != lara.xPosition) && (player.yPosition != lara.yPosition))
                        {
                            if (player.xPosition < lara.xPosition)
                                if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                                    lara.xPosition -= 1;
                                else
                                    lara.yPosition += 1;
                            if (player.xPosition > lara.xPosition)

                                if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                                    lara.xPosition += 1;
                                else
                                    lara.yPosition -= 1;




                        }
                        else if (player.yPosition == lara.yPosition)
                        {
                            if (player.yPosition < lara.yPosition)
                                if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                                    lara.xPosition -= 1;
                                else
                                    lara.yPosition += 1;
                            if (player.yPosition > lara.yPosition)

                                if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                                    lara.xPosition += 1;
                                else
                                    lara.yPosition -= 1;


                        }
                        else if (player.xPosition == lara.xPosition)
                        {
                            if (player.yPosition < lara.yPosition)
                                if (elementi.scacchiera[xPosition, yPosition - 1].status < 2)
                                    lara.yPosition -= 1;
                                else
                                    lara.xPosition += 1;
                            if (player.yPosition > lara.yPosition)

                                if (elementi.scacchiera[xPosition, yPosition + 1].status < 2)
                                    lara.yPosition += 1;
                                else
                                    lara.xPosition -= 1;

                        }

                        targetTr = elementi.scacchiera[lara.xPosition, lara.yPosition].transform;
                    }

                    


                    else if (ManhattanDist() == 1)

                    {
                        foreach(var statistiche in levelsList)

                        if (attacco == 1)
                            player.controlloVita(statistiche.Attack);
                        attacco = 0;
                        ///gamec.turno = 1;
                    }


                }
                
            }
           // gamec.turno = 1;

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