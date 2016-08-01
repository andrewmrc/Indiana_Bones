using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using IndianaBones;

public class OpenDoors : MonoBehaviour {

	public GameObject doorsToOpen;
	public Sprite buttonsActiveSprite;
	bool isActive;

	int xPosition;
	int yPosition;

    bool audioB = true;

	private Grid elementi;

	public bool door_Ankh;
	public bool door_Eye;
	public bool door_Bird;

    AudioSource audioPulsante;

	// Use this for initialization
	void Start () {
		elementi = FindObjectOfType<Grid>();

		xPosition = (int)this.transform.position.x;
		yPosition = (int)this.transform.position.y;

		this.transform.position =  elementi.scacchiera[xPosition, yPosition].transform.position;
		//Facciamo cercare la porta giusta
		if (door_Ankh) {
			doorsToOpen = GameObject.FindGameObjectWithTag ("Door_Ankh");
		} else if (door_Eye) {
			doorsToOpen = GameObject.FindGameObjectWithTag ("Door_Eye");
		} else if (door_Bird) {
			doorsToOpen = GameObject.FindGameObjectWithTag ("Door_Bird");
		} else {
			Debug.Log ("Specificare che tipo di porta si vuole aprire");
		}

        audioPulsante = GetComponent<AudioSource>();
	}


    IEnumerator Pulsante()
    {

        yield return new WaitForSeconds(0.1f);
        if (audioB == true)
        {
            audioPulsante.clip = AudioContainer.Self.SFX_Pulsante_Porte;
            audioPulsante.Play();
        }

        audioB = false;


    }

    // Update is called once per frame
    void Update () {
		if (Player.Self.transform.position == elementi.scacchiera[xPosition, yPosition].transform.position && isActive == false)
		{
			isActive = true;
			if (doorsToOpen != null) {
				//doors
				elementi.scacchiera[(int)doorsToOpen.transform.position.x, (int)doorsToOpen.transform.position.y].status = 0;
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = buttonsActiveSprite;
                StartCoroutine(Pulsante());
				Destroy(doorsToOpen);
			} else {
				Debug.Log ("Porta da disattivare non inserita, prenderla dalla scena e trascinarla nell'apposito campo nell'Inspector");
			}
		}
	}

	/*
	public void OnTriggerEnter2D(Collider2D coll)
	{

		//Se il player entra sul pulsante la porta associata si spegne
		if (coll.gameObject.tag == "Player") {
			if (doorsToOpen != null) {
				doorsToOpen.SetActive (false);
			} else {
				Debug.Log ("Porta da disattivare non inserita, prenderla dalla scena e trascinarla nell'apposito campo nell'Inspector");
			}
			this.gameObject.GetComponent<Image> ().sprite = buttonsActiveSprite;
		}

	}*/
}
