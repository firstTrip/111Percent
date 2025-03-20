
using UnityEngine;
[CreateAssetMenu(fileName = "Magic Data", menuName = "Scriptable Object/Magic Data", order = int.MaxValue)]
public class SOMagic : ScriptableObject
{
    public int MagicDamage;
    public Sprite MagicImage;
    public float MagicCoolTime;
    public float MagicSpeed;
    public float MagicRange;
}
