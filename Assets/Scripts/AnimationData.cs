using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationsData")]
public class AnimationData : ScriptableObject
{
    // state thing
    [SerializeField] public string idleNoWeapon;
    [SerializeField] public string walkNoWeapon;

    [SerializeField] public string idleRifle;
    [SerializeField] public string walkRifle;

    [SerializeField] public string idleMace;
    [SerializeField] public string walkMace;

    [SerializeField] public string idleTrident;
    [SerializeField] public string walkTrident;

    [SerializeField] public string actionRifle;
    [SerializeField] public string actionMace;
    [SerializeField] public string actionTrident;

    public string enemyHit;
    public string enemyDeath;

}
