using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmostSimplestAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    [SerializeField] float timePerFrame = 0.3f;
    float timeStarted;

    public UnityEngine.Events.UnityEvent onCompleted;

    private void Start()
    {
        timeStarted = Time.time;
        GetComponent<SimplestAnimatorPossible>().enabled = false;
    }
    void Update()
    {
        var spriteIndex = (int)((Time.time - timeStarted) / timePerFrame);
        if (spriteIndex > sprites.Length+1) onCompleted.Invoke();
        spriteIndex = Mathf.Clamp(spriteIndex, 0, sprites.Length - 1);
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
