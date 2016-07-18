using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IndianaBones
{

    public class Scaramucca : MonoBehaviour
    {

        public int xPosition;
        public int yPosition;
        public int xOld;
        public int yOld;
        public int movimento = 0;
        public bool attivo = false;
        public int TipoMovimento = 1;
        public int RangePattuglia = 3;
        Transform targetTr;
        public int RangeInterno;
        public int RangeOld = 0;

        [Header("Level and Stats")]
        [Space(10)]

        public int powerLevel;
        public List<EnemyLevels> levelsList = new List<EnemyLevels>();

        void Start()
        {
            Grid elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            targetTr = elementi.scacchiera[xPosition, yPosition].transform;

            RangeInterno = RangePattuglia;



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

        public void MovimentoAsseX()
        {
            Grid elementi = FindObjectOfType<Grid>();

            if ((RangeInterno > 0) && (elementi.scacchiera[xPosition + 1, yPosition].status != 4))

            {

                xPosition += 1;
                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                RangeInterno--;
                RangeOld++;
            }
            else if ((RangeInterno <= 0) && (elementi.scacchiera[xPosition - 1, yPosition].status != 4))


            {
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

        public void MovimentoAsseY()
        {
            Grid elementi = FindObjectOfType<Grid>();

            if ((RangeInterno > 0) && (elementi.scacchiera[xPosition, yPosition + 1].status != 4))
            {

                yPosition += 1;
                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                RangeInterno--;
                RangeOld++;
            }
            else if ((RangeInterno <= 0) && (elementi.scacchiera[xPosition, yPosition - 1].status != 4))
            {
                yPosition -= 1;
                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                RangeOld++;
                if ((RangeOld / 2) == RangePattuglia)
                {
                    RangeInterno = RangePattuglia;
                    RangeOld = 0;

                }

            }
        }

        void Update()
        {
            //Controlliamo se la vita va a zero e in tal caso aggiungiamo gli exp al player prendendoli dalle stats del livello corretto
           /* if (this.levelsList[powerLevel].Life <= 0)
            {
                Player.Self.expCollected += levelsList[powerLevel].Exp;
                //da aggiungere la distruzione del nemico da valutare con l'animazione relativa alla morte

            }*/

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

    

