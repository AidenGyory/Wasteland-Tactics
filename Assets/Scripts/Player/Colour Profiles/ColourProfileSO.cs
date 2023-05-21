using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Colour Profile", menuName = "Wasteland Tactics/Players/Create New Colour Profile")]
public class ColourProfileSO : ScriptableObject
{
    public Color PrimaryColor;
    public Color SecondaryColor;
    [Space]
    public Material[] Headquarters; 
    public Material[] Outpost; 
    public Material[] Generator; 
    public Material[] Refinery;
    public Material[] ResearchLab;
    [Space]
    public Material OutlineMaterial; 

}
