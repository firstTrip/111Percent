using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    RectTransform attackPos;

    [SerializeField]
    float HP;

    public void Attack(SOMagic magic)
    {
        var obj = PoolingManager.GetObj("Magic");
        obj.GetComponent<Magic>().Init(magic);
        obj.transform.position = attackPos.position;
    }
}
