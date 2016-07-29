using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class ItemDente : MonoBehaviour
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

			targetTr = Player.Self.transform;
            Vector3 distance = targetTr.position - this.transform.position;

            if (distance.magnitude < 0.10f)
            {
                Destroy(this.gameObject);
                transform.position = targetTr.position;
				int valueDenti = Random.Range (1, 3);
				Player.Self.proiettili += valueDenti;

            }

        }
    }
}
