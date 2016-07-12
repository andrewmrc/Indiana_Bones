using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class EditorGriglia : MonoBehaviour
    {

        

        int n = 200;
        int m = 200;
       



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
                    nuovaCella.name = "cella " + i + " " + j;

                }
            }

            

        }

        

   

       

       

 
        


        void Update()
        {

        }
    }
}
