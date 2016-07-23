using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class trappolaSxDx : MonoBehaviour
    {
       
       int x;
       int y;
       private Grid elementi;

        public int DannoTrappola = 1;


        void Start()
        {
            

            

            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;

            elementi = FindObjectOfType<Grid>();
            this.transform.position =  elementi.scacchiera[x, y].transform.position;
        }

        // Update is called once per frame

        IEnumerator ResetPlayerColor()
        {
            yield return new WaitForSeconds(0.3f);
            Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            
        }

        void Update()
        {
            

            
            if (Player.Self.transform.position == elementi.scacchiera[x+1,y].transform.position)
            {
                

                Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                StartCoroutine(ResetPlayerColor());

                Player.Self.currentLife -= DannoTrappola;

                Player.Self.xPosition += 1;
                Player.Self.xOld += 1;
                //inserito un valore di default per il danno trappola da rivedere se si vuole 
                //avere un aumento del danno per livello come nel caso dei nemici
                
                Player.Self.transform.position = elementi.scacchiera[x + 2, y].transform.position;
                elementi.scacchiera[Player.Self.xPosition, Player.Self.yPosition].status = 4;
                elementi.scacchiera[Player.Self.xPosition - 1, Player.Self.yPosition].status = 2;
                Player.Self.targetTr = elementi.scacchiera[x+2, y].transform;

                

                SpriteRenderer muro = elementi.scacchiera[x+1, y].GetComponent<SpriteRenderer>();
                muro.sprite = Resources.Load("buco", typeof(Sprite)) as Sprite;
                
            }
        }
    }
}
