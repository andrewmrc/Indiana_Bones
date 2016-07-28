using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class BucoNero : MonoBehaviour
    {
       
        public int x;
        public int y;
		Grid myGrid;

        public int DannoBucoNero = 1;

        void Start()
        {

           // Player objPlayer = FindObjectOfType<Player>();
           // player = objPlayer.gameObject.transform;

            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;

            myGrid = FindObjectOfType<Grid>();
            this.transform.position = myGrid.scacchiera[x, y].transform.position;
			myGrid.scacchiera[x, y].status = 1;

        }


        IEnumerator ResetPlayerColor()
        {
            yield return new WaitForSeconds(0.3f);
            Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

        }


        void Update()
        {         
            if (Player.Self.transform.position == myGrid.scacchiera[x, y].transform.position)
            {
                Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                StartCoroutine(ResetPlayerColor());

                Player.Self.currentLife -= DannoBucoNero;

                Player.Self.transform.position = myGrid.scacchiera[Player.Self.xOld, Player.Self.yOld].transform.position;

                Player.Self.targetTr = myGrid.scacchiera[Player.Self.xOld, Player.Self.yOld].transform;

				Player.Self.SetPlayerCellStatus ();

				myGrid.scacchiera[x,y].status = 1;
				Player.Self.xPosition = Player.Self.xOld;
				Player.Self.yPosition = Player.Self.yOld;

            }
        }


    }
}
