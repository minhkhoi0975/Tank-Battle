﻿/**
 * HUDUpdatePlayerStats.cs
 * Programmer: Khoi Ho
 * Description: This script displays Player's stats.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HUDUpdatePlayerStats : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text =
        "Lives: " + GameManager.Instance.currentLives + "\n" +
        "Score: " + (GameManager.Instance.totalScore + GameManager.Instance.currentLevelScore) + "\n" +
        "Ammo: " + GameManager.Instance.currentAmmo + "/" + GameManager.Instance.maxAmmo;
    }
}
