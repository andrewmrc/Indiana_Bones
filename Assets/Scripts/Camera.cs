using UnityEngine;
using System.Collections;

namespace IndianaBones
{
    public class Camera : MonoBehaviour
    {


        public Transform target;

       

        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
           if (target)
              transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime) + new Vector3(0,0,-10);
            
        }
    }
}

