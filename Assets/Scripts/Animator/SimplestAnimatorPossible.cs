using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplestAnimatorPossible : MonoBehaviour
{
    public Sprite sprite1, sprite2;

    public Sprite CurrentSprite => spriteRenderer.sprite;
    public SpriteRenderer spriteRenderer;

    public float timePerSprite = 0.3f;
    float variation;

    private void Start()
    {
        variation = Random.Range(0, timePerSprite * 2);
    }

    void Update()
    {
        if(Mathf.PingPong(Time.time + variation, timePerSprite*2 + variation / 2) - timePerSprite > 0)
        {
            if(CurrentSprite != sprite1)
            {
                spriteRenderer.sprite = sprite1;
            }
        }
        else
        {
            if (CurrentSprite != sprite2)
            {
                spriteRenderer.sprite = sprite2;
            }
        }
    }
}
