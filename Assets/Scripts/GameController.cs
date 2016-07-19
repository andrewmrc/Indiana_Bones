using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace IndianaBones
{

    public class GameController : MonoBehaviour
    {

		// Singleton Implementation
		protected static GameController _self;
		public static GameController Self
		{
			get
			{
				if (_self == null)
					_self = FindObjectOfType(typeof(GameController)) as GameController;
				return _self;
			}
		}


        public int turno = 1;
        public int turnoNemici = 1;
        //public Scrollbar vita;
       // public float barraVita = 1.0f;

        void Awake()
        {
            
        }

        

        void Update()
        {
            Player elementi = FindObjectOfType<Player>();
            
            if (elementi.movimento == 0)
                turno = 0;
            if (turno == 0 && turnoNemici == 1)
            {
                turnoNemici = 0;
                Scaramucca[] scaramucche = FindObjectsOfType<Scaramucca>();

                foreach (Scaramucca scaramucca in scaramucche)
                {
                    scaramucca.attivo = true;
                }

               Canubi[] AllCanubi = FindObjectsOfType<Canubi>();

                foreach (Canubi canubi in AllCanubi)
                {
                    canubi.attivo = true;
                }

                turnoNemici = 1;
            }

           //Scrollbar nuovaVita = vita.GetComponent<Scrollbar>();
          // nuovaVita.size = barraVita;
           



        }
    }
}
