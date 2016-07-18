﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

namespace IndianaBones
{
    

    public class Canubi : MonoBehaviour
    {
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
                                    xPosition -= 1;
                                else if (elementi.scacchiera[xPosition , yPosition +1].status < 2)
                                    yPosition += 1;
                            if (Player.Self.xPosition > xPosition)

                                if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                                    xPosition += 1;
                                else if (elementi.scacchiera[xPosition , yPosition -1].status < 2)
                                    yPosition -= 1;




                        }
                        else if (Player.Self.yPosition == yPosition)
                        {
                            if (Player.Self.yPosition < yPosition)
                                if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                                    xPosition -= 1;
                                else if (elementi.scacchiera[xPosition , yPosition+1].status < 2)
                                    yPosition += 1;
                            if (Player.Self.yPosition > yPosition)

                                if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                                    xPosition += 1;
                                else if (elementi.scacchiera[xPosition , yPosition-1].status < 2)
                                    yPosition -= 1;


                        }
                        else if (Player.Self.xPosition == xPosition)
                        {
                            if (Player.Self.yPosition < yPosition)
                                if (elementi.scacchiera[xPosition, yPosition - 1].status < 2)
                                    yPosition -= 1;
                                else if (elementi.scacchiera[xPosition+1, yPosition].status < 2)
                                    xPosition += 1;
                            if (Player.Self.yPosition > yPosition)

                                if (elementi.scacchiera[xPosition, yPosition + 1].status < 2)
                                    yPosition += 1;
                                else if (elementi.scacchiera[xPosition-1, yPosition].status < 2)
                                    xPosition -= 1;

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


        public void OldValue()
        {
            
            xOld = (int)this.transform.position.x;
            yOld = (int)this.transform.position.y;
            elementi.scacchiera[xOld, yOld].status = 0;

        }


        void Update()
        {     
			

			//Controlliamo se la vita va a zero e in tal caso aggiungiamo gli exp al player prendendoli dalle stats del livello corretto
			if (vita <= 0)
            {
				Player.Self.expCollected += levelsList[powerLevel].Exp;
                //da aggiungere la distruzione del nemico da valutare con l'animazione relativa alla morte

            }



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

            if (attivo == true)
            {

                Posizione();
                attivo = false;
            }

        }



    }
}