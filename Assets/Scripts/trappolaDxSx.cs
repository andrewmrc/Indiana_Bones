using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class trappolaDxSx : MonoBehaviour
    {
       Transform player;
       int x;
       int y;
        
        void Start()
        {
           

            Player objPlayer = FindObjectOfType<Player>();
            player = objPlayer.gameObject.transform;

            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;

            Grid elementi = FindObjectOfType<Grid>();
            this.transform.position = elementi.scacchiera[x, y].transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            Player objPlayer = FindObjectOfType<Player>();

            Grid elementi = FindObjectOfType<Grid>();
            if (player.transform.position == elementi.scacchiera[x-1,y].transform.position)
            {
                objPlayer.xPosition -= 1;
                objPlayer.xOld -= 1;
                objPlayer.controlloVita();
               player.transform.position = elementi.scacchiera[x - 2, y].transform.position;
                objPlayer.targetTr = elementi.scacchiera[x-2, y].transform;
               
                
                SpriteRenderer muro = elementi.scacchiera[x-1, y].GetComponent<SpriteRenderer>();
                muro.sprite = Resources.Load("buco", typeof(Sprite)) as Sprite;
            }
        }
    }
}
