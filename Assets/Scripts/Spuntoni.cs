using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace IndianaBones
{

    public class Spuntoni : Character
    {

        bool seen = false;
       

        public int DannoSpuntoni = 1;

        public bool attivo = false;
       
        
        private Grid elementi;

        public int turnoSpuntone = 1;
       
        void Awake()
        {
            gameObject.tag = "Enemy";
        }
        
        void Start()
        {
            elementi = FindObjectOfType<Grid>();

            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            this.transform.position =  elementi.scacchiera[xPosition, yPosition].transform.position;


        }

        public void AttackHandler()
        {
            //Formula calcolo attacco 
            //il risultato si sottrae alla vita del player

            
            Player.Self.currentLife -= DannoSpuntoni;
            
            Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
            StartCoroutine(ResetPlayerColor());
            GameController.Self.PassTurn();
           
        }

        IEnumerator ResetPlayerColor()
        {
            yield return new WaitForSeconds(0.3f);
            Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }




        


        void Update()
        {
            
            if (turnoSpuntone == 3)
            {
                SpriteRenderer spuntoni = GetComponent<SpriteRenderer>();
                spuntoni.sprite = Resources.Load("Spuntoni_on", typeof(Sprite)) as Sprite;
            }
            else
            {
                SpriteRenderer spuntoni = GetComponent<SpriteRenderer>();
                spuntoni.sprite = Resources.Load("Spuntoni_off", typeof(Sprite)) as Sprite;
            }

            if (gameObject.GetComponent<TurnHandler>().itsMyTurn)
            {

                if (turnoSpuntone < 3)
                    turnoSpuntone++;
                else if (turnoSpuntone == 3)
                    turnoSpuntone = 1;
                    

                    

                    if (turnoSpuntone == 3 && Player.Self.transform.position == elementi.scacchiera[xPosition, yPosition].transform.position)
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
           

            
                

            

            
        }
    }
}

    

