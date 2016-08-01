using UnityEngine;
using System.Collections;

namespace IndianaBones
{
    public class CameraFollow : MonoBehaviour
    {


        public Transform target;
		//public Transform startPoint;

		void Awake () {
			target = Player.Self.transform;
		}

        void Start()
        {
			//Se l'index di questa scena è maggiore di quello della scena precedente carica l'end point altrimenti lo start point
			if (Player.Self.fromLevelInf) {
				transform.position = new Vector3 (GameController.Self.startPoint.transform.position.x, GameController.Self.startPoint.transform.position.y);
			} else if (Player.Self.fromLevelSup) {
				transform.position = new Vector3 (GameController.Self.endPoint.transform.position.x, GameController.Self.endPoint.transform.position.y);
			} else {
				transform.position = new Vector3 (Player.Self.transform.position.x, Player.Self.transform.position.y);
			}
        }

        // Update is called once per frame
        void Update()
        {
           if (target)
              transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime) + new Vector3(0,0,-10);
            
        }
    }
}

