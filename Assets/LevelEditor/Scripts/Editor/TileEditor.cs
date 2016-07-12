using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// @todo: make layers without sorting layers, with specific ids instead, or just use their transform order
// @todo:make sure to tell them to use the layer

namespace LevelEditor
{
    [ExecuteInEditMode]
    public class TileEditorWindow : EditorWindow
    {
        const int Mode_Tiles = 0;
        const int Mode_Prefabs = 1;
        static string[] modes = { "Tiles", "Prefabs" };

        const int Tool_Paint = 0;
        const int Tool_Erase = 1;
        static string[] tools = { "Paint", "Erase" };

        const int LayerAction_Minus = 0;
        const int LayerAction_Plus = 1;
        const int LayerAction_Remove = 2;
        const int LayerAction_Rename = 3;
        static string[] layerActions = { "-", "+", "x", "r" };

        // Tile sprites
        static Texture[] allEditorTilesets;
        static Sprite[] allEditorSprites;
        static Sprite[] currentEditorSprites;
        static List<int> selectedTilesIds = new List<int>();
        static List<Sprite> selectedTilesSprites = new List<Sprite>();

        // Tile objects
        static GameObject[] loadedPrefabs = new GameObject[0];
        static GameObject selectedPrefab;

        // Editor window
        static EditorWindow window;
        static GameObject editorFieldGo;
        static Texture2D editorSelectionTexture;
        static int editorCurrentSelectedTileSet = 0;
        static Vector2 scrollAreaPosition = Vector2.zero;

        static int currentModeID = 0;

        static int currentToolID = 0;

        // Mouse editing
        static Event e;
        static Vector3 currentMouseSnapPos;

        // Layers
        static GameObject layersHolderGo;
        static List<Transform> layerTransforms = new List<Transform>();
        static int selectedLayerID = 0;
        static bool bHighlightLayer = false;
        static int renameId = -1;
        static Texture2D texVisible;
        static Texture2D texHidden;
        static Color highlightColor = Color.red;

        // Configuration
        const string TILESET_EDITOR_FIELD_NAME = "TileEditorField";
        const string LAYER_HOLDER_NAME = "TilesLayerHolder";

        #region Setup
        [MenuItem("Window/TileEditor")]
        public static void OpenTileEditor()
        {
            ReloadAssets();

            // Creates the highlight color for selecting tiles
            editorSelectionTexture = new Texture2D(1, 1);
            editorSelectionTexture.alphaIsTransparency = true;
            editorSelectionTexture.filterMode = FilterMode.Point;
            editorSelectionTexture.SetPixel(0, 0, new Color(.5f, .5f, 1f, .5f));
            editorSelectionTexture.Apply();

            window = EditorWindow.GetWindow(typeof(TileEditorWindow));
            window.minSize = new Vector2(325, 400);
        }

        public static void ReloadAssets()
        {
            AssetDatabase.Refresh();
            layerTransforms.Clear();
            allEditorTilesets = Resources.LoadAll<Texture>("Tilesets"); // Load all tilesets as texture
            allEditorSprites = Resources.LoadAll<Sprite>("Tilesets"); // Load all tileset sub objects as tiles
            texVisible = Resources.Load("Icons/Visible") as Texture2D;
            texHidden = Resources.Load("Icons/Hidden") as Texture2D;
            LoadTileset(0); // Process loaded tiles into proper tilesets
        }

        public static void OnEnable()
        {
            ReloadAssets();
        }

        static void ResetGameObjects()
        {
            // Intended to create objects required for the tileset editor to work
            if (editorFieldGo == null)
            {
                editorFieldGo = GameObject.Find(TILESET_EDITOR_FIELD_NAME);
                if (editorFieldGo == null)
                {
                    editorFieldGo = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    editorFieldGo.name = TILESET_EDITOR_FIELD_NAME;
                    editorFieldGo.layer = LayerMask.NameToLayer("TileEditor");
                }

                if (editorFieldGo.GetComponent<Renderer>())
                {
                    DestroyImmediate(editorFieldGo.GetComponent<Renderer>());
                }

            }
            if (layersHolderGo == null)
            {
                layersHolderGo = GameObject.Find(LAYER_HOLDER_NAME);
                if (layersHolderGo == null)
                {
                    layersHolderGo = new GameObject(LAYER_HOLDER_NAME);

                    // Setup at least 1 layer
                    AddLayer();
                    ResetLayers();
                }
            }

            // Make sure the tile editor is kept in place
            editorFieldGo.transform.position = Vector3.zero;
            editorFieldGo.transform.localScale = Vector3.one * 1000000;

            if (window != null)
            {
                // Force repaint to show proper layers if the window exists
                window.Repaint();
            }
        }


