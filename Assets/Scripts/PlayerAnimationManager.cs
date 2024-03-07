using System;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public static event Action OnRifleShoot;

    [SerializeField] Animator animator;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] AnimationData anims;

    [SerializeField] GameObject[] weapons;

    bool isHitting = false;

    int currentState = 1;

    void Start()
    {
        SetWeapon( null );
    }

    void Update()
    {
        ChangeWeapons();
        NoWeaponHandler();
        RifleHandler();
        MaceHandler();
        TridentHandler();

    }


    private void TridentHandler()
    {
        // walkTrident
        if( playerMovement.isWalking && currentState == 4 && !isHitting)
        {
            animator.Play( $"{anims.walkTrident}" );
        }
        // idleTrident
        if( !playerMovement.isWalking && currentState == 4 && !isHitting )
        {
            animator.Play( $"{anims.idleTrident}" );
        }
        if(Input.GetKeyDown(KeyCode.Space) )
        {
            isHitting = true;
            if( currentState == 4 )
            {
                animator.Play( $"{anims.actionTrident}" );
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            isHitting = false;
        }
    }

    private void MaceHandler()
    {
        // walkMace
        if( playerMovement.isWalking && currentState == 3 && !isHitting )
        {
            animator.Play( $"{anims.walkMace}" );
        }
        // idleMace
        if( !playerMovement.isWalking && currentState == 3 && !isHitting )
        {
            animator.Play( $"{anims.idleMace}" );
        }
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            isHitting = true;
            if( currentState == 3 )
            {
                animator.Play( $"{anims.actionMace}" );
            }
        }
        if( Input.GetKeyUp( KeyCode.Space ) )
        {
            isHitting = false;
        }
    }

    private void RifleHandler()
    {
        // walkRifle
        if( playerMovement.isWalking && currentState == 2 && !isHitting )
        {
            animator.Play( $"{anims.walkRifle}" );
        }
        // idleRifle
        if( !playerMovement.isWalking && currentState == 2 && !isHitting )
        {
            animator.Play( $"{anims.idleRifle}" );
        }
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            isHitting = true;
            if( currentState == 2 )
            {
                OnRifleShoot?.Invoke();
            }
        }
        if( Input.GetKeyUp( KeyCode.Space ) )
        {
            isHitting = false;
        }
    }

    private void NoWeaponHandler()
    {
        // walkNoWeapon
        if( playerMovement.isWalking && currentState == 1 )
        {
            animator.Play( $"{anims.walkNoWeapon}" );
        }
        // idleNoWeapon
        if( !playerMovement.isWalking && currentState == 1 )
        {
            animator.Play( $"{anims.idleNoWeapon}" );
        }
    }

    void SetWeapon( GameObject targetWeapon)
    {
        foreach (var weapon in weapons)
        {
            if(weapon != targetWeapon)
            {
                weapon.SetActive( false );
            }
            else
            {
                weapon.SetActive( true );
            }
        }
    }

    void ChangeWeapons()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            // no weapon
            currentState = 1;
            Debug.Log( "No Weapon Selected" );
            SetWeapon( null );
        }
        if( Input.GetKeyDown( KeyCode.Alpha2 ) )
        {
            // rifle
            currentState = 2;
            Debug.Log( "Rifle Selected" );
            SetWeapon( weapons[0] );
        }
        if( Input.GetKeyDown( KeyCode.Alpha3 ) )
        {
            // mace
            currentState = 3;
            Debug.Log( "mace Selected" );
            SetWeapon( weapons[1] );
        }
        if( Input.GetKeyDown( KeyCode.Alpha4 ) )
        {
            // trident
            currentState = 4;
            Debug.Log( "trident Selected" );
            SetWeapon( weapons[2] );
        }
    }
}
