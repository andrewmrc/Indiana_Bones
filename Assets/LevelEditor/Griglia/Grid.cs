using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Grid : MonoBehaviour
    {
		
		

		public int n = 100;
        public int m = 100;
       
        public GameObject cell;

        float offset_x = 1;
        float offset_y = 1;

        public Cella[,] scacchiera; 

        void Awake()
        {
            scacchiera = new Cella[n, m];

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m; j++)
                {
                    GameObject nuovaCella = Instantiate(cell);
                    nuovaCella.transform.position = new Vector3(i + 0.5f, j + 0.5f, 0);
                    scacchiera[i, j] = nuovaCella.GetComponent<Cella>();
                    nuovaCella.name = "Cell_Empty " + i + " " + j;
                    nuovaCella.transform.parent = this.gameObject.transform;
                }
            }

        }


		void Start () {

		}
        

       
        void Update()
        {

        }
    }
}
