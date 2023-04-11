using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    [SerializeField]
    private float alphaMultiplier = 0.85f;

    private Transform player;

    private SpriteRenderer SR;
    private SpriteRenderer playerSr;

    private Color color;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSr = player.GetComponent<SpriteRenderer>();
        alpha = alphaSet;
        SR.sprite = playerSr.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    
    private void FixedUpdate()
    {
        alpha *= alphaMultiplier;
        color = new(1, 1, 1, alpha);
        SR.color = color;
        if (Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
