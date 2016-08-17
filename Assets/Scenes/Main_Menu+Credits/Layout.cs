using UnityEngine;
using System.Collections;

public class Layout : MonoBehaviour {

    private int screenX = 1024;
    private int screenY = 768;

    void Awake()
    {
        TweakLayout();
    }

    void Update()
    {
        if (screenX != Screen.width || screenY != Screen.height)
        {
            screenX = Screen.width;
            screenY = Screen.height;

            TweakLayout();
        }
    }

    void TweakLayout()
    {
        float currentRatio = (float)screenX / screenY;
        if (currentRatio <= 1.555f)
        {
            // tweak UI
        }
        else
        {
            // tweak UI
        }
    }
}
