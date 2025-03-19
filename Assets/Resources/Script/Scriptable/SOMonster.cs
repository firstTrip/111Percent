
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data", order = int.MaxValue)]
public class SOMonster : ScriptableObject
{
    public string MonsterName;
    public float MonsterSpeed;
    public float MonsterAttack;
    public float MonsterHP;

}
