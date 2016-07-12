using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LevelEditor
{

    /*
     * Tests the tile loader.
     */
    public class TileTester : MonoBehaviour
    {
        public void Awake()
        {
            TileLoader tl = FindObjectOfType<TileLoader>();
            List<TileData> allTileDatas = tl.LoadAllTilesInScene("Tile");
            string s = "";
            foreach (TileData td in allTileDatas)
            {
                s += td.go.name + ": " + "(" + td.cell_x + "," + td.cell_y + ")" + "\n";
            }
            Debug.Log(s);
        }
    }
}
