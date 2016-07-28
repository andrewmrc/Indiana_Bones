using UnityEngine;
using System.Collections;

namespace IndianaBones
{
    public class CameraFollow : MonoBehaviour
    {


        public Transform target;
		public Transform startPoint;

		void Awake () {
			target = Player.Self.transform;
			startPoint = GameObject.Find ("StartPoint").transform;
		}

        void Start()
        {
			//Se l'index di questa scena è maggiore di quello della scena precedente carica l'end point altrimenti lo start point
			transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y);
        }

        // Update is called once per frame
        void Update()
        {
           if (target)
              transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime) + new Vector3(0,0,-10);
            
        }
    }
}

