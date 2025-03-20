
using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Pet Data", menuName = "Scriptable Object/Pet Data", order = int.MaxValue)]
public class SOPet : ScriptableObject
{
    public int PetAttack;
    public string[] condition;
    public Action action;

}
