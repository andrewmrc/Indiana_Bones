using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace IndianaBones
{

    public class Player : Character
    {

        public int life = 5;
        public int proiettili = 5;
        public GameObject dente;
        public int x;
        public int y;
        public int bulletDir = 3;
        public float speed = 2;
        public Transform targetTr;
        private Animator animator;
       public Text numDenti;
        public GameObject child;


        void Awake()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("Walk", false);
        }

        void Start()
        {
            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;

            Grid elementi = FindObjectOfType<Grid>();
            this.transform.position = elementi.scacchiera[xPosition, yPosition].transform.position;
            targetTr = elementi.scacchiera[xPosition, yPosition].transform;





        }

        protected override void SetupStats()
        {
            lifePoints = 5;
        }

       public void controlloVita()
        {
            life -= 1;
            GameController gamec = FindObjectOfType<GameController>();
            gamec.barraVita -= 0.20f;


        }

        

       /* public void Attacco()
        {
            Grid elementi = FindObjectOfType<Grid>();
            if (ManhattanDist() == 1)
                Destroy(elementi.enemy);
        }*/

        public void OldValue()
        {
            Grid elementi = FindObjectOfType<Grid>();
            xOld = (int)this.transform.position.x;
            yOld = (int)this.transform.position.y;
            elementi.scacchiera[xOld, yOld].status = 0;

        }

        public void PickUp()
        {
            Grid elementi = FindObjectOfType<Grid>();
            if (elementi.scacchiera[xPosition, yPosition].status == -1)
                proiettili += 5;
        }

		IEnumerator Camminata(float seconds){
            
            

            yield return new WaitForSeconds(seconds); ;
                animator.SetBool("Walk", false);
                
        
        }

        void Update()
        {
            x = xPosition;
            y = yPosition;

           numDenti.text = (proiettili.ToString());

            Grid elementi = FindObjectOfType<Grid>();

            Vector3 distance = targetTr.position - this.transform.position;
            Vector3 direction = distance.normalized;

            transform.position = transform.position + direction *  speed * Time.deltaTime;

            if (distance.magnitude < 0.20f)
            {
                transform.position = targetTr.position;

            }


            if (Input.GetKeyDown("space"))
                //Attacco();

                elementi.scacchiera[xPosition, yPosition].status = 1;

            if (movimento == 1) //movimento la utilizzeranno solo i nemici per i loro turni
            {
                elementi.scacchiera[xOld, yOld].status = 0;
                
            }

            GameController gamec = FindObjectOfType<GameController>();

            if (Input.GetKeyDown("a"))
                if (gamec.turno == 1)
                    if (proiettili > 0)
                {
                    proiettili -= 1;

                        
                        
                        GameObject nuovoDente = Instantiate(dente);
                    nuovoDente.transform.position = new Vector2(child.transform.position.x,child.transform.position.y);
                gamec.turno = 0;
            }
                

            if (Input.GetKeyDown(KeyCode.RightArrow))
            
                   if(elementi.scacchiera[xPosition+1,yPosition].status < 2)
                    if (gamec.turno == 1)
                    {
                            PickUp();
                            animator.SetBool("Walk", true);
                            StartCoroutine(Camminata(0.5f));
                            bulletDir = 3;
                            OldValue();
                        xPosition += 1;
                        targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                gamec.turno = 0;
                Debug.Log("sono qui");
                    }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                if (xPosition > 0)
                    if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                        if (gamec.turno == 1)
                    {
                            PickUp();
                            animator.SetBool("Walk", true);
                            StartCoroutine(Camminata(0.5f));
                            bulletDir = 4;
                            OldValue();
                        xPosition -= 1;
                        targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                        gamec.turno = 0;

                    }

            if (Input.GetKeyDown(KeyCode.DownArrow))
                if (yPosition > 0)
                    if (elementi.scacchiera[xPosition, yPosition -1].status < 2)
                        if (gamec.turno == 1)
                    {
                            PickUp();
                            animator.SetBool("Walk", true);
                            StartCoroutine(Camminata(0.5f));
                            bulletDir = 2;
                            OldValue();
                        yPosition -= 1;
                        targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                        gamec.turno = 0;

                    }

            if (Input.GetKeyDown(KeyCode.UpArrow))
                if (yPosition < 100)
                    if (elementi.scacchiera[xPosition, yPosition + 1].status < 2)
                        if (gamec.turno == 1)
                    {
                            PickUp();
                            animator.SetBool("Walk", true);
                            StartCoroutine(Camminata(0.5f));
                            bulletDir = 1;
                        OldValue();
                        yPosition += 1;
                        targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                        gamec.turno = 0;
                    }

            

        }
    }
}
