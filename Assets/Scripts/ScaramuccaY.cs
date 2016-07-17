using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IndianaBones
{

    public class ScaramuccaY : Character
    {

        public int RangePattuglia = 3;
        Transform targetTr;
        public int RangeInterno;
        public int RangeOld = 0;

        [Header("Level and Stats")]
        [Space(10)]

        public List<EnemyLevels> levelsList = new List<EnemyLevels>();

        void Start()
        {
            Grid elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            targetTr = elementi.scacchiera[xPosition, yPosition].transform;

            RangeInterno = RangePattuglia;



        }

        protected override void SetupStats()
        {
            lifePoints = 2;
        } 

        public void MovimentoScaramucca()
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
                if ((RangeOld/2) == RangePattuglia)
                {
                    RangeInterno = RangePattuglia;
                    RangeOld = 0;
                    
                }
                
                

                
            }

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


            Vector3 distance = targetTr.position - this.transform.position;
            Vector3 direction = distance.normalized;

            transform.position = transform.position + direction * 2  * Time.deltaTime;

            if (distance.magnitude < 0.1f)
            {
                transform.position = targetTr.position;

            }
        }
    }
}
