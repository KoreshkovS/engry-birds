using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private float _damageToDestroy = 4f;

    public static int EnemiesAlive = 0;

    private void Start()
    {
        EnemiesAlive++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude >= _damageToDestroy)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
        EnemiesAlive--;
        if (EnemiesAlive <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
        Destroy(gameObject);
    }
}
