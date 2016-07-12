using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class objectCollision : MonoBehaviour
    {

        public GameObject laraCroft;
        public GameObject denteSpawn;

        public void OnCollisionEnter2D(Collision2D coll)
        {



            if (coll.gameObject.name == "denteSpawn")
            {


                

                GameObject newDente = Instantiate(denteSpawn);
                newDente.transform.position = this.transform.position;
                

               
                Destroy(coll.gameObject);
               


            }

            



        }
    }
}
