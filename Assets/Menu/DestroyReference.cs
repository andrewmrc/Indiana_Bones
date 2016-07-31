using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace IndianaBones
{

    public class DestroyReference : MonoBehaviour
    {

        
        void Start()
        {
            Destroy(Player.Self.gameObject);
            CanvasUI elementi = FindObjectOfType<CanvasUI>();
            Destroy(elementi.gameObject);

        }

    }
}