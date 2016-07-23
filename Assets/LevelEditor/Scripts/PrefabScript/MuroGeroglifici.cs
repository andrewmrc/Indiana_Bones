﻿using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class MuroGeroglifici : MonoBehaviour
    {
        int x;
        int y;

        void Start()
        {
            Grid elementi = FindObjectOfType<Grid>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            elementi.scacchiera[x, y].status = 2;
            elementi.scacchiera[x, y].name = "muro";


            


        }

    }
}