using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    public enum ECellType
    {
        NONE,
        PRIZE,
        PET,
        UPGRADE,
        MINE

    }

    public ECellType eCellType;

    [SerializeField]
    Transform PrizeGroup;

    [SerializeField]
    Transform PetGroup;
    [SerializeField]
    Transform UpgradeGroup;
    [SerializeField]
    Transform MineGroup;

    private void Awake()
    {
        switch (eCellType)
        { 
            case ECellType.PRIZE:
                PrizeGroup.gameObject.SetActive (true);
                break;

            case ECellType.PET:
                PetGroup.gameObject.SetActive (true);
                break;
            case ECellType.UPGRADE:
                UpgradeGroup.gameObject.SetActive (true);
                break;
            case ECellType.MINE:
                MineGroup.gameObject.SetActive (true);
                break;

        }

    }


}
