using UnityEngine;
using System.Collections;

namespace IndianaBones
{

    public class Pavimento1 : MonoBehaviour
    {
        int x;
        int y;
        
        void Start()
        {
            Grid elementi = FindObjectOfType<Grid>();
            x = (int)this.transform.position.x;
            y = (int)this.transform.position.y;
            
            SpriteRenderer muro = elementi.scacchiera[x, y].GetComponent<SpriteRenderer>();
            muro.sprite = Resources.Load("pavimento1", typeof(Sprite)) as Sprite;
            this.transform.position = new Vector2(x, y);
            
            Destroy(this.gameObject);

        }

    }
}
