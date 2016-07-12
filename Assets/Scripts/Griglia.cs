using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Griglia : MonoBehaviour
    {

        public GameObject player;
        public GameObject lara;
        public GameObject colonna;

        int n = 30;
        int m = 10;
       



        public GameObject cell;

        float offset_x = 1;
        float offset_y = 1;

        public Cella[,] scacchiera = new Cella[30, 10];
        void Awake()
        {

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m; j++)
                {


                    GameObject nuovaCella = Instantiate(cell);
                    nuovaCella.transform.position = new Vector3(i , j , 0);
                    scacchiera[i, j] = nuovaCella.GetComponent<Cella>();
                    nuovaCella.name = "cella " + i + " " + j;

                }
            }

            player.transform.position = scacchiera[1, 1].transform.position;
            lara.transform.position = scacchiera[8, 8].transform.position;
            colonna.transform.position = scacchiera[5,5].transform.position;
            scacchiera[5, 5].status = 2;
            Lara eleLara = FindObjectOfType<Lara>();
            eleLara.targetTr = scacchiera[8, 8].transform;
            Player  playerEle = FindObjectOfType<Player>();
            playerEle.targetTr = scacchiera[1, 1].transform;
            creazioneMuro();
            creazioneCorridoio();
            creazioneVuoto();

        }

        public int ManhattanDist()
        {
            return (Mathf.Abs((int)player.transform.position.x - (int)lara.transform.position.x) + Mathf.Abs((int)player.transform.position.y - (int)lara.transform.position.y));
        }

        public void MD()
        {
            Debug.Log(Mathf.Abs((int)player.transform.position.x - (int)lara.transform.position.x) + Mathf.Abs((int)player.transform.position.y - (int)lara.transform.position.y));
        }

        public void creazioneMuro()
        {
            for (int j = 0; j < 10; j+=9)
            {
                for (int i = 0; i < 30; i++)
                {
                    SpriteRenderer muro = scacchiera[i, j].GetComponent<SpriteRenderer>();
                    muro.sprite = Resources.Load("muro", typeof(Sprite)) as Sprite;
                    scacchiera[i, j].status = 2;
                    scacchiera[i, j].name = "muro";
                   
                    scacchiera[i, j].gameObject.AddComponent<BoxCollider2D>();
                 
                }
                
            }

            for (int j = 0; j < 30; j += 29)
            {
                for (int i = 0; i < 10; i++)
                {
                    SpriteRenderer muro = scacchiera[j, i].GetComponent<SpriteRenderer>();
                    muro.sprite = Resources.Load("muro", typeof(Sprite)) as Sprite;
                    scacchiera[j, i].status = 2;
                    scacchiera[j, i].name = "muro";
                    scacchiera[j, i].gameObject.AddComponent<BoxCollider2D>();
                }

            }
        }

        public void creazioneCorridoio()
        {
            for (int j = 0; j < 10; j++ )
            {
                for (int i = 10; i < 20; i++)
                {
                    SpriteRenderer muro = scacchiera[i, j].GetComponent<SpriteRenderer>();
                    muro.sprite = Resources.Load("muro", typeof(Sprite)) as Sprite;
                    scacchiera[i, j].name = "muro";
                    scacchiera[i, j].status = 2;
                    scacchiera[i, j].gameObject.AddComponent<BoxCollider2D>();
                }

            }

            for (int j = 4; j < 6; j++)
            {
                for (int i = 10; i < 20; i++)
                {
                    SpriteRenderer muro = scacchiera[i, j].GetComponent<SpriteRenderer>();
                    muro.sprite = Resources.Load("pavimento", typeof(Sprite)) as Sprite;
                    scacchiera[i, j].status = 0;
                    scacchiera[i, j].gameObject.AddComponent<BoxCollider2D>();
                }
            }

           
        }

        public void creazioneVuoto()
        {
            for (int j = 7; j < 10; j++)
            {
                for (int i = 11; i < 19; i++)
                {
                    SpriteRenderer muro = scacchiera[i, j].GetComponent<SpriteRenderer>();
                    muro.sprite = Resources.Load("nera", typeof(Sprite)) as Sprite;
                    scacchiera[i, j].status = 2;
                    
                }

            }

            for (int j = 0; j < 3; j++)
            {
                for (int i = 11; i < 19; i++)
                {
                    SpriteRenderer muro = scacchiera[i, j].GetComponent<SpriteRenderer>();
                    muro.sprite = Resources.Load("nera", typeof(Sprite)) as Sprite;
                    scacchiera[i, j].status = 0;
                    
                }
            }


        }


        void Update()
        {
            if (Input.GetKeyDown("d"))
                MD();

        }
    }
}
