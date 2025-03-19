
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", menuName = "Scriptable Object/Stage Data", order = int.MaxValue)]
public class SOStage : ScriptableObject
{
    public int time;
    public int MonsterCnt;
    public int MonsterHP;
}
