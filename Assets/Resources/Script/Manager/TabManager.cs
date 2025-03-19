using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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


    private void Awake()
    {
        BTN_Prize.onClick.AddListener(PrizeEvent);
        BTN_Pet.onClick.AddListener(PetEvent);
        BTN_Summons.onClick.AddListener(SummonsEvent);
        BTN_Upgrade.onClick.AddListener(UpgradeEvent);
        BTN_Mine.onClick.AddListener(MineEvent);

        uiCanvas = UIManager.Instance.GetUICanvas();
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
            // 소환 로직 
            GameManager.Instance.SetCoin(GameManager.Instance.GetSummons() * (-1));
            GameManager.Instance.IncreaseSummonsCnt();
            UIManager.Instance.GetMagicPanel().SetCoinText();
            BTN_Summons.GetComponent<TabUnit>().SetCnt(GameManager.Instance.GetSummons());
        }
    }


    void MoveMagicPanel()
    {
        IMG_MagicPanel.transform.position += new Vector3(0, 200, 0);
    }


    void RevertMagicPanel()
    {
        IMG_MagicPanel.transform.position -= new Vector3(0, 200, 0);
    }

}
