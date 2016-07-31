﻿using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Porta_Mausoleo : MonoBehaviour
    {

        public int xPosition;
        public int yPosition;
        AudioSource portaAudio;
        SpriteRenderer porta;
        Grid elementi;
        bool abilitata;

        void Start()
        {
            porta = GetComponent<SpriteRenderer>();
            portaAudio = GetComponent<AudioSource>();
            xPosition = (int)this.transform.position.x;
            yPosition = (int)this.transform.position.y;
            elementi = FindObjectOfType<Grid>();
            elementi.scacchiera[xPosition, yPosition].status = 3;
        }

        void Update ()
        {

            if (Player.Self.transform.position == elementi.scacchiera[xPosition,yPosition - 1].transform.position && Player.Self.keyScarabeo == 3 && abilitata == false)
            {
                abilitata = true;
                StartCoroutine(OpenTheDoor());
            }
            else if (Player.Self.transform.position == elementi.scacchiera[xPosition, yPosition + 1].transform.position && Player.Self.keyScarabeo == 3 && abilitata == false)
            {
                abilitata = true;
                StartCoroutine(OpenTheDoor());
            }
            else if (Player.Self.transform.position == elementi.scacchiera[xPosition - 1, yPosition].transform.position && Player.Self.keyScarabeo == 3 && abilitata == false)
            {
                abilitata = true;
                StartCoroutine(OpenTheDoor());
            }
            else if (Player.Self.transform.position == elementi.scacchiera[xPosition + 1, yPosition].transform.position && Player.Self.keyScarabeo == 3 && abilitata == false)
            {
                abilitata = true;
                StartCoroutine(OpenTheDoor());
            }
        }

        IEnumerator OpenTheDoor()
        {

            porta.sprite = Resources.Load("Porta_On", typeof(Sprite)) as Sprite;
            portaAudio.clip = AudioContainer.Self.SFX_Temple_Door;
            portaAudio.Play();
            yield return new WaitForSeconds(1);
            elementi.scacchiera[xPosition, yPosition].status = 0;
            Destroy(this.gameObject);
        }
    }
}