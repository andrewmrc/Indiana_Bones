using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class NemicoColpitoDaArmaFinale : MonoBehaviour
    {


        void Start()
        {

        }

        public void OnTriggerEnter2D(Collider2D coll)
        {

            //Da inserire all'interno dell'OnTriggerEnter2D di tutti i nemici
            if (coll.gameObject.name == "armaFinale(Clone)")
            {
                
                   // vita = 0;
            
            }
            


        }

        void Update()
        {

        }
    }
}
