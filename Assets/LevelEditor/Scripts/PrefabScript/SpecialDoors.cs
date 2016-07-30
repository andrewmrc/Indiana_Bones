using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class SpecialDoors : MonoBehaviour
    {
        int x;
        int y;

        Grid elementi;

        void Start()
        {
            elementi = FindObjectOfType<Grid>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            elementi.scacchiera[x, y].status = 15;
            elementi.scacchiera[x, y].name = "SpecialDoor";
        }


        void Update()
        {
            
        }

    }
}