        private void OnSelectionChange()
        {
            Repaint();
        }

        #endregion

        #region Layers

        // Creates a new layer, renames it, and sets it's proper layer settings
        static void AddLayer()
        {
            layerTransforms.Add(new GameObject().transform);
            int index = layerTransforms.Count - 1;
            layerTransforms[index].name = "New Layer";
            layerTransforms[index].transform.parent = layersHolderGo.transform;
            SpriteRenderer tmpRenderer = layerTransforms[index].gameObject.AddComponent<SpriteRenderer>();
            tmpRenderer.sortingOrder = index;
        }

        // Rebuilds a list of all of the layers
        static void ResetLayers()
        {
            layerTransforms.Clear();
            foreach (Transform t in layersHolderGo.GetComponentsInChildren(typeof(Transform), true))
            {
                if (t.parent == layersHolderGo.transform)
                {
                    layerTransforms.Add(t);
                }
            }
        }

        static private List<Transform> SortLayers(List<Transform> layers)
        {
            // Sorts layers based on their sorting order
            layers.Sort(delegate(Transform x, Transform y)
            {
                int sortOrderX = x.GetComponent<SpriteRenderer>().sortingOrder;
                int sortOrderY = y.GetComponent<SpriteRenderer>().sortingOrder;
                return sortOrderX.CompareTo(sortOrderY);
            });
            return layers;
        }

        private void HighlightLayer(GameObject layerGo, bool choice)
        {
            // Highlights the current layer if status is true, unhighlights if false
            foreach (SpriteRenderer sr in layersHolderGo.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = new Color(1, 1, 1, 1);
            }
            foreach (SpriteRenderer sr in layerTransforms[selectedLayerID].GetComponentsInChildren<SpriteRenderer>())
            {
                if (choice)
                {
                    sr.color = highlightColor;
                }
                else
                {
                    sr.color = new Color(1, 1, 1, 1);
                }
            }
        }

        #endregion

        #region Tileset
        static void LoadTileset(int tileSetID)
        {
            // Loads the tilesets into proper variables
            ResetGameObjects();

            currentEditorSprites = new Sprite[allEditorSprites.Length];
            int curCount = 0;
            int i = 0;

            // Sets the displayed tileset based on the name of the tile. Also counts the number of tiles in the new tileset that's loaded.
            for (i = 0; i < allEditorSprites.Length; i++)
            {
                if (allEditorSprites[i].texture.name == allEditorTilesets[tileSetID].name)
                {
                    currentEditorSprites[curCount] = allEditorSprites[i];
                    curCount++;
                }
            }

            // Resizes the displayed tileset's array size to match the new size
            Sprite[] tmpSprites = new Sprite[curCount];
            for (i = 0; i < curCount; i++)
            {
                tmpSprites[i] = currentEditorSprites[i];
            }
            currentEditorSprites = tmpSprites;
        }

        #endregion


        #region GUI

