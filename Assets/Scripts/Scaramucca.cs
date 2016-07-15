using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Scaramucca : Character
    {

        public int RangePattuglia = 3;
        Transform targetTr;
        public int RangeInterno;
        public int RangeOld = 0;

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

            if (RangeInterno > 0)
            {

                xPosition += 1;
                targetTr = elementi.scacchiera[xPosition +1, yPosition].transform;
                RangeInterno--;
            }
            else
            { 
                xPosition -= 1;
                targetTr = elementi.scacchiera[xPosition - 1, yPosition].transform;
                
                if (RangeOld == RangePattuglia)
                {
                    RangeInterno = RangePattuglia;
                    MovimentoScaramucca();
                }
                else 
                RangeOld++;

                
            }

        }

        void Update()
        {
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
