﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static int collectibleCount = 0;

    public int score = 100;

    private void Awake()
    {
        collectibleCount++;
    }

    private void OnDestroy()
    {
        collectibleCount--;
    }

    void Update()
    {
        // Rotate the collectible.
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0.0f, 60.0f * Time.deltaTime, 0.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player touches the collectible, destroy the collectible and increase player's score.
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.currentLevelScore += score;
            Destroy(gameObject);
        }
    }  
}
