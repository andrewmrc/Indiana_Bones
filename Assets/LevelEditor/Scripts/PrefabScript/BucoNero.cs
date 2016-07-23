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

        // Update is called once per frame
        void Update()
        {
            

            Grid elementi = FindObjectOfType<Grid>();
            if (Player.Self.transform.position == elementi.scacchiera[x, y].transform.position)
            {
                //objPlayer.xPosition -= 1;
                //objPlayer.xOld -= 1;
                //inserito un valore di default per il danno trappola da rivedere se si vuole 
                //avere un aumento del danno per livello come nel caso dei nemici
                // objPlayer.controlloVita(1);
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
