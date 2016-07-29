using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Abilita_Recupero : MonoBehaviour
    {

        public int puntiRecupero = 1;

        public void TravasoPuntiRecupero()
        {
            Player.Self.currentLife += puntiRecupero;
            Player.Self.currentMana -= puntiRecupero;
        }

    }
}