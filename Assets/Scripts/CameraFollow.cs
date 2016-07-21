using UnityEngine;
using System.Collections;

namespace IndianaBones
{
    public class CameraFollow : MonoBehaviour
    {


        public Transform target;

       

        void Start()
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y);
        }

        // Update is called once per frame
        void Update()
        {
           if (target)
              transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime) + new Vector3(0,0,-10);
            
        }
    }
}

