using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int maceDamage = 10;
    [SerializeField] int rifleDamage = 30;
    [SerializeField] int tridentamage = 20;
    [SerializeField] int health;

    [SerializeField] Animator enemyAnimator;
    [SerializeField] AnimationData animData;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] GameObject canvas;
    [SerializeField] Transform bullets;

    [SerializeField] LayerMask rifleLayer;

    private void OnEnable()
    {
        PlayerAnimationManager.OnRifleShoot += RifleSequence;
    }
    
    private void OnDisable()
    {
        PlayerAnimationManager.OnRifleShoot -= RifleSequence;
    }

    RaycastHit hitData;

    void Start()
    {
        health = 100;
    }


    void Update()
    {
        Mathf.Clamp(health, 0, 100);
        healthText.text = $"{health}";
    }

    private void OnTriggerEnter( Collider other )
    {
        if(other.CompareTag("Mace"))
        {
            Debug.Log( "Hit by Mace" );
            TakeDamage( maceDamage );
        }
        else if(other.CompareTag("Trident"))
        {
            Debug.Log( "Hit by Trident" );
            TakeDamage( tridentamage );
        }
        else
        {
            RifleSequence();
        }
    }

    void RifleSequence()
    {
        Ray ray = new Ray( bullets.position, bullets.forward );
        if( Physics.Raycast( ray, out hitData, Mathf.Infinity ) )
        {
            if( hitData.collider != null )
            {
                Debug.Log( $"{hitData.collider.gameObject.name}" );
                if(hitData.collider.CompareTag( "Enemy" ))
                {
                    TakeDamage( rifleDamage );
                }
            }
        }
    }

    private void OnTriggerExit( Collider other )
    {
        enemyAnimator.SetBool( "isHit", false );
    }

    void TakeDamage(int damage)
    {
        enemyAnimator.SetBool( "isHit", true );
        health -= damage;
        Debug.Log( $"{health}" );
        if(health <= 0)
        {
            Debug.Log( $"{health}" );
            canvas.SetActive( false );
            enemyAnimator.Play( $"{animData.enemyDeath}" );
        }
    }
}
