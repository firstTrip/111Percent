using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

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


    [SerializeField]
    Button button;

    [SerializeField]
    int step;

    [SerializeField]
    SOPrize[] SOPrizeData;

    [SerializeField]
    SOPet[] SOPetData;

    [Space]
    [Header("Canvas")]
    Canvas uiCanvas;


    [Space]
    [Header("Upgrade Text")]
    [SerializeField]
    Text TXT_Cost;

    [SerializeField]
    Text TXT_NowUpgrade;

    //[SerializeField]
    //SOMine[] SOMineData;

    private void Awake()
    {
        switch (eCellType)
        { 
            case ECellType.PRIZE:
                PrizeGroup.gameObject.SetActive (true);
                button.onClick.AddListener(PrizeEvent);
                break;

            case ECellType.PET:
                PetGroup.gameObject.SetActive (true);
                button.onClick.AddListener(PETEvent);

                break;
            case ECellType.UPGRADE:
                UpgradeGroup.gameObject.SetActive (true);
                button.onClick.AddListener(UPGRADEEvent);

                switch (step)
                {
                    case (int)UpgradeType.GRADE:

                        TXT_NowUpgrade.text = GameManager.Instance.GetSummonsUpgrade().ToString();
                        TXT_Cost.text = (75 * GameManager.Instance.GetSummonsUpgrade()).ToString();
                        break;

                    case (int)UpgradeType.GROUND:

                        TXT_NowUpgrade.text = GameManager.Instance.GetMagicUpgrade(MagicType.GROUND).ToString();
                        TXT_Cost.text = GameManager.Instance.GetMagicUpgrade(MagicType.GROUND) >0 ? GameManager.Instance.GetMagicUpgrade(MagicType.GROUND).ToString() : "1" ;
                       
                        break;

                    case (int)UpgradeType.FIRE:

                        TXT_NowUpgrade.text = GameManager.Instance.GetMagicUpgrade(MagicType.FIRE).ToString();
                        TXT_Cost.text = GameManager.Instance.GetMagicUpgrade(MagicType.FIRE) > 0 ? GameManager.Instance.GetMagicUpgrade(MagicType.FIRE).ToString() : "1";


                        break;

                    case (int)UpgradeType.WATER:

                        TXT_NowUpgrade.text = GameManager.Instance.GetMagicUpgrade(MagicType.WATER).ToString();
                        TXT_Cost.text = GameManager.Instance.GetMagicUpgrade(MagicType.WATER) > 0 ? GameManager.Instance.GetMagicUpgrade(MagicType.WATER).ToString() : "1";

                        break;

                }

                break;
            case ECellType.MINE:
                MineGroup.gameObject.SetActive (true);
                button.onClick.AddListener(MINEEvent);

                break;
        }
        uiCanvas = UIManager.Instance.GetUICanvas();
    }

    void PrizeEvent()
    {
        GameManager.Instance.GetPlayerSpawn().CreatePrize(SOPrizeData[step-1]);
    }

    void PETEvent()
    {

    }

    enum UpgradeType
    {
        NONE,
        GRADE,
        GROUND,
        FIRE,
        WATER,
        PET

    }

    void UPGRADEEvent()
    {
        switch (step)
        {
            case (int)UpgradeType.GRADE:

                if(GameManager.Instance.GetCoin() < 1 * 75 * GameManager.Instance.GetSummonsUpgrade())
                {
                    var obj1 = PoolingManager.GetObj("Indicator");
                    obj1.transform.SetParent(uiCanvas.transform);
                    obj1.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    obj1.GetComponent<Indicator>().SetDesc("코인이 부족합니다.", 1.5f);
                    return;
                }

                GameManager.Instance.SetCoin((-1) * 75 * GameManager.Instance.GetSummonsUpgrade());
                GameManager.Instance.SummonsUpgrade();

                TXT_NowUpgrade.text = GameManager.Instance.GetSummonsUpgrade().ToString();
                TXT_Cost.text = (75 * GameManager.Instance.GetSummonsUpgrade()).ToString();
                break;

            case (int)UpgradeType.GROUND:

                if (GameManager.Instance.GetSprit() < 1 * (GameManager.Instance.GetMagicUpgrade(MagicType.GROUND) > 0 ? GameManager.Instance.GetMagicUpgrade(MagicType.GROUND) : 1))
                {
                    var obj2 = PoolingManager.GetObj("Indicator");
                    obj2.transform.SetParent(uiCanvas.transform);
                    obj2.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    obj2.GetComponent<Indicator>().SetDesc("영혼이 부족합니다.", 1.5f);
                    return;
                }
                GameManager.Instance.SetSprit((-1)  * GameManager.Instance.GetMagicUpgrade(MagicType.GROUND));
                GameManager.Instance.GroundUpgrade();

                TXT_NowUpgrade.text = GameManager.Instance.GetMagicUpgrade(MagicType.GROUND).ToString();
                TXT_Cost.text = GameManager.Instance.GetMagicUpgrade(MagicType.GROUND).ToString();
                break;

            case (int)UpgradeType.FIRE:

                if (GameManager.Instance.GetSprit() < 1 * (GameManager.Instance.GetMagicUpgrade(MagicType.FIRE) > 0 ? GameManager.Instance.GetMagicUpgrade(MagicType.FIRE) : 1))
                {
                    var obj3 = PoolingManager.GetObj("Indicator");
                    obj3.transform.SetParent(uiCanvas.transform);
                    obj3.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    obj3.GetComponent<Indicator>().SetDesc("영혼이 부족합니다.", 1.5f);
                    return;
                }
                GameManager.Instance.SetSprit((-1)  * GameManager.Instance.GetMagicUpgrade(MagicType.FIRE));
                GameManager.Instance.FireUpgrade();

                TXT_NowUpgrade.text = GameManager.Instance.GetMagicUpgrade(MagicType.FIRE).ToString();
                TXT_Cost.text = GameManager.Instance.GetMagicUpgrade(MagicType.FIRE).ToString();

                break;

            case (int)UpgradeType.WATER:

                if (GameManager.Instance.GetSprit() < 1 * (GameManager.Instance.GetMagicUpgrade(MagicType.WATER) > 0 ? GameManager.Instance.GetMagicUpgrade(MagicType.WATER) : 1))
                {
                    var obj4 = PoolingManager.GetObj("Indicator");
                    obj4.transform.SetParent(uiCanvas.transform);
                    obj4.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    obj4.GetComponent<Indicator>().SetDesc("영혼이 부족합니다.", 1.5f);
                    return;
                }
                GameManager.Instance.SetSprit((-1) * GameManager.Instance.GetMagicUpgrade(MagicType.WATER));
                GameManager.Instance.WaterUpgrade();

                TXT_NowUpgrade.text = GameManager.Instance.GetMagicUpgrade(MagicType.WATER).ToString();
                TXT_Cost.text = GameManager.Instance.GetMagicUpgrade(MagicType.WATER).ToString();
                break;

            case (int)UpgradeType.PET:

                var obj5 = PoolingManager.GetObj("Indicator");
                obj5.transform.SetParent(uiCanvas.transform);
                obj5.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                obj5.GetComponent<Indicator>().SetDesc("미개발 기능입니다.", 1.5f);
                return;
                break;

        }

    }

    void MINEEvent()
    {

    }
}
