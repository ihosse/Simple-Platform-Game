using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private IMove mover;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mover = GetComponent<IMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        float speed = mover.Speed;
        animator.SetFloat("Speed", Mathf.Abs(speed));

        if (speed != 0)
            spriteRenderer.flipX = speed < 0;
    }
}
