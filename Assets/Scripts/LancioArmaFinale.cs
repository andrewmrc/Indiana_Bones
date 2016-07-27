using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class LancioArmaFinale : MonoBehaviour
    {

        public GameObject armaFinale;

        void Start()
        {

        }

        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                GameObject nuovaArma;
                nuovaArma = Instantiate(armaFinale);
                nuovaArma.transform.position = Player.Self.transform.position;
            }
        }
    }
}
