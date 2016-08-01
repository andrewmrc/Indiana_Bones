using UnityEngine;
using System.Collections;

public class AudioContainer : MonoBehaviour {

    // Singleton Implementation
    protected static AudioContainer _self;
    public static AudioContainer Self

    {
        get
        {
            if (_self == null)
                _self = FindObjectOfType(typeof(AudioContainer)) as AudioContainer;
            return _self;
        }
    }

    

    public AudioClip SFX_RotturaVaso;
    public AudioClip SFX_Canubi_Attack;
    public AudioClip SFX_Canubi_Death;
    public AudioClip SFX_Cat_Attack;
    public AudioClip SFX_Cat_Death;
    public AudioClip SFX_Camel_Attack;
    public AudioClip SFX_Camel_Death;
    public AudioClip SFX_Scaramucca_Attack;
    public AudioClip SFX_Scaramucca_Death;
    public AudioClip SFX_Mummy_Attack;
    public AudioClip SFX_Mummy_Death;
    public AudioClip SFX_AngryRa_Attack;
    public AudioClip SFX_AngryRa_Death;
    public AudioClip SFX_Faraona_Attack;
    public AudioClip SFX_Faraona_Death;

    public AudioClip SFX_Temple_Door;
    public AudioClip SFX_Muro_Distruttibile;
    public AudioClip SFX_Pulsante_Porte;

    public AudioClip SFX_Consumabile;





    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
