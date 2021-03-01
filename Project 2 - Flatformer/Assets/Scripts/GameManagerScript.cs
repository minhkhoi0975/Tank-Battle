﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private static GameManagerScript instance = null;

    public static GameManagerScript Instance 
    { 
        get 
        { 
            return instance; 
        } 
    }

    private int coinsCollected = 0;

    public int CoinsCollected
    {
        get
        {
            return coinsCollected;
        }
        set
        {
            coinsCollected = value < 0 ? 0 : value;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
