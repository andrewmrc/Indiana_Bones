using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace IndianaBones
{

    public class DestroyReference : MonoBehaviour
    {
		GameObject player;
		GameObject canvas;

		void Awake (){
			player = GameObject.FindGameObjectWithTag ("Player");
			canvas = GameObject.FindGameObjectWithTag ("CanvasUI");
			if (player != null) {
				Destroy (player);
			}
			if (canvas != null) {
				Destroy (canvas);
			}
		}
        
        void Start()
        {

        }

    }
}