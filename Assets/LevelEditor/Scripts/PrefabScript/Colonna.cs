﻿using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Colonna : MonoBehaviour
    {
        int x;
        int y;

        void Start()
        {
            Grid elementi = FindObjectOfType<Grid>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            elementi.scacchiera[x, y].status = 2;
			elementi.scacchiera[x, y].gameObject.tag = "Colonne";
            elementi.scacchiera[x, y].gameObject.AddComponent<BoxCollider2D>();


        }

    }
}
