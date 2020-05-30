using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class SetMaterialColorFromTexture : MonoBehaviour
{

    public RenderTexture renderTexture;
    public float hueRotation;
    public float valueInvert;
    public Material material;
    public string color;
    Texture2D tex;
    void Start()
    {
        tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

    }

    void Update()
    {
        Color avg = GetAverageColor.ReadRenderTexture(renderTexture,tex);
        float H, S, V;

        Color.RGBToHSV(avg, out H, out S, out V);
        avg = Color.HSVToRGB((H + hueRotation) % 1, S, Mathf.Lerp(V, 1 - V, valueInvert));
        material.SetColor(color, avg);
    }
}


}