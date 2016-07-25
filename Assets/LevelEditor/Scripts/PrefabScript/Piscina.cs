using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Piscina : MonoBehaviour
    {
       
        int x;
        int y;
		Grid myGrid;

        public int DannoPiscina = 1;
        bool attiva = true;
        void Start()
        {

            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;

            myGrid = FindObjectOfType<Grid>();
            this.transform.position = myGrid.scacchiera[x, y].transform.position;
        }


        IEnumerator ResetPlayerColor()
        {
            yield return new WaitForSeconds(0.3f);
            Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

        }


        void Update()
        {
            
            
            if (attiva == true && Player.Self.transform.position == myGrid.scacchiera[x, y].transform.position)
            {
                attiva = false;

                Player.Self.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                StartCoroutine(ResetPlayerColor());

                Player.Self.currentLife -= DannoPiscina;


            }

            else if (Player.Self.transform.position != myGrid.scacchiera[x, y].transform.position)
                attiva = true;
        }

    }
}
