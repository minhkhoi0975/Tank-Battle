﻿/**
 * PlayerCamera.cs
 * Programmer: Khoi Ho
 * Description: This script makes a camera follow the player. When the player is destroyed, the game restarts the level or loads the main menu.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;

    Vector3 offset;

    bool isPlayerDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        offset = player.transform.position - gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Always keep the same distance to the player.
            gameObject.transform.position = player.transform.position - offset;
        }

        // If the player is destroyed, replay the level if the player still has lives. Otherwise, go to the Game Over scene.
        else
        {
            StartCoroutine(GameOver());
        }
        
    }

    IEnumerator GameOver()
    {
        if (!isPlayerDestroyed)
        {
            isPlayerDestroyed = true;

            // Decrement the player's number of lives.
            GameManager.Instance.currentLives--;

            // If the player still has lives, get rid of the score of the current level.
            if (GameManager.Instance.currentLives > 0)
            {
                GameManager.Instance.currentLevelScore = 0;
            }

            // Wait for 5 seconds.
            yield return new WaitForSeconds(5);

            // If the player still has lives, restart the level. Otherwise, go to the Game Over scene.
            if (GameManager.Instance.currentLives > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                SceneManager.LoadScene("GameOver");
            }
        }     
    }
}
