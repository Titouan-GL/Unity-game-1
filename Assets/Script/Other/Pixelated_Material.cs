using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pixelated_Material : MonoBehaviour
{
    public enum PixelScreenMode { Resize, Scale }

    [System.Serializable]
    
    public struct ScreenSize
    {
        public int width;
        public int height;
    }

    [Header("Screen scaling settings")]
    public PixelScreenMode mode;
    public ScreenSize targetScreenSize = new ScreenSize{ width = 256, height = 144 };
    public uint screenScaleFactor = 1;

    private Camera renderCamera;
    private RenderTexture renderTexture;
    private int screenWidth, screenHeight;

    [Header("Display")]
    public RawImage display;

    public void Init() {
        //Initialize the camera and get screen size values
        if (!renderCamera) renderCamera = GetComponent<Camera>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Prevent any error
        if (screenScaleFactor < 1) screenScaleFactor = 1;
        if (targetScreenSize.width < 1) targetScreenSize.width =1;
        if (targetScreenSize.height <1) targetScreenSize.height = 1;

        // Calculate the render texture size
        int width = mode == PixelScreenMode.Resize ? (int)targetScreenSize.width : screenWidth / (int)screenScaleFactor;
        int height = mode == PixelScreenMode.Resize ? (int)targetScreenSize.height : screenHeight / (int)screenScaleFactor;

        // Initialize the render texture
        renderTexture = new RenderTexture(width, height, 24) {
            filterMode = FilterMode.Point,
            antiAliasing = 1
        };

        // Set the render texture as the camera's output
        renderCamera.targetTexture = renderTexture;

        // Attaching texture to the display UI RawImage
        display.texture = renderTexture;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        //Re initialize system if the screen has been resized
        if(CheckScreenResize()) Init();
    }

    public bool CheckScreenResize(){
        //Check whether the screen has been resized
        return Screen.width != screenWidth || Screen.height != screenHeight;
    }
}
