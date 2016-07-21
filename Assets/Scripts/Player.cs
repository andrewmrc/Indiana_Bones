﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace IndianaBones
{

    public class Player : Character
    {



        bool onMove = true;
        bool onOff = false;
        bool crossActive = false;
        bool attacco = false;
        public bool endMove;

        public int croce = 1;

        public GameObject up;
        public GameObject down;
        public GameObject right;
        public GameObject left;

        // Singleton Implementation
        protected static Player _self;
        public static Player Self

        {
            get
            {
                if (_self == null)
                    _self = FindObjectOfType(typeof(Player)) as Player;
                return _self;
            }
        }


        public int proiettili = 5;
        public GameObject dente;
        public int x;
        public int y;
        public int bulletDir = 3;
        public float speed = 2;
        public Transform targetTr;
        private Animator animator;
        public Text numDenti;
        public Text valoreVita;
        public GameObject child;
        private Grid elementi;

        [Header("Level and Stats")]
        [Space(10)]
        public int playerLevel = 1;
        public int currentLife;
        public int currentAttack;
        public int currentMana;

        public float expToLevelUp;
        public int expCollected;
        public int startingLife;
        public int startingAttack;
        public int startingMana;

        //public List<PlayerLevels> levelsList = new List<PlayerLevels> ();

        public static Player Instance;

        void Awake()
        {
            //Metodo per passare l'oggetto da una scena all'altra
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            animator = GetComponent<Animator>();
            animator.SetBool("Walk", false);

            if (playerLevel == 1)
            {
                //Set the starting stats of the player
                currentLife = startingLife;
                currentAttack = startingAttack;
                currentMana = startingMana;
            }
        }


        void Start()
        {
            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            elementi = FindObjectOfType<Grid>();
            this.transform.position = elementi.scacchiera[xPosition, yPosition].transform.position;
            targetTr = elementi.scacchiera[xPosition, yPosition].transform;

        }


        public void controlloVita(int attack)
        {
            currentLife -= attack;
            // GameController gamec = FindObjectOfType<GameController>();
            // gamec.barraVita -= 0.20f;


        }


        public void AttaccoADistanza()
        {
            if (croce == 1)
            {
                if (proiettili > 0)
                {
                    proiettili -= 1;

                    bulletDir = 1;

                    GameObject nuovoDente = Instantiate(dente);
                    nuovoDente.transform.position = up.transform.position;

                }

            }

            if (croce == 2)
            {
                if (proiettili > 0)
                {
                    proiettili -= 1;

                    bulletDir = 3;

                    GameObject nuovoDente = Instantiate(dente);
                    nuovoDente.transform.position = right.transform.position;

                }

            }

            if (croce == 3)
            {
                if (proiettili > 0)
                {
                    proiettili -= 1;

                    bulletDir = 2;

                    GameObject nuovoDente = Instantiate(dente);
                    nuovoDente.transform.position = down.transform.position;

                }

            }

            if (croce == 4)
            {
                if (proiettili > 0)
                {
                    proiettili -= 1;

                    bulletDir = 4;

                    GameObject nuovoDente = Instantiate(dente);
                    nuovoDente.transform.position = left.transform.position;

                }

            }


        }


        public void Attacco()
        {
            if (croce == 1)
            {
                up.GetComponent<BoxCollider2D>().enabled = true;

            }

            if (croce == 2)
            {
                right.GetComponent<BoxCollider2D>().enabled = true;

            }

            if (croce == 3)
            {
                down.GetComponent<BoxCollider2D>().enabled = true;

            }

            if (croce == 4)
            {
                left.GetComponent<BoxCollider2D>().enabled = true;

            }

            StartCoroutine(PostAttacco(0.2f));
        }


        IEnumerator PostAttacco(float seconds)
        {

            yield return new WaitForSeconds(seconds);

            //Set false to the attack parameter of animation
            animator.SetBool("Attack", false);

            up.GetComponent<BoxCollider2D>().enabled = false;
            right.GetComponent<BoxCollider2D>().enabled = false;
            down.GetComponent<BoxCollider2D>().enabled = false;
            left.GetComponent<BoxCollider2D>().enabled = false;
        }


        public void OldValue()
        {
            //Grid elementi = FindObjectOfType<Grid>();
            xOld = (int)this.transform.position.x;
            yOld = (int)this.transform.position.y;
            elementi.scacchiera[xOld, yOld].status = 0;

        }

        public void PickUp()
        {
            //Grid elementi = FindObjectOfType<Grid>();
            if (elementi.scacchiera[xPosition, yPosition].status == -1)
                proiettili += 5;
        }

        IEnumerator Camminata(float seconds)
        {



            yield return new WaitForSeconds(seconds); ;
            animator.SetBool("Walk", false);


        }


        public void LevelUp()
        {
            //Save the previous required exp
            float expToPreviousLevelUp = expToLevelUp;

            //Set the new exp required to level up to the next level
            expToLevelUp = expToPreviousLevelUp * 1.5f;

            //Increase Life each level up
            currentLife++;

            //Increase Attack if the player level is pair
            if (playerLevel % 2 == 0)
            {
                currentAttack++;
            }

            //Increase Mana if the player level is odd
            if (playerLevel % 2 != 0)
            {
                currentMana = currentMana + 3;
            }

        }


        public void RotazioneAttacco()
        {
            if (croce == 1)
            {
                RicoloraRosso();
                SpriteRenderer colore = up.GetComponent<SpriteRenderer>();
                colore.sprite = Resources.Load("green", typeof(Sprite)) as Sprite;
            }

            if (croce == 2)
            {
                RicoloraRosso();
                SpriteRenderer colore = right.GetComponent<SpriteRenderer>();
                colore.sprite = Resources.Load("green", typeof(Sprite)) as Sprite;
            }

            if (croce == 3)
            {
                RicoloraRosso();
                SpriteRenderer colore = down.GetComponent<SpriteRenderer>();
                colore.sprite = Resources.Load("green", typeof(Sprite)) as Sprite;
            }

            if (croce == 4)
            {
                RicoloraRosso();
                SpriteRenderer colore = left.GetComponent<SpriteRenderer>();
                colore.sprite = Resources.Load("green", typeof(Sprite)) as Sprite;
            }

        }



        public void RicoloraRosso()
        {

            SpriteRenderer colore = up.GetComponent<SpriteRenderer>();
            colore.sprite = Resources.Load("red", typeof(Sprite)) as Sprite;


            SpriteRenderer colore1 = right.GetComponent<SpriteRenderer>();
            colore1.sprite = Resources.Load("red", typeof(Sprite)) as Sprite;


            SpriteRenderer colore2 = down.GetComponent<SpriteRenderer>();
            colore2.sprite = Resources.Load("red", typeof(Sprite)) as Sprite;


            SpriteRenderer colore3 = left.GetComponent<SpriteRenderer>();
            colore3.sprite = Resources.Load("red", typeof(Sprite)) as Sprite;


        }

        void Update()
        {
            if (gameObject.GetComponent<TurnHandler>().itsMyTurn)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    croce = 2;
                    RotazioneAttacco();
                    //Flip the sprite of the player
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;

                    if (onMove == true)
                    {
                        if (elementi.scacchiera[xPosition + 1, yPosition].status < 2)
                        {
                            onMove = false;
                            PickUp();
                            animator.SetBool("Walk", true);
                            StartCoroutine(Camminata(0.5f));
                            bulletDir = 3;
                            OldValue();
                            xPosition += 1;
                            targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                            elementi.scacchiera[xPosition, yPosition].status = 4;
                            endMove = true;
                        }
                    }
                }


                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    croce = 4;
                    RotazioneAttacco();
                    //Flip the sprite of the player
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;

                    if (onMove == true)
                    {
                        if (xPosition > 0)
                        {
                            if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                            {
                                onMove = false;
                                PickUp();
                                animator.SetBool("Walk", true);
                                StartCoroutine(Camminata(0.5f));
                                bulletDir = 4;
                                OldValue();
                                xPosition -= 1;
                                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                                elementi.scacchiera[xPosition, yPosition].status = 4;
                                endMove = true;

                            }
                        }
                    }
                }


                if (Input.GetKey(KeyCode.DownArrow))
                {
                    croce = 3;
                    RotazioneAttacco();
                    if (onMove == true)
                    {
                        if (yPosition > 0)
                        {
                            if (elementi.scacchiera[xPosition, yPosition - 1].status < 2)
                            {
                                onMove = false;
                                PickUp();
                                animator.SetBool("Walk", true);
                                StartCoroutine(Camminata(0.5f));
                                bulletDir = 2;
                                OldValue();
                                yPosition -= 1;
                                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                                elementi.scacchiera[xPosition, yPosition].status = 4;
                                endMove = true;

                            }
                        }
                    }
                }


                if (Input.GetKey(KeyCode.UpArrow))
                {
                    croce = 1;
                    RotazioneAttacco();
                    if (onMove == true)
                    {
                        if (yPosition < 100)
                        {
                            if (elementi.scacchiera[xPosition, yPosition + 1].status < 2)
                            {
                                onMove = false;
                                PickUp();
                                animator.SetBool("Walk", true);
                                StartCoroutine(Camminata(0.5f));
                                bulletDir = 1;
                                OldValue();
                                yPosition += 1;
                                targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                                elementi.scacchiera[xPosition, yPosition].status = 4;
                                endMove = true;
                            }
                        }
                    }
                }



                if (onOff == true)
                {
                    up.gameObject.SetActive(true);
                    down.gameObject.SetActive(true);
                    right.gameObject.SetActive(true);
                    left.gameObject.SetActive(true);
                    RotazioneAttacco();

                }
                else if (onOff == false)
                {
                    up.gameObject.SetActive(false);
                    down.gameObject.SetActive(false);
                    right.gameObject.SetActive(false);
                    left.gameObject.SetActive(false);

                }



                //Check if the player have reached the required exp to level up
                if (expCollected >= expToLevelUp)
                {
                    playerLevel++;
                    LevelUp();
                }

                x = xPosition;
                y = yPosition;

                numDenti.text = (proiettili.ToString());
                valoreVita.text = (currentLife.ToString());

                //Grid elementi = FindObjectOfType<Grid>();

                Vector3 distance = targetTr.position - this.transform.position;
                Vector3 direction = distance.normalized;

                transform.position = transform.position + direction * speed * Time.deltaTime;


                if (endMove)
                {
                    Debug.Log("Finito turno1");

                    if (distance.magnitude < 0.20f && crossActive == false)
                    {
                        Debug.Log("Finito turno2");

                        transform.position = targetTr.position;
                        onMove = true;
                        if (transform.position == targetTr.position)
                        {
                            endMove = false;
                            GameController.Self.PassTurn();
                            Debug.Log("Finito turno3");
                        }
                        
                    }

                }


                if (Input.GetKeyDown("space"))
                {
                    //Set true to the attack parameter of animation
                    animator.SetBool("Attack", true);
                    Attacco();
                }


                if (movimento == 1) //movimento la utilizzeranno solo i nemici per i loro turni
                {
                    elementi.scacchiera[xOld, yOld].status = 0;

                }

                GameController gamec = FindObjectOfType<GameController>();

                /* Transform figlio = transform.FindChild("lancio");

                    if (Input.GetKeyDown(KeyCode.Keypad8))
                    {
                        figlio.transform.localEulerAngles = new Vector3(0, 0, 90);
                        //figlio.transform.Translate(0, 90, 0);
                        Debug.Log("sono qui");
                    }*/

                if (Input.GetKeyDown(KeyCode.Z) && onOff == false && endMove == false)
                {
                    onOff = true;
                    onMove = false;
                    crossActive = true;
                }
                else if (Input.GetKeyDown(KeyCode.Z) && onOff == true)
                {
                    onOff = false;
                    onMove = true;
                    crossActive = false;
                }

                if (Input.GetKeyDown(KeyCode.LeftControl))
                    if (onOff == true)
                        AttaccoADistanza();

             
            }

        }


    }
}
