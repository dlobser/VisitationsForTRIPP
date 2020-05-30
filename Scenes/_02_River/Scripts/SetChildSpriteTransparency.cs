using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChildSpriteTransparency : MonoBehaviour
{
    SpriteRenderer[] sprites;

    void Start()
    {
    }

    public void SetTransparency(float t){
        if(sprites==null)
            sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sp in sprites){
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, t);
        }
    }
   
}
