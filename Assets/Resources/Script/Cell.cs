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


    [SerializeField]
    SOMine[] SOMineData;

    [Space]
    [Header("Canvas")]
    Canvas uiCanvas;


    [Space]
    [Header("Upgrade Text")]
    [SerializeField]
    Text TXT_Cost;

    [SerializeField]
    Text TXT_NowUpgrade;

    [Space]
    [Header("Mine Text")]
    [SerializeField]
    Text TXT_MineCost;

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

                switch (step)
                {
                    case (int)MineType.ONE:

                        TXT_MineCost.text = "1";
                        break;

                    case (int)MineType.TWO:
                        TXT_MineCost.text = "3";
                        break;

                    case (int)MineType.THREE:
                        TXT_MineCost.text = "7";
                        break;
                }

                break;
        }
        uiCanvas = UIManager.Instance.GetUICanvas();
    }


    float timer;
    void PrizeEvent()
    {
        timer = UIManager.Instance.GetTabManager().GetTimer();

        if (timer >0)
        {
            GameManager.Instance.GetPlayerSpawn().CreatePrize(SOPrizeData[step - 1]);
            UIManager.Instance.GetTabManager().StartTimer();
        }
        else
        {
            var obj = PoolingManager.GetObj("Indicator");
            obj.transform.SetParent(uiCanvas.transform);
            obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            obj.GetComponent<Indicator>().SetDesc("현상범이 부족합니다.", 1.5f);
            return;
        }
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

    enum MineType
    {
        NONE,
        ONE,
        TWO,
        THREE,
    }

    void MINEEvent()
    {
        switch (step)
        { 
            case (int)MineType.ONE:
                if (GameManager.Instance.GetSprit() < 1)
                {
                    var obj2 = PoolingManager.GetObj("Indicator");
                    obj2.transform.SetParent(uiCanvas.transform);
                    obj2.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    obj2.GetComponent<Indicator>().SetDesc("영혼이 부족합니다.", 1.5f);
                    return;
                }
                GameManager.Instance.SetSprit(-1);
                GameManager.Instance.IncreaseSummonsCnt();
                var picker = new MagicRandomMangager<GradeType>();
                var TypePicker = new MagicRandomMangager<MagicType>();

                SOMine data = SOMineData[0];
                GradeType[] grade = GameManager.Instance.summonsGrade;
                for (int i = 0; i < grade.Length; ++i)
                {
                    picker.AddEntry(grade[i], data.SummonsWeghitRate[i]);
                }

                TypePicker.AddEntry(MagicType.GROUND, 33);
                TypePicker.AddEntry(MagicType.FIRE, 33);
                TypePicker.AddEntry(MagicType.WATER, 33);

                GameManager.Instance.AddMagic(TypePicker.PickRandom(), picker.PickRandom());

                UIManager.Instance.GetMagicPanel().SetMagic();

                break;

            case (int)MineType.TWO:

                if (GameManager.Instance.GetSprit() < 3)
                {
                    var obj2 = PoolingManager.GetObj("Indicator");
                    obj2.transform.SetParent(uiCanvas.transform);
                    obj2.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    obj2.GetComponent<Indicator>().SetDesc("영혼이 부족합니다.", 1.5f);
                    return;
                }
                GameManager.Instance.SetSprit((-1) * 3);
                GameManager.Instance.IncreaseSummonsCnt();
                var picker2 = new MagicRandomMangager<GradeType>();
                var TypePicker2 = new MagicRandomMangager<MagicType>();

                SOMine data2 = SOMineData[1];
                GradeType[] grade2 = GameManager.Instance.summonsGrade;
                for (int i = 0; i < grade2.Length; ++i)
                {
                    picker2.AddEntry(grade2[i], data2.SummonsWeghitRate[i]);
                }

                TypePicker2.AddEntry(MagicType.GROUND, 33);
                TypePicker2.AddEntry(MagicType.FIRE, 33);
                TypePicker2.AddEntry(MagicType.WATER, 33);

                GameManager.Instance.AddMagic(TypePicker2.PickRandom(), picker2.PickRandom());

                UIManager.Instance.GetMagicPanel().SetMagic();

                break;

            case (int)MineType.THREE:

                if (GameManager.Instance.GetSprit() < 7)
                {
                    var obj2 = PoolingManager.GetObj("Indicator");
                    obj2.transform.SetParent(uiCanvas.transform);
                    obj2.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    obj2.GetComponent<Indicator>().SetDesc("영혼이 부족합니다.", 1.5f);
                    return;
                }

                GameManager.Instance.SetSprit((-1) * 7);
                GameManager.Instance.IncreaseSummonsCnt();
                var picker3 = new MagicRandomMangager<GradeType>();
                var TypePicker3 = new MagicRandomMangager<MagicType>();

                SOMine data3 = SOMineData[2];
                GradeType[] grade3 = GameManager.Instance.summonsGrade;
                for (int i = 0; i < grade3.Length; ++i)
                {
                    picker3.AddEntry(grade3[i], data3.SummonsWeghitRate[i]);
                }

                TypePicker3.AddEntry(MagicType.GROUND, 33);
                TypePicker3.AddEntry(MagicType.FIRE, 33);
                TypePicker3.AddEntry(MagicType.WATER, 33);

                GameManager.Instance.AddMagic(TypePicker3.PickRandom(), picker3.PickRandom());

                UIManager.Instance.GetMagicPanel().SetMagic();


                break;
        }

    }
}
