using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class TabManager : MonoBehaviour
{
    [Header("Button Group")]
    
    [SerializeField]
    Button BTN_Prize;
    
    [SerializeField]
    Button BTN_Pet;
    
    [SerializeField]
    Button BTN_Summons;

    [SerializeField]
    Button BTN_Upgrade;

    [SerializeField]
    Button BTN_Mine;

    [Header("Magic Panel")]
    [SerializeField]
    Image IMG_MagicPanel;


    [Space]
    [Header("Canvas")]
    Canvas uiCanvas;

    [SerializeField]
    List<GameObject> panelList = new List<GameObject>();

    [SerializeField]
    Vector3 originPanel;

    [SerializeField]
    Vector3 upPanelTran;

    private void Awake()
    {
        BTN_Prize.onClick.AddListener(PrizeEvent);
        BTN_Pet.onClick.AddListener(PetEvent);
        BTN_Summons.onClick.AddListener(SummonsEvent);
        BTN_Upgrade.onClick.AddListener(UpgradeEvent);
        BTN_Mine.onClick.AddListener(MineEvent);

        uiCanvas = UIManager.Instance.GetUICanvas();
        originPanel = IMG_MagicPanel.transform.position;
        upPanelTran = IMG_MagicPanel.transform.position + new Vector3(0, 200, 0);
    }

    void PrizeEvent()
    {
        Debug.Log("PrizeEvent");

        BTN_Prize.GetComponent<TabUnit>().ActivePanel(MoveMagicPanel, RevertMagicPanel);
    }


    void PetEvent()
    {
        Debug.Log("PetEvent");
        BTN_Pet.GetComponent<TabUnit>().ActivePanel(MoveMagicPanel, RevertMagicPanel);
    }

    void SummonsEvent()
    {
        Debug.Log("SummonsEvent");
        SpawnMagic();
    }

    void UpgradeEvent()
    {
        Debug.Log("UpgradeEvent");
        BTN_Upgrade.GetComponent<TabUnit>().ActivePanel(MoveMagicPanel, RevertMagicPanel);
    }

    void MineEvent()
    {
        Debug.Log("MineEvent");
        BTN_Mine.GetComponent<TabUnit>().ActivePanel(MoveMagicPanel, RevertMagicPanel);
    }

    void SpawnMagic()
    {
        if(GameManager.Instance.GetCoin() < GameManager.Instance.GetSummons())
        {
            var obj = PoolingManager.GetObj("Indicator");
            obj.transform.SetParent(uiCanvas.transform);
            obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            obj.GetComponent<Indicator>().SetDesc("코인이 부족합니다.", 1.5f);
        }
        else
        {

            if(GameManager.Instance.GetMagicCnt() >= GameManager.Instance.GetMaxMagicCnt())
            {
                var obj = PoolingManager.GetObj("Indicator");
                obj.transform.SetParent(uiCanvas.transform);
                obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                obj.GetComponent<Indicator>().SetDesc("주술 최대치 입니다.", 1.5f);
                return;
            }
            // 소환 로직 
            GameManager.Instance.SetCoin(GameManager.Instance.GetSummons() * (-1));
            GameManager.Instance.IncreaseSummonsCnt();
            var picker = new MagicRandomMangager<GradeType>();
            var TypePicker = new MagicRandomMangager<MagicType>();

            SOSummons data = GameManager.Instance.GetSummonsData();
            GradeType[] grade = GameManager.Instance.summonsGrade;
            for (int i =0;i< grade.Length;++i)
            {
                picker.AddEntry(grade[i], data.SummonsWeghitRate[i]);
            }

            TypePicker.AddEntry(MagicType.GROUND, 33);
            TypePicker.AddEntry(MagicType.FIRE, 33);
            TypePicker.AddEntry(MagicType.WATER, 33);

            GameManager.Instance.AddMagic(TypePicker.PickRandom(), picker.PickRandom());

            UIManager.Instance.GetMagicPanel().SetMagic();
            UIManager.Instance.GetMagicPanel().SetCoinText();
            UIManager.Instance.GetMagicPanel().SetMagicCntText();
            BTN_Summons.GetComponent<TabUnit>().SetCnt(GameManager.Instance.GetSummons());
        }
    }


    void MoveMagicPanel()
    {
        AllClosePanel();
        IMG_MagicPanel.transform.position = upPanelTran;
    }


    void RevertMagicPanel()
    {
        if(!IsActivePanel())
            IMG_MagicPanel.transform.position = originPanel;
    }

    void AllClosePanel()
    {
        foreach(var data in panelList)
        {
            data.SetActive(false);
        }
    }

    bool IsActivePanel()
    {
        foreach (var data in panelList)
        {
            if(data.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
}
