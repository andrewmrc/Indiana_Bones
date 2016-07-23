using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class BucoNero : MonoBehaviour
    {
       
        int x;
        int y;

        public int DannoBucoNero = 1;

        void Start()
        {


           // Player objPlayer = FindObjectOfType<Player>();
           // player = objPlayer.gameObject.transform;

            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;

            Grid elementi = FindObjectOfType<Grid>();
            this.transform.position = elementi.scacchiera[x, y].transform.position;
        }


        IEnumerator ResetPlayerColor()
        {
            yield return new WaitForSeconds(0.3f);
            Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

        }


        void Update()
        {
            

            Grid elementi = FindObjectOfType<Grid>();
            if (Player.Self.transform.position == elementi.scacchiera[x, y].transform.position)
            {
                Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                StartCoroutine(ResetPlayerColor());

                Player.Self.currentLife -= DannoBucoNero;

                Player.Self.transform.position = elementi.scacchiera[Player.Self.xOld, Player.Self.yOld].transform.position;

                Player.Self.targetTr = elementi.scacchiera[Player.Self.xOld, Player.Self.yOld].transform;

                Player.Self.xPosition = Player.Self.xOld;
                Player.Self.yPosition = Player.Self.yOld;

               


                


              //  SpriteRenderer muro = elementi.scacchiera[x - 1, y].GetComponent<SpriteRenderer>();
              //  muro.sprite = Resources.Load("buco", typeof(Sprite)) as Sprite;
            }
        }

    }
}
