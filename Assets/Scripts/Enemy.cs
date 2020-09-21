using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 100;
    public GameObject deathEffect;
    Material material;
    float fade = 1f;
    bool isDisolving = false;
    bool isDead = false;

    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            isDead = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;   
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            isDisolving = true;
            if (isDisolving)
            {
                fade -= Time.deltaTime;
                if (fade <= 0f)
                {
                    fade = 0f;
                    isDisolving = false;
                    Instantiate(deathEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                material.SetFloat("_Fade", fade);
            }
        }
    }
}
