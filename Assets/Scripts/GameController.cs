using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace IndianaBones
{

    public class GameController : MonoBehaviour
    {

        // Singleton Implementation
        protected static GameController _self;
        public static GameController Self
        {
            get
            {
                if (_self == null)
                    _self = FindObjectOfType(typeof(GameController)) as GameController;
                return _self;
            }
        }


        public int turno = 1;
        public int turnoNemici = 1;
        //public Scrollbar vita;
        // public float barraVita = 1.0f;
        public List<GameObject> charactersList = new List<GameObject>();

        void Start()
        {
            charactersList.Add(Player.Self.gameObject);
            //Inseriamo tutti i nemici visibili nella lista e facciamo uno shuffle
            //charactersList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            /*for(int i = 1; i < charactersList.Count; i++)
            {/*
                if (charactersList[i].gameObject.GetComponent<SpriteRenderer>().isVisible)
                {
                    charactersList.Remove(charactersList[i]);
                    Debug.Log("Rimuovo: " + charactersList[i].gameObject.name);
                }*/
            //}
           
        }



        void Update()
        {
       



            //Player elementi = FindObjectOfType<Player>();

            /*
            if (elementi.movimento == 0)
                turno = 0;
            if (turno == 0 && turnoNemici == 1)
            {
                turnoNemici = 0;
                Scaramucca[] scaramucche = FindObjectsOfType<Scaramucca>();

                foreach (Scaramucca scaramucca in scaramucche)
                {
                    
                    scaramucca.attivo = true;
                }

               Canubi[] AllCanubi = FindObjectsOfType<Canubi>();

                foreach (Canubi canubi in AllCanubi)
                {
                    canubi.attivo = true;
                }

                turnoNemici = 1;
                turno = 1;
            }*/

            //Scrollbar nuovaVita = vita.GetComponent<Scrollbar>();
            // nuovaVita.size = barraVita;




        }

        public void PassTurn()
        {
            //Ciclare sulla lista e trovare il primo personaggio che ha il bool itsMyTurn a false e metterlo a true e il precedente che era a true va rimesso a false.
            //Se tutti sono false il primo va a true
            for (int i = 0; i < charactersList.Count; i++)
            {

                if (charactersList[i].gameObject.GetComponent<TurnHandler>().itsMyTurn)
                {
                    Debug.Log("Ha passato il turno: " + charactersList[i].gameObject.name);

                    charactersList[i].gameObject.GetComponent<TurnHandler>().itsMyTurn = false;
                    if(i + 1 < charactersList.Count) { 
                        charactersList[i + 1].gameObject.GetComponent<TurnHandler>().itsMyTurn = true;
                        Debug.Log("E' il turno di: " + charactersList[i + 1].gameObject.name);
                    } else
                    {
                        charactersList[0].gameObject.GetComponent<TurnHandler>().itsMyTurn = true;
                        Debug.Log("E' il turno di: " + charactersList[0].gameObject.name);

                    }
                    break;

                }
            }

        }

    }
}
