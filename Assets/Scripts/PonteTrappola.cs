using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class PonteTrappola : MonoBehaviour
    {
       Transform player;
       int x;
       int y;
       private Grid elementi;

       public int DannoPonte = 1;

        void Start()
        {
           

          

            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;

            elementi = FindObjectOfType<Grid>();
            this.transform.position = elementi.scacchiera[x, y].transform.position;
        }

        IEnumerator ResetPlayerColor()
        {
            yield return new WaitForSeconds(0.3f);
            Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            
        }

        // Update is called once per frame
        void Update()
        {
            

            
            if (Player.Self.transform.position == elementi.scacchiera[x,y].transform.position)
            {
                Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                StartCoroutine(ResetPlayerColor());
                Player.Self.currentLife -= DannoPonte;
                Player.Self.xPosition = Player.Self.xOld;
                Player.Self.yPosition = Player.Self.yOld;
                //inserito un valore di default per il danno trappola da rivedere se si vuole 
                //avere un aumento del danno per livello come nel caso dei nemici

                Player.Self.transform.position = elementi.scacchiera[Player.Self.xPosition, Player.Self.yPosition].transform.position;
                elementi.scacchiera[Player.Self.xPosition, Player.Self.yPosition].status = 4;
                elementi.scacchiera[x, y].status = 2;
                Player.Self.targetTr = elementi.scacchiera[Player.Self.xPosition, Player.Self.yPosition].transform;

                


                SpriteRenderer voragine = GetComponent<SpriteRenderer>();
                voragine.sprite = Resources.Load("nera", typeof(Sprite)) as Sprite;
            }
        }
    }
}
