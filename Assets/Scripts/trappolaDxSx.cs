using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class trappolaDxSx : MonoBehaviour
    {
       Transform player;
       int x;
       int y;

        public int DannoTrappola = 1;

        void Start()
        {
           

          

            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;

            Grid elementi = FindObjectOfType<Grid>();
            this.transform.position = elementi.scacchiera[x, y].transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            

            Grid elementi = FindObjectOfType<Grid>();
            if (Player.Self.transform.position == elementi.scacchiera[x-1,y].transform.position)
            {
                Player.Self.currentLife -= DannoTrappola;
                Player.Self.xPosition -= 1;
                Player.Self.xOld -= 1;
                //inserito un valore di default per il danno trappola da rivedere se si vuole 
                //avere un aumento del danno per livello come nel caso dei nemici

                Player.Self.transform.position = elementi.scacchiera[x - 2, y].transform.position;
                Player.Self.targetTr = elementi.scacchiera[x-2, y].transform;
               
                
                SpriteRenderer muro = elementi.scacchiera[x-1, y].GetComponent<SpriteRenderer>();
                muro.sprite = Resources.Load("buco", typeof(Sprite)) as Sprite;
            }
        }
    }
}