        void DrawModeTilesets()
        {
            // Load tilesets
            int i,j;
            string[] tilesetNames = new string[allEditorTilesets.Length];
            for (i = 0; i < allEditorTilesets.Length; i++)
            {
                try
                {
                    tilesetNames[i] = allEditorTilesets[i].name;
                }
                catch (System.Exception ex)
                {
                    Debug.Log("There was an error getting the tilesetNames of the files. We'll try to reload the tilesets. If this continues to show, please close the script and try remimporting and check your images.");
                    Debug.Log("Full system error: " + ex.Message);
                    ReloadAssets();
                }
            }

            // Show tileset options
            EditorGUILayout.BeginHorizontal();
            int selectedTilesetIndex = EditorGUILayout.Popup("Tileset", editorCurrentSelectedTileSet, tilesetNames);
            if (GUILayout.Button("Reload")) ReloadAssets();
            EditorGUILayout.EndHorizontal();

            // @todo: check this!
            //Causes an error on editor load if the window is visible.
            //This seems to be a problem with how the gui is drawn the first
            //loop of the script. It only happens the once, and I can't figure
            //out why. I've been trying for literally weeks and still can't
            //find an answer. This is the only known bug, but it doesn't
            //stop the functionality of the script in any way, and only serves
            //as an annoying message on unity load or this script being 
            //recompiled. Sorry for this bug. I am looking for a way to remove
            //this error, but I really am stummped as to why it's happening
            //and I just can not find an answer online.

            // Highlight
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Highlight Current Layer", GUILayout.Width(150));
            bHighlightLayer = EditorGUILayout.Toggle(bHighlightLayer, GUILayout.Width(25));
            highlightColor = EditorGUILayout.ColorField(highlightColor);
            EditorGUILayout.EndHorizontal();

            // View current tileset
            if (selectedTilesetIndex != editorCurrentSelectedTileSet)
            {
                LoadTileset(selectedTilesetIndex);
            }
            editorCurrentSelectedTileSet = selectedTilesetIndex;

            // Cols and Rows
            i = 0;
            int columnCount = Mathf.RoundToInt((position.width) / 38) - 2;
            j = 0;
            int current = 0;

            // Scroll area
            scrollAreaPosition = EditorGUILayout.BeginScrollView(scrollAreaPosition, false, true, GUILayout.Width(position.width));
            GUILayout.BeginHorizontal();
            for (int q = 0; q < allEditorSprites.Length; q++)
            {
                Sprite currentSprite = allEditorSprites[q];
                try
                {
                    // Iff it's the tiles inside the image, not the entire image
                    if (currentSprite.texture.name == tilesetNames[editorCurrentSelectedTileSet] && currentSprite.name != tilesetNames[editorCurrentSelectedTileSet])
                    {
                        Rect singleTileSpriteRect = new Rect(
                                            currentSprite.rect.x / currentSprite.texture.width,
                                            currentSprite.rect.y / currentSprite.texture.height,
                                            currentSprite.rect.width / currentSprite.texture.width,
                                            currentSprite.rect.height / currentSprite.texture.height
                                            );//gets the x and y position in pixels of where the image is. Used later for display.

                        // Selectio nbutton
                        if (GUILayout.Button("", GUILayout.Width(34), GUILayout.Height(34)))
                        {
                            if (selectedTilesIds != null && !e.shift)
                            {
                                // Empty the selected tile list if shift isn't held. Allows multiselect of tiles.
                                selectedTilesIds.Clear();
                                selectedTilesSprites.Clear();
                            }
                            selectedTilesIds.Add(current); // Adds clicked on tile to list of selected tiles.
                            selectedTilesSprites.Add(currentEditorSprites[current]);
                        }

                        GUI.DrawTextureWithTexCoords(new Rect(5 + (j * 38), 4 + (i * 37), 32, 32), currentSprite.texture, singleTileSpriteRect, true); //draws tile base on pixels gotten at the beginning of the loop
                        if (selectedTilesIds != null && selectedTilesIds.Contains(current))
                        {
                            //if the current tile is inside of the list of selected tiles, draw a highlight indicator over the button.
                            if (editorSelectionTexture)
                            {
                                GUI.DrawTexture(new Rect(5 + (j * 38), 4 + (i * 37), 32, 32), editorSelectionTexture, ScaleMode.ScaleToFit, true);
                            }
                        }

                        // Next row
                        if (j < columnCount)
                        {
                            j++;
                        }
                        else
                        {
                            // if we have enough columns to fill the scroll area, reset the column count and start a new line of buttons
                            j = 0;
                            i++;
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                        }
                        current++;
                    }
                }
                catch (System.Exception ex)
                {
                    if (ex.Message.StartsWith("IndexOutOfRangeException"))
                    {
                        Debug.Log("Tileset index was out of bounds, reloading and trying again.");
                        ReloadAssets();
                        return;
                    }
                }
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
        }

        void DrawModePrefabs()
        {
            // Load prefabs
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Loaded prefabs: " + loadedPrefabs.Length);
            if (GUILayout.Button("Reload"))
            {
                loadedPrefabs = Resources.LoadAll<GameObject>("TilePrefabs");
            }
            GUILayout.EndHorizontal();

            // Show prefabs
            scrollAreaPosition = EditorGUILayout.BeginScrollView(scrollAreaPosition, false, true, GUILayout.Width(position.width));
            GUILayout.BeginVertical();
            for (int q = 0; q < loadedPrefabs.Length; q++)
            {
                GameObject currentPrefab = loadedPrefabs[q];

                var guiStyle = new GUIStyle(GUI.skin.button);
                if (selectedPrefab == currentPrefab)
                    guiStyle.normal.textColor = Color.red;

                if (GUILayout.Button(currentPrefab.name, guiStyle))
                {
                    selectedPrefab = currentPrefab;
                }
            }
            GUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

       bool editorEnabled;
       void OnGUI()
       {
           int i;
           ResetGameObjects();
           e = Event.current; // Gets current event (mouse move, repaint, keyboard press, etc)


           if (renameId != -1 && (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter))
           {
               renameId = -1;
           }

           if (allEditorTilesets == null) // Check to make certain there is actually a tileset in the resources/tileset folder.
           {
                // @todo: remove this check since we can just use prefabs
               EditorGUILayout.LabelField("No tilesets found. Retrying.");
                ReloadAssets();
           }
           else
           {
                // Utils
                bool wasEnabled = editorEnabled;
                editorEnabled  = EditorGUILayout.Toggle("Enabled", editorEnabled);
                if (wasEnabled != editorEnabled)
                {
                    if (editorEnabled)
                    {
                        SceneView.onSceneGUIDelegate += OnSceneGUI;
                    }
                    else
                    {
                        SceneView.onSceneGUIDelegate -= OnSceneGUI;
                    }
                }


                // Tools
                EditorGUILayout.BeginHorizontal(GUILayout.Width(position.width));
                EditorGUILayout.LabelField("Tool");
                GUILayout.FlexibleSpace();
                currentToolID = GUILayout.Toolbar(currentToolID, tools);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                // Mode
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Mode");
                GUILayout.FlexibleSpace();
                currentModeID = GUILayout.Toolbar(currentModeID, modes);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                switch (currentModeID)
                {
                    case Mode_Tiles:
                        DrawModeTilesets();
                        break;
                    case Mode_Prefabs:
                        DrawModePrefabs();
                        break;
                }


                // Layers
                GUILayout.Label("Layers:");
                if (GUILayout.Button("Add Layer"))
                {
                    AddLayer();
                }
                ResetLayers();
                layerTransforms = SortLayers(layerTransforms); //Sort the layers based on their sorting order instead of name
                int destroyFlag = -1;

                // Draw each layer
                for (i = 0; i < layerTransforms.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    RectOffset tmpPadding = GUI.skin.button.padding;
                    GUI.skin.button.padding = new RectOffset(3, 3, 3, 3);

                    // Visible / Hidden
                    if (layerTransforms[i].gameObject.activeSelf)
                    {
                        if (GUILayout.Button(texVisible, GUILayout.Width(15), GUILayout.Height(15)))
                        {
                            layerTransforms[i].gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button(texHidden, GUILayout.Width(15), GUILayout.Height(15)))
                        {
                            layerTransforms[i].gameObject.SetActive(true);
                        }
                    }
                    GUI.skin.button.padding = tmpPadding;

                    // Layer selection
                    if (i == renameId)
                    {
                        layerTransforms[i].name = EditorGUILayout.TextField(layerTransforms[i].name);

                    }
                    else { 
                        if (i == selectedLayerID)
                        {
                            // Toggle it
                            EditorGUILayout.ToggleLeft(layerTransforms[i].name + " - " + layerTransforms[i].GetComponent<SpriteRenderer>().sortingOrder, true);
                        }
                        else
                        {
                            // If not the selected layer, and is clicked, set it as the selected layer
                            if (EditorGUILayout.ToggleLeft(layerTransforms[i].name + " - " + layerTransforms[i].GetComponent<SpriteRenderer>().sortingOrder, false))
                            {
                                selectedLayerID = i;
                            }
                        }
                    }

                    // Actions
                    int layerActionID = GUILayout.Toolbar(-1, layerActions);
                    switch (layerActionID)
                    {
                        case LayerAction_Minus:
                            if (i > 0)
                            {
                                // Moves layer and all tiles in it to move away from the camera, and moves the layer above it toward the camera.
                                layerTransforms[i - 1].GetComponent<SpriteRenderer>().sortingOrder += 1;
                                int upLayer = layerTransforms[i - 1].GetComponent<SpriteRenderer>().sortingOrder;

                                foreach (SpriteRenderer sr in layerTransforms[i - 1].GetComponentsInChildren<SpriteRenderer>())
                                {
                                    sr.sortingOrder = upLayer;
                                }

                                int downLayer = layerTransforms[i].GetComponent<SpriteRenderer>().sortingOrder -= 1;
                                foreach (SpriteRenderer sr in layerTransforms[i].GetComponentsInChildren<SpriteRenderer>())
                                {
                                    sr.sortingOrder = downLayer;
                                }
                                selectedLayerID = i - 1;
                            }
                            layerTransforms = SortLayers(layerTransforms);
                            break;
                        case LayerAction_Plus:
                            if (i < layerTransforms.Count - 1)
                            {
                                // Moves layer and all tiles in it to move toward the camera, and moves the layer above it away from the camera.
                                layerTransforms[i + 1].GetComponent<SpriteRenderer>().sortingOrder -= 1;
                                int upLayer = layerTransforms[i + 1].GetComponent<SpriteRenderer>().sortingOrder;

                                foreach (SpriteRenderer sr in layerTransforms[i + 1].GetComponentsInChildren<SpriteRenderer>())
                                {
                                    sr.sortingOrder = upLayer;
                                }

                                //layers[i].GetComponent<SpriteRenderer>().sortingOrder += 1;
                                int downLayer = layerTransforms[i].GetComponent<SpriteRenderer>().sortingOrder += 1;

                                foreach (SpriteRenderer sr in layerTransforms[i].GetComponentsInChildren<SpriteRenderer>())
                                {
                                    sr.sortingOrder = downLayer;
                                }
                                selectedLayerID = i + 1;
                            }
                            layerTransforms = SortLayers(layerTransforms);
                            break;
                        case LayerAction_Remove:
                            // Deletes the layer game object, which also deletes all the children
                            destroyFlag = i;
                            break;
                        case LayerAction_Rename:
                            if (renameId == -1)
                                renameId = i;
                            else
                                renameId = -1;
                            break;
                        default:
                            // Do nothing if a button wasn't pressed
                            break;
                    }
                    EditorGUILayout.EndHorizontal();
                }

                // Double check to make certain a layer of some sort is selected and is in valid range
                if (selectedLayerID <= layerTransforms.Count - 1 && layerTransforms[selectedLayerID] != null)
                {
                    HighlightLayer(layerTransforms[selectedLayerID].gameObject, bHighlightLayer);
                }

                // Destroy case
                if (destroyFlag != -1)
                {
                    DestroyImmediate(layerTransforms[destroyFlag].gameObject);
                    return; // Breaks method to not have errors down the line. Forces reload of tilesets to keep inside the bounds of the array.
                }
                destroyFlag = -1;
            }
        }

        static void OnSceneGUI(SceneView sceneview)
        { 
            if (layersHolderGo != null)
            {
                // At least one layer should always be there
                if (layersHolderGo.transform.childCount <= 0)
                {
                    AddLayer();
                    ResetLayers();
                }
                if (selectedLayerID >= layersHolderGo.transform.childCount)
                {
                    ResetLayers();
                }

                if (Event.current.type == EventType.layout)
                {
                    if (window)
                        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(window.GetHashCode(), FocusType.Passive));
                }
                e = Event.current;

                // Hit the field with the mouse
                Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                RaycastHit hit;
                LayerMask mask = new LayerMask();
                mask.value = 1 << LayerMask.NameToLayer("TileEditor");
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, mask.value, QueryTriggerInteraction.UseGlobal))
                {
                    // Draw the selection widget
                    Handles.BeginGUI();
                    Handles.color = Color.white;
                    Handles.Label(currentMouseSnapPos, "(" + currentMouseSnapPos.x + "," + currentMouseSnapPos.y + ")", EditorStyles.boldLabel);
                    Handles.DrawLine(currentMouseSnapPos + new Vector3(0, 0, 0), currentMouseSnapPos + new Vector3(1, 0, 0));
                    Handles.DrawLine(currentMouseSnapPos + new Vector3(1, 0, 0), currentMouseSnapPos + new Vector3(1, 1, 0));
                    Handles.DrawLine(currentMouseSnapPos + new Vector3(1, 1, 0), currentMouseSnapPos + new Vector3(0, 1, 0));
                    Handles.DrawLine(currentMouseSnapPos + new Vector3(0, 1, 0), currentMouseSnapPos + new Vector3(0, 0, 0));
                    Handles.EndGUI();

                    int x = Mathf.FloorToInt(hit.point.x);
                    int y = Mathf.FloorToInt(hit.point.y);

                    // Mouse action
                    if (e.isMouse)
                    {
                        if (e.button == 0 && (e.type == EventType.MouseUp || e.type == EventType.MouseDrag))
                        {
                            string coordsString = "(" + x + "," + y + ")";
                            string nameString = "";
                            switch (currentModeID)
                            {
                                case Mode_Tiles: nameString = "Tile"; break;
                                case Mode_Prefabs: nameString = "O-" + selectedPrefab.name; break;
                            }

                            switch (currentToolID)
                            {
                                case Tool_Paint:
                                    GameObject tmpObj = null;
                                    switch (currentModeID)
                                    {
                                        case Mode_Tiles:
                                            tmpObj = GenerateSpriteTile(nameString, coordsString);

                                            Sprite[] tmpSelectedSprites = new Sprite[selectedTilesSprites.Count];
                                            selectedTilesSprites.CopyTo(tmpSelectedSprites);

                                            // Choose a tile at random from the selected ones //@todo: remove this?
                                            if (tmpSelectedSprites.Length > 0)
                                            {
                                                tmpObj.GetComponent<SpriteRenderer>().sprite = tmpSelectedSprites[UnityEngine.Random.Range((int)0, (int)tmpSelectedSprites.Length)];
                                            }
                                            else
                                            {
                                                Debug.LogWarning("Tile not selected for painting. Please select a tile to paint.");
                                            }
                                            break;
                                        case Mode_Prefabs:
                                            tmpObj = GenerateObjectTile(nameString, coordsString, selectedPrefab);
                                            break;
                                    }
                                    Undo.RegisterCreatedObjectUndo(tmpObj, "Created Tile");

                                    if (selectedLayerID > layerTransforms.Count - 1)
                                    {
                                        selectedLayerID = layerTransforms.Count - 1;
                                        ResetLayers();
                                        layerTransforms = SortLayers(layerTransforms);
                                    }
                                    tmpObj.transform.localPosition = new Vector3(x + .5f, y + .5f, layerTransforms[selectedLayerID].transform.position.z);
                                    tmpObj.transform.parent = layerTransforms[selectedLayerID];
                                    tmpObj.transform.localScale = new Vector3(1, 1, 1);
                                    tmpObj.tag = "Tile";
                                    break;

                                case Tool_Erase:
                                    DestroyTilesAt(layerTransforms[selectedLayerID], coordsString);
                                    break;
                            }
                        }
                    }
                    currentMouseSnapPos.x = (float)x;
                    currentMouseSnapPos.y = (float)y;
                    if (layerTransforms[selectedLayerID] != null)
                        currentMouseSnapPos.z = layerTransforms[selectedLayerID].position.z - 1;
                    else
                        currentMouseSnapPos.z = 0;
                }
            }
            else
            {
                ResetGameObjects();
            }
            SceneView.RepaintAll();
        }

