using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Abilita_Recupero : MonoBehaviour
    {

        public int manaCost = 1;
		public int healthQuantity = 1;

        public void TravasoPuntiRecupero()
        {
			Player.Self.currentLife += healthQuantity;
			Player.Self.currentMana -= manaCost;
        }

    }
}