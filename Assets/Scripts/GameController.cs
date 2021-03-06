﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

		public float delayTurni = 0.2f;
        //public int turno = 1;
        //public int turnoNemici = 1;
        //public Scrollbar vita;
        // public float barraVita = 1.0f;
        public List<GameObject> charactersList = new List<GameObject>();
		public GameObject startPoint;
		public GameObject endPoint;
		float backupDelay;
		public bool inPause;
		public GameObject canvasToOpen;



		void Awake(){
			//startPoint = GameObject.FindGameObjectWithTag ("StartPoint");
		}

        void Start()
        {
            charactersList.Add(Player.Self.gameObject);
			/*if (startPoint != null) {
				Player.Self.transform.position = startPoint.transform.position;
			}*/
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
			backupDelay = delayTurni;
        }



        void Update()
        {
			//Velocizza i turni quando in scena c'è solo il Player
			if (charactersList.Count == 1) {
				delayTurni = 0;
			} else {
				delayTurni = backupDelay;
			}

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
					StartCoroutine (WaitToNextTurn (i));

                    
                    break;

                }
            }

        }


		IEnumerator WaitToNextTurn (int i) {
			yield return new WaitForSeconds (delayTurni);
			Debug.Log ("WaitToNextTurn");
			if(i + 1 < charactersList.Count) { 
				charactersList[i + 1].gameObject.GetComponent<TurnHandler>().itsMyTurn = true;
				Debug.Log("E' il turno di: " + charactersList[i + 1].gameObject.name);
			} else
			{
				charactersList[0].gameObject.GetComponent<TurnHandler>().itsMyTurn = true;
				Debug.Log("E' il turno di: " + charactersList[0].gameObject.name);

			}
		}


		public void StartEndGameCoroutine (){
			//ElementsReference.Self.canvasUI.SetActive (false);
			//Player.Self.gameObject.SetActive (false);
			StartCoroutine(StartCreditsScene());
		}

		IEnumerator StartCreditsScene(){
			Debug.Log ("Credits");
			yield return new WaitForSeconds (0.5f);

			canvasToOpen.SetActive (true);

			yield return new WaitForSeconds (3f);
			SceneManager.LoadScene ("Credits");
		}


    }
}
