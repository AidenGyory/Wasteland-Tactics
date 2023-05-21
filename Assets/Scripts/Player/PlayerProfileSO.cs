using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FactionType
{
    NewEden,
    Sapien,
    StarBorn,
    CyberSwarm
}

public enum SuperPowerType
{
    //New Eden
    AirStrike, // - Damage all enemy units in sight 
    RepairAid, // - heal all friendly units 
    Overclock, // - Gain another round of turn resources 

    //SAPI-3N
    OrbitalStrike, // - Deal massive damage to a single unit 
    ForceField, // - Place 3 "forcefield buildings on the map (similar to outposts, firendly units can move through, provide defense and need to be destoryed to get through) 
    Blackout, // - Turn off the enemies resource collection for the next turn 

    //Starborn
    GravityMine, // - set a mine trap that only you can see, when an enemy walks on that tile deal damaghe to them and surrounding units. 
    Enhancement, // - level up all friendly units  
    Teleport, // - select a unit and move it to any other tile on the map 

    //CyberSwarm
    Assimilate, // - pick a tile, all units surrounding this tile are destroyed and a super unit is created that is multiplied by the amount of units destroyed. 
    Cloak, // - Unit is now invisible to all but you until it deals damage. 
    DataAbsorption // - Fog of war is removed and you can now see everything on the board. 

}

[CreateAssetMenu(fileName = "New Player Profile", menuName = "Wasteland Tactics/Players/Create New Player Profile")]
public class PlayerProfileSO : ScriptableObject
{
    public string playerProfileName;
    [Space]
    public ColourProfileSO colourProfile;
    [Space]
    public FactionType playerFaction;
    public SuperPowerType playerSuperPower;

    //Add AI controller Profile for difficulty levels etc. 
}
