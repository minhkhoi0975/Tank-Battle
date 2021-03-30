﻿/**
 * ButtonExit.cs
 * Programmer: Khoi Ho
 * Description: This script stops the game when the player clicks the "Exit" button. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExit : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
}
