﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IndianaBones
{

    public class Scaramucca : Character
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

            if (RangeInterno > 0)
            {

                xPosition += 1;
                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                RangeInterno--;
                RangeOld++;
            }
            else
            { 
                xPosition -= 1;
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