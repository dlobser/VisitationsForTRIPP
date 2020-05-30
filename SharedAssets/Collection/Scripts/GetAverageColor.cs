using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public static class GetAverageColor : object {

    public static Color32 ReadRenderTexture(RenderTexture renTex, Texture2D tex)
    {
        //Texture2D tex = new Texture2D(renTex.width, renTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = renTex;
        tex.ReadPixels(new Rect(0, 0, renTex.width, renTex.height), 0, 0);
        return AverageColorFromTexture(tex,1);
    }

    public static Color32 ReadRenderTexture(RenderTexture renTex, float power,Texture2D tex)
    {
        //Texture2D tex = new Texture2D(renTex.width, renTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = renTex;
        tex.ReadPixels(new Rect(0, 0, renTex.width, renTex.height), 0, 0);
        return AverageColorFromTexture(tex,power);
    }

    public static Color32 AverageColorFromTexture(Texture2D tex, float power)
    {

        Color32[] texColors = tex.GetPixels32();

        float total = (float)texColors.Length;

        float r = 0;
        float g = 0;
        float b = 0;

        for (int i = 0; i < total; i++)
        {

            r += Mathf.Pow((float)texColors[i].r / 255, power);
            g += Mathf.Pow((float)texColors[i].g / 255, power);
            b += Mathf.Pow((float)texColors[i].b / 255, power);

        }
        return new Color32((byte)(int)((r / total) * 255f), (byte)(int)((g / total) * 255f), (byte)(int)((b / total) * 255f), 0);


    }
}


}