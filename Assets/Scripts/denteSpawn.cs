using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class denteSpawn : MonoBehaviour
    {

        public Transform targetTr;
        public float speed = 1;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {




            /*
            

            
            Vector3 direction = distance.normalized;

            transform.position = transform.position + direction * 2 * speed * Time.deltaTime;*/

			targetTr = Player.Self.transform;
            Vector3 distance = targetTr.position - this.transform.position;

            if (distance.magnitude < 0.10f)
            {
                Destroy(this.gameObject);
                transform.position = targetTr.position;
				Player.Self.proiettili += 1;

            }

        }
    }
}
