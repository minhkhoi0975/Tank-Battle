﻿/**
 * Shell.cs
 * Programmer: Khoi Ho (credits to professor Dearbon)
 * Description: This script handles the movement of a shell. When the shell hits the player or an enemy tank, it will create debris and explosion.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Shell : MonoBehaviour
{
    public float speed = 30.0f;
    public bool isShotByPlayer = false;  // Is this shell shot by player?

    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidBody.position += transform.forward.normalized * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Shell"))
        {
            // If the object is the player's tank, destroy it (don't destroy its parent).
            if (other.CompareTag("Player") && !isShotByPlayer)
            {
                // Play hit sound.
                AudioSource.PlayClipAtPoint(other.GetComponent<PlayerTankOnDestroy>().hitSound, other.transform.position + new Vector3(0.0f, 10.0f, 0.0f));

                // Create explosion.
                GameObject explosion = Instantiate(other.gameObject.GetComponent<PlayerTankOnDestroy>().explosionPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);
                Destroy(explosion.gameObject, explosion.GetComponent<ParticleSystem>().main.startLifetime.Evaluate(0.0f));

                // Create debris.
                Instantiate(other.gameObject.GetComponent<PlayerTankOnDestroy>().debrisPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);

                // Create smoke.
                Instantiate(other.gameObject.GetComponent<PlayerTankOnDestroy>().smokePrefab, other.gameObject.transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));

                // Destroy player tank.
                Destroy(other.gameObject);            
            }

            // If the object is an enemy tank, update the score of the current round and destroy the enemy tank.
            if (other.CompareTag("Character") && isShotByPlayer)
            {
                Debug.Log(Vector3.Dot(gameObject.transform.forward, other.transform.forward).ToString());

                // Play hit sound.
                AudioSource.PlayClipAtPoint(other.GetComponent<EnemyTankOnDestroy>().hitSound, other.transform.position + new Vector3(0.0f, 10.0f, 0.0f));

                // If the enemy tank can only be attacked from the back, check the dot product of the direction vectors of the shell and the enemy tank.
                if (!other.GetComponent<EnemyTankOnDestroy>().canOnlyBeAttackedOnBack || Vector3.Dot(gameObject.transform.forward, other.transform.forward) >= 0.8f)
                {
                    // Decrement the health.
                    other.gameObject.GetComponent<EnemyTankOnDestroy>().health--;

                    if (other.gameObject.GetComponent<EnemyTankOnDestroy>().health == 0)
                    {
                        // Increase the score in the current level.
                        GameManager.Instance.currentLevelScore += other.gameObject.GetComponent<EnemyTankOnDestroy>().score;

                        // Create explosion.
                        GameObject explosion = Instantiate(other.gameObject.GetComponent<EnemyTankOnDestroy>().explosionPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);
                        Destroy(explosion.gameObject, explosion.GetComponent<ParticleSystem>().main.startLifetime.Evaluate(0.0f));

                        // Create debris.
                        Instantiate(other.gameObject.GetComponent<EnemyTankOnDestroy>().debrisPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);

                        // Create smoke.
                        Instantiate(other.gameObject.GetComponent<EnemyTankOnDestroy>().smokePrefab, other.gameObject.transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));

                        // Destroy enemy tank.
                        Destroy(other.transform.parent.gameObject);
                    }
                }
            }

            // Destroy the shell.
            Destroy(gameObject);
        }
    }
}
