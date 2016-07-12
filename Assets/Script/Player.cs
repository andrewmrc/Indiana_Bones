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


        void Awake()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("Walk", false);
        }

        void Start()
        {
            xPosition = 1;
            yPosition = 1;
           
            
            
            
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
        

        
        public void Attacco()
        {
            Griglia elementi = FindObjectOfType<Griglia>();
            if (elementi.ManhattanDist() == 1)
                Destroy(elementi.lara);
        }

        public void OldValue()
        {
            Griglia elementi = FindObjectOfType<Griglia>();
            xOld = (int)this.transform.position.x;
            yOld = (int)this.transform.position.y;
            elementi.scacchiera[xOld, yOld].status = 0;

        }

        public void PickUp()
        {
            Griglia elementi = FindObjectOfType<Griglia>();
            if (elementi.scacchiera[xPosition, yPosition].status == -1)
                proiettili += 5;
        }

		IEnumerator SayCiaoTenTimes(float seconds){
            
            

            yield return new WaitForSeconds(seconds); ;
                animator.SetBool("Walk", false);
                
        
        }

        void Update()
        {
            x = xPosition;
            y = yPosition;

            numDenti.text = (proiettili.ToString());

            Griglia elementi = FindObjectOfType<Griglia>();

            Vector3 distance = targetTr.position - this.transform.position;
            Vector3 direction = distance.normalized;

            transform.position = transform.position + direction * 2 * speed * Time.deltaTime;

            if (distance.magnitude < 0.10f)
            {
                transform.position = targetTr.position;

            }


            if (Input.GetKeyDown("space"))
                Attacco();
            elementi.scacchiera[xPosition, yPosition].status = 1;

            if (movimento == 1)
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
                    nuovoDente.transform.position = new Vector2(xPosition, yPosition);
                gamec.turno = 0;
            }
                

            if (Input.GetKeyDown("right"))
                if (xPosition < 29)
                   if(elementi.scacchiera[xPosition+1,yPosition].status < 2)
                    if (gamec.turno == 1)
                    {
                            PickUp();
                            animator.SetBool("Walk", true);
                            StartCoroutine(SayCiaoTenTimes(0.5f));
                            bulletDir = 3;
                            OldValue();
                        xPosition += 1;
                        targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                        gamec.turno = 0;

                    }

            if (Input.GetKeyDown("left"))
                if (xPosition > 0)
                    if (elementi.scacchiera[xPosition - 1, yPosition].status < 2)
                        if (gamec.turno == 1)
                    {
                            PickUp();
                            animator.SetBool("Walk", true);
                            StartCoroutine(SayCiaoTenTimes(0.5f));
                            bulletDir = 4;
                            OldValue();
                        xPosition -= 1;
                        targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                        gamec.turno = 0;

                    }

            if (Input.GetKeyDown("down"))
                if (yPosition > 0)
                    if (elementi.scacchiera[xPosition, yPosition -1].status < 2)
                        if (gamec.turno == 1)
                    {
                            PickUp();
                            animator.SetBool("Walk", true);
                            StartCoroutine(SayCiaoTenTimes(0.5f));
                            bulletDir = 2;
                            OldValue();
                        yPosition -= 1;
                        targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                        gamec.turno = 0;

                    }

            if (Input.GetKeyDown("up"))
                if (yPosition < 9)
                    if (elementi.scacchiera[xPosition, yPosition + 1].status < 2)
                        if (gamec.turno == 1)
                    {
                            PickUp();
                            animator.SetBool("Walk", true);
                            StartCoroutine(SayCiaoTenTimes(0.5f));
                            bulletDir = 1;
                        OldValue();
                        yPosition += 1;
                        targetTr = elementi.scacchiera[xPosition, yPosition].transform;
                        gamec.turno = 0;
                    }

            

        }
    }
}
