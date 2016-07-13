using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Grid : MonoBehaviour
    {
		
		public GameObject player;
		public GameObject enemy;

		public int n = 200;
        public int m = 200;
       
        public GameObject cell;

        float offset_x = 1;
        float offset_y = 1;

        public Cella[,] scacchiera = new Cella[200, 200];

        void Awake()
        {

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m; j++)
                {
                    GameObject nuovaCella = Instantiate(cell);
                    nuovaCella.transform.position = new Vector3(i , j , 0);
                    scacchiera[i, j] = nuovaCella.GetComponent<Cella>();
                    nuovaCella.name = "Cell_Empty " + i + " " + j;
					nuovaCella.transform.parent = this.gameObject.transform;
                }
            }
				
        }

        
		public int ManhattanDist()
		{
			return (Mathf.Abs((int)player.transform.position.x - (int)enemy.transform.position.x) + Mathf.Abs((int)player.transform.position.y - (int)enemy.transform.position.y));
		}

       
        void Update()
        {

        }
    }
}
