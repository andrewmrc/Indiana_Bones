using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LevelEditor
{

    public struct TileData
    {
        public GameObject go;
        public int cell_x;
        public int cell_y;
    }

    /*
     * Loads all tiles found in the current scene.
     */
    public class TileLoader : MonoBehaviour
    {

        public List<TileData> LoadAllTilesInScene(string tag)
        {
            List<TileData> allTileDatas = new List<TileData>();
            GameObject[] allGos = FindObjectsOfType<GameObject>();
            foreach (GameObject go in allGos)
            {
                if (go.CompareTag(tag))
                {
                    TileData td = new TileData();
                    td.go = go;
                    td.cell_x = Mathf.RoundToInt(go.transform.position.x);
                    td.cell_y = Mathf.RoundToInt(go.transform.position.y);
                    allTileDatas.Add(td);
                }
            }
            return allTileDatas;
        }

    }
}
