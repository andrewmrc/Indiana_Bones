using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace IndianaBones {

    public class ToLevel1 : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

            if (Player.Self.transform.position == this.transform.position)
         
            SceneManager.LoadScene("Livello1");
        

        }
    }
}
