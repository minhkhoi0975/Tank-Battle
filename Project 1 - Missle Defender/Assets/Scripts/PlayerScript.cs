﻿/**
 * PlayerScript.cs
 * Author: Khoi Ho
 * Description: This script deals with the player control and creates a particle effect when the player is hit by an enemy.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float      rotationSpeed = 40;          // Camera rotation speed.
    [SerializeField] private GameObject bulletToSpawn;               // The prefab of the bullet to be spawed.
    [SerializeField] private GameObject playerHurtParticleEffect;    // The particle effect when the player is hit by the enemy.

    private float rotationAngle = 0.0f;
    private int numberOfLives = 3;
    private int score;

    public int NumberOfLives { get { return numberOfLives; } }
    public int Score { get { return score; } set { score = value; } }


    // Update is called once per frame
    void Update()
    {
        // When the player presses A or D, the camera moves horizontally.
        rotationAngle += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);

        // When the player presses the left mouse button, the character shoots a bullet.
        if(Input.GetButtonDown("Fire1") && isAlive())
        {
            // Spawn a bullet.
            GameObject newBullet = Instantiate(bulletToSpawn, transform.position, transform.rotation);

            // Get access to the bullet script.
            BulletScript newBulletScript = newBullet.GetComponent<BulletScript>();

            // Set the owner of the bullet.
            newBulletScript.player = gameObject;

            // Tell the bullet where it goes to.
            Camera camera = GetComponentInChildren<Camera>();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            newBulletScript.destination = ray.GetPoint(newBulletScript.MaxDistanceFromOwner);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy the enemy if it hits the player.
        if(other.CompareTag("Enemy"))
        {
            // Decrement the number of lives, but don't make it below 0.
            numberOfLives = (numberOfLives == 0 ? 0 : numberOfLives - 1);

            // Create "player hurt" particles.
            GameObject playerHurt = Instantiate(playerHurtParticleEffect, transform.position, transform.rotation);
            Destroy(playerHurt, playerHurt.GetComponent<ParticleSystem>().main.startLifetime.Evaluate(0.0f));

            // Destroy the enemy.
            Destroy(other.gameObject);
        }
    }

    // Check if the number of lives of the player is greater than 0.
    public bool isAlive()
    {
        return numberOfLives > 0;
    }
}
