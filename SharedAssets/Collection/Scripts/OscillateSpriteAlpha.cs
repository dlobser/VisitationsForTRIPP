using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON
{
    public class OscillateSpriteAlpha : MonoBehaviour
    {

        public Vector2 transparency;
        public float speed;
        float random;

        void Start()
        {
            random = Random.value * 100;
        }

        void Update()
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                SpriteRenderer sprite = this.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                Color c = sprite.color;
                float alpha = DLUtility.remap(Mathf.Sin(random + i + Time.timeSinceLevelLoad * speed), 0, 1, transparency.x, transparency.y);
                if (alpha <= 0)
                    sprite.enabled = false;
                else
                    sprite.enabled = true;
                sprite.color = new Color(c.r, c.g, c.b, alpha);
            }
        }
    }
}