        #endregion


        #region Tile Generation

        static GameObject GenerateSpriteTile(string nameString, string coordsString)
        {
            string completeName = nameString + coordsString;

            DestroyTilesAt(layerTransforms[selectedLayerID], coordsString);

            GameObject tmpObj = null;
            tmpObj = new GameObject(completeName);
            tmpObj.AddComponent<SpriteRenderer>();
            tmpObj.GetComponent<SpriteRenderer>().sortingOrder = layerTransforms[selectedLayerID].GetComponent<SpriteRenderer>().sortingOrder;
           
            SceneView.RepaintAll();
            return tmpObj;
        }

        static void DestroyTilesAt(Transform layer, string coordsString)
        {
            // Destroy any tile in that place and layer
            if (layer != null)
            {
                Transform[] children = layer.gameObject.GetComponentsInChildren<Transform>();
                foreach (Transform current in children)
                    if (current && current.name.Contains(coordsString) && current.parent == layer)
                        DestroyImmediate(current.gameObject);
            }
        }

        static GameObject GenerateObjectTile(string nameString, string coordsString, GameObject prefabGo)
        {
            string completeName = nameString + coordsString;

            DestroyTilesAt(layerTransforms[selectedLayerID], coordsString);

            GameObject tmpObj = null;
            tmpObj = PrefabUtility.InstantiatePrefab(prefabGo) as GameObject;
            tmpObj.name = completeName;

            SceneView.RepaintAll();
            return tmpObj;
        }

        #endregion
    }

}
