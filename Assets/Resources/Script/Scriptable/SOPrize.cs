
using UnityEngine;
[CreateAssetMenu(fileName = "Prize Data", menuName = "Scriptable Object/Prize Data", order = int.MaxValue)]

public class SOPrize : ScriptableObject
{
    [SerializeField]
    public enum EprizeType
    { 
        NONE,
        COIN,
        SPRIT
    
    }

    public int PrizeHP;
    public EprizeType PrizeType;
    public int PrizeCnt;

}
