
using UnityEngine;
using static SOPrize;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data", order = int.MaxValue)]
public class SOMonster : ScriptableObject
{
    public string MonsterName;
    public float MonsterSpeed;
    public float MonsterAttack;
    public float MonsterHP;

    public EprizeType PrizeType;
    public int PrizeCnt;

}